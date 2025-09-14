using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SPT_API.Data;
using SPT_API.Data.DTOs;
using SPT_API.Models;
using SPT_API.Services.Password;


namespace SPT_API.Services.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly SPT_APIDbContext _db;

        public StudentService(SPT_APIDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<StudentModel>> GetAllStudents()
        {
            var students = await _db.StudentTable.ToListAsync();
            return students;
        }

        public async Task<StudentModel> GetStudentByParams(string FirstName, string LastName)
        {
            return await _db.StudentTable.FirstOrDefaultAsync(s => s.firstName == FirstName && s.lastName == LastName);
        }

        public async Task<StudentModel> GetStudentByUsername(string username)
        {
            var studentByUser = await _db.StudentTable.FirstOrDefaultAsync(s => s.studentUserName == username);
            return studentByUser;
        }
        public async Task<StudentModel> GetStudentByEmail(string email)
        {
            var studentByEmail  = await _db.StudentTable.FirstOrDefaultAsync(s => s.email == email);
            return studentByEmail;
        }

        public async Task<StudentModel> AddStudent(StudentModel student, IPasswordService passwordService)
        {
            var hashedPassword = passwordService.HashPassword(student.studentPassword);

            student.studentPassword = hashedPassword;
            student.uniqueUserId = Guid.NewGuid().ToString("N").Substring(0, 8);
            student.studentUserName =
               student.firstName.Substring(0, 2).ToLower() + student.lastName.Substring(0, 2).ToLower() + student.uniqueUserId.Substring(5, 2).ToLower();


            _db.StudentTable.AddAsync(student);
            _db.SaveChangesAsync();

            return student;
        }

        public async Task<DeleteUserResponse> DeleteStudent(DeleteUserDTO deleteReq, IPasswordService passwordService)
        {
            var acpass = await _db.StudentTable.FirstOrDefaultAsync(s => s.studentUserName == deleteReq._studentUsername);
            DeleteUserResponse delResponse = new DeleteUserResponse();
            if (!passwordService.VerifyPassword(deleteReq._studentPassword, acpass.studentPassword))
            {
                delResponse.Success = false;
                delResponse.Message = "Invalid Password";
                return delResponse;
            }
            var studentDelReq = _db.StudentTable.FirstOrDefaultAsync(s => s.firstName == deleteReq._firstName
           && s.lastName == deleteReq._lastName
           && s.studentUserName == deleteReq._studentUsername
           && s.studentPassword == deleteReq._studentPassword);

            if (studentDelReq == null)
            {
                delResponse.Success = false;
                delResponse.Message = "Student not Found";
                return delResponse;
            }
            
            _db.Remove(studentDelReq);
            _db.SaveChangesAsync();

            delResponse.Success = true;
            delResponse.Message = "Success";
            return delResponse;
        }

        public async Task<StudentModel> EditStudent(string userName, updateStudentDTO edit)
        {
            var student = await _db.StudentTable.FirstOrDefaultAsync(s => s.studentUserName == userName);
            if (student == null) { return null; }

            var studentType = typeof(StudentModel);
            var dtoType = typeof(updateStudentDTO);

            var properties = typeof(StudentModel).GetProperties();
            foreach (var dtoProperty in dtoType.GetProperties())
            {
                var newValue = dtoProperty.GetValue(edit);
                if (newValue != null)
                {
                    var modelProp = studentType.GetProperty(dtoProperty.Name);
                    if (modelProp != null && modelProp.CanWrite)
                    {
                        modelProp.SetValue(student, newValue);
                    }

                }
            }
            _db.SaveChangesAsync();
            return student;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginReq, IPasswordService passwordService)
        {
            var response = new LoginResponseDTO();

            
            if (string.IsNullOrWhiteSpace(loginReq._Password) ||
            (string.IsNullOrWhiteSpace(loginReq._UserName) && string.IsNullOrWhiteSpace(loginReq._Email)))
            {
                response.Success = false;
                response.Message = "Username/Email or Password is required";
                return response;
            }

           
            var student = await _db.StudentTable.FirstOrDefaultAsync(s =>
            (!string.IsNullOrEmpty(loginReq._UserName) && s.studentUserName == loginReq._UserName) ||
            (!string.IsNullOrEmpty(loginReq._Email) && s.email == loginReq._Email));


           
            if (student == null || !passwordService.VerifyPassword(loginReq._Password, student.studentPassword))
            {
                response.Success = false;
                response.Message = "Invalid Username/Email or Password";
                return response;
            }

            
            response.Success = true;
            response.Message = "Login successful";
            response.studentID = student.uniqueUserId;

            var key = Encoding.UTF8.GetBytes("whatthehellisthisthingsproblemjesustakecontrol"); // must match Program.cs exactly
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, student.uniqueUserId)}),
                Expires = DateTime.UtcNow.AddDays(1),
                NotBefore = DateTime.UtcNow,                // valid immediately
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            response.token = tokenHandler.WriteToken(token);


            return response;
        }

    }
    
}


