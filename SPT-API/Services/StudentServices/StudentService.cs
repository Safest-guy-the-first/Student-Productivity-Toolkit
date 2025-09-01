using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<StudentModel> GetAllStudents()
        {
            return _db.StudentTable.ToList();
        }

        public StudentModel GetStudentByParams(string FirstName, string LastName)
        {
            return _db.StudentTable.FirstOrDefault(s => s.firstName == FirstName && s.lastName == LastName);
        }

        public StudentModel GetStudentByUsername(string username)
        {
            return _db.StudentTable.FirstOrDefault(s => s.studentUserName == username);
        }

        public StudentModel AddStudent(StudentModel student, IPasswordService passwordService)
        {
            var hashedPassword = passwordService.HashPassword(student.studentPassword);

            student.studentPassword = hashedPassword;
            student.uniqueUserId = Guid.NewGuid().ToString("N").Substring(0, 8);
            student.studentUserName =
               student.firstName.Substring(0, 2).ToLower() + student.lastName.Substring(0, 2).ToLower() + student.uniqueUserId.Substring(5, 2).ToLower();


            _db.StudentTable.Add(student);
            _db.SaveChanges();

            return student;
        }

        public DeleteUserResponse DeleteStudent(DeleteUserDTO deleteReq, IPasswordService passwordService)
        {
            var acpass = _db.StudentTable.FirstOrDefault(s => s.studentUserName == deleteReq._studentUsername);
            DeleteUserResponse delResponse = new DeleteUserResponse();
            if (acpass == null)
            {
                delResponse.Success = false;
                delResponse.Message = "Invalid Username";
                return delResponse;
            }

            if (!passwordService.VerifyPassword(deleteReq._studentPassword, acpass.studentPassword))
            {
                delResponse.Success = false;
                delResponse.Message = "Invalid Password";
                return delResponse;
            }
            var studentDelReq = _db.StudentTable.FirstOrDefault(s => s.firstName == deleteReq._firstName
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
            _db.SaveChanges();

            delResponse.Success = true;
            delResponse.Message = "Success";
            return delResponse;
        }

        public StudentModel EditStudent(string userName, updateStudentDTO edit) //this function has reflection study it well
        {
            var student = _db.StudentTable.FirstOrDefault(s => s.studentUserName == userName);
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
            _db.SaveChanges();
            return student;
        }

        public LoginResponseDTO Login(LoginRequestDTO loginReq, IPasswordService passwordService)
        {
            var response = new LoginResponseDTO();

            
            if (string.IsNullOrWhiteSpace(loginReq._Password) ||
            (string.IsNullOrWhiteSpace(loginReq._UserName) && string.IsNullOrWhiteSpace(loginReq._Email)))
            {
                response.Success = false;
                response.Message = "Username/Email or Password is required";
                return response;
            }

           
            var student = _db.StudentTable.FirstOrDefault(s =>
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

            return response;
        }

    }
    
}


