namespace Application.Interfaces;

public interface IAdminService
{
    Task DeleteAccountAsync(TeacherLoginRequest request);
    Task DeleteAccountAsync(LoginRequest request);

    Task<TeacherRegisterResponse> RegisterTeacherAsync(TeacherRegisterRequest request);

    Task<RegisterResponse> RegisterAdminAsync(RegistrationRequest request);

    Task<RegisterResponse> RegisterSuperAdminAsync(RegistrationRequest request);

    Task<IEnumerable<TeacherWithFansRequest>> GetAllTeachersWithFanAsync();
    Task<IEnumerable<TeacherWithFansRequest>> GetAllByIdTeacherWithFanAsync(string id);
    Task ConfirmEmailAsync(string email);

}
