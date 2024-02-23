

namespace Application.Services
{
    public class TeacherService(UserManager<Domain.Entities.Entity.Teachers.Teacher> userManager,
                                IConfiguration configuration,
                                RoleManager<ApplicationRole> roleManager, 
                                IUnitOfWork unitOfWork) : ITeacherService
    {
        private readonly UserManager<Domain.Entities.Entity.Teachers.Teacher> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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
                    throw new CustomException("Failed to change password");

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
                throw new CustomException("Failed to delete user");
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

        public async Task<IEnumerable<TeacherWithFansRequest>> GetAllTeachersWithFanAsync()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");

            var teachersWithFans = new List<TeacherWithFansRequest>();

            foreach (var teacher in teachers)
            {
                List<Fan> fans = new();
                var teacherWithFans = new TeacherWithFansRequest
                {
                    Teacher = teacher,
                    Fans = fans
                };

                var associatedFanIds = teacher.FanIds;

                foreach (var fanId in associatedFanIds)
                {
                    var fan = await _unitOfWork.FanRepository.GetByIdAsync(fanId);
                    if (fan != null)
                    {
                        teacherWithFans.Fans.Add(fan);
                    }
                 
                }

                teachersWithFans.Add(teacherWithFans);
            }

            return teachersWithFans;
        }

        public async Task<IEnumerable<TeacherWithFansRequest>> GetAllByIdTeacherWithFanAsync(string id)
        {
            var teacher = await _userManager.FindByIdAsync(id);
            if (teacher == null)
            {
                throw new NotFoundException("Teacher topilmadi");
            }

            var teacherWithFans = new TeacherWithFansRequest
            {
                Teacher = teacher,
                Fans = new List<Fan>()
            };

            var associatedFanIds = teacher.FanIds;

            foreach (var fanId in associatedFanIds)
            {
                var fan = await _unitOfWork.FanRepository.GetByIdAsync(fanId);
                if (fan != null)
                {
                    teacherWithFans.Fans.Add(fan);
                }
             
            }

            return new List<TeacherWithFansRequest> { teacherWithFans };
        }

    }
}
