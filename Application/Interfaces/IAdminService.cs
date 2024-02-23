using Application.Dtos.AuthDto;
using Application.Dtos.TeacherDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IAdminService
{
    Task DeleteAccountAsync(TeacherLoginRequest request);
    Task DeleteAccountAsync(LoginRequest request);

    Task<TeacherRegisterResponse> RegisterTeacherAsync(TeacherRegisterRequest request);

    Task<RegisterResponse> RegisterAdminAsync(RegistrationRequest request);

    Task<RegisterResponse> RegisterSuperAdminAsync(RegistrationRequest request);
}
