using SPT_API.Data.DTOs;
using SPT_API.Models;
using SPT_API.Services.Password;

namespace SPT_API.Services.StudentServices
{
    public interface IStudentService
    {
        IEnumerable<StudentModel> GetAllStudents();
        StudentModel GetStudentByParams(string FirstName, string LastName);
        StudentModel GetStudentByUsername(string username);
        StudentModel AddStudent(StudentModel student, IPasswordService passwordService);
        DeleteUserResponse DeleteStudent(DeleteUserDTO deleteReq, IPasswordService passwordService);
        StudentModel EditStudent(string userName, updateStudentDTO edit);
        LoginResponseDTO Login(LoginRequestDTO loginReq, IPasswordService passwordService);
    }
}
