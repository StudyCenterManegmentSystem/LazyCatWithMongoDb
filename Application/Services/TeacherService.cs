using Amazon.Runtime.Internal;
using Application.Commens.Helpers;
using Application.Dtos.TeacherDto;
using Domain.Entities.Entity.Teachers;

namespace Application.Services
{
    public class TeacherService(UserManager<Teacher> userManager,
                                IConfiguration configuration,
                                RoleManager<ApplicationRole> roleManager) : ITeacherService
    {
        private readonly UserManager<Teacher> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

        public async Task<TeacherLoginResponse> LoginAsync(TeacherLoginRequest request)
        {
            var teacher = await _userManager.FindByEmailAsync(request.Email);
            if (teacher == null)
                throw new CustomException("Invalid email/password");

            // Check password validity
            var passwordValid = await _userManager.CheckPasswordAsync(teacher, request.Password);
            if (!passwordValid)
                throw new CustomException("Invalid email/password");

            var roles = await _userManager.GetRolesAsync(teacher);
            var token = JwtHelperForTeacher.GenerateJwtToken(teacher, roles, _configuration);

            return new TeacherLoginResponse
            {
                AccessToken = token,
                Message = "Login Successful",
                Email = teacher.Email,
                Success = true,
                UserId = teacher.Id.ToString()
            };
        }


        public async Task<TeacherLoginResponse> ChangePasswordAsync(TeacherChangePasswordRequest dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                    throw new NotFoundException("User not found");

                var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
                if (!result.Succeeded)
                    throw new ValidationException("Failed to change password");

                var roles = await _userManager.GetRolesAsync(user);
                var token = JwtHelperForTeacher.GenerateJwtToken(user, roles, _configuration);

                // Generate the new token before removing the old one
                await _userManager.RemoveAuthenticationTokenAsync(user, _configuration["Jwt:Issuer"] ?? "", "Token");

                return new TeacherLoginResponse
                {
                    AccessToken = token,
                    Message = "Password changed successfully",
                    Email = user.Email,
                    Success = true,
                    UserId = user.Id.ToString()
                };
            }
            catch (NotFoundException ex)
            {
                return new TeacherLoginResponse { Success = false, Message = ex.Message };
            }
            catch (ValidationException ex)
            {
                return new TeacherLoginResponse { Success = false, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new TeacherLoginResponse { Success = false, Message = $"An error occurred while changing password: {ex.Message}" };
            }
        }

        public async Task DeleteAccountAsync(TeacherLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new NotFoundException("User not found");

            // Check password validity
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
                throw new CustomException("Invalid email/password");

            await _userManager.RemoveAuthenticationTokenAsync(user, _configuration["Jwt:Issuer"] ?? "", "Token");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new ValidationException("Failed to delete user");
        }

        public async Task LogoutAsync(TeacherLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new NotFoundException("User not found");
            // Check password validity
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
                throw new CustomException("Invalid email/password");

            await _userManager.RemoveAuthenticationTokenAsync(user, _configuration["Jwt:Issuer"] ?? "", "Token");
        }
    }
}
