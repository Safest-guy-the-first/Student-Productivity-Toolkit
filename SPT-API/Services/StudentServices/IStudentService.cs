using SPT_API.Data.DTOs;
using SPT_API.Models;
using SPT_API.Services.Password;

namespace SPT_API.Services.StudentServices
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> GetAllStudents();
        Task<StudentModel> GetStudentByParams(string FirstName, string LastName);
        Task<StudentModel> GetStudentByUsername(string username);
        Task<StudentModel> GetStudentByEmail(string username);
        Task<StudentModel> AddStudent(StudentModel student, IPasswordService passwordService);
        Task<DeleteUserResponse> DeleteStudent(DeleteUserDTO deleteReq, IPasswordService passwordService);
        Task<StudentModel> EditStudent(string userName, updateStudentDTO edit);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginReq, IPasswordService passwordService);
    }
}
