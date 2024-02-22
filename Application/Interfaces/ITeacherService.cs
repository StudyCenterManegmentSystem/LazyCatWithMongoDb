using Application.Dtos.TeacherDto;

namespace Application.Interfaces;

public interface ITeacherService
{
    Task<TeacherLoginResponse> LoginAsync(TeacherLoginRequest request);
    Task LogoutAsync(TeacherLoginRequest request);
    Task<TeacherLoginResponse> ChangePasswordAsync(TeacherChangePasswordRequest dto);
    Task DeleteAccountAsync(TeacherLoginRequest request);

}
