namespace Application.Interfaces;

public interface ITeacherService
{
    Task<TeacherLoginResponse> LoginAsync(TeacherLoginRequest request);
    Task<IEnumerable<TeacherWithFansRequest>> GetAllTeachersWithFanAsync();
    Task<IEnumerable<TeacherWithFansRequest>> GetAllByIdTeacherWithFanAsync(string id);

    Task LogoutAsync(TeacherLoginRequest request);
    Task<TeacherLoginResponse> ChangePasswordAsync(TeacherChangePasswordRequest dto);
    Task DeleteAccountAsync(TeacherLoginRequest request);


}
