



namespace Application.Services;

public class AdminService (UserManager<Teacher> userManager,
                           IConfiguration configuration,
                           RoleManager<ApplicationRole> roleManager,
                           UserManager<ApplicationUser> userManager1, 
                           IUnitOfWork unitOfWork) : IAdminService
{
    private readonly UserManager<Teacher> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly UserManager<ApplicationUser> _userManager1 = userManager1;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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
    public async Task DeleteAccountAsync(LoginRequest request)
    {
        var user = await _userManager1.FindByEmailAsync(request.Email);
        if (user == null)
            throw new NotFoundException("User not found");

        // Check password validity
        var passwordValid = await _userManager1.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
            throw new CustomException("Invalid email/password");

        await _userManager1.RemoveAuthenticationTokenAsync(user, _configuration["Jwt:Issuer"] ?? "", "Token");

        var result = await _userManager1.DeleteAsync(user);
        if (!result.Succeeded)
            throw new ValidationException("Failed to delete user");
    }
    public async Task<TeacherRegisterResponse> RegisterTeacherAsync(TeacherRegisterRequest request)
    {
        try
        {
            foreach (var id in request.FanIds)
            {
                if (!ObjectId.TryParse(id, out ObjectId objectId))
                {
                    throw new CustomException("Fan identifikatorlari ObjectId ko'rinishida emas");
                }

                var fan = await _unitOfWork.FanRepository.GetByIdAsync(id);
                if (fan is null)
                {
                    throw new NotFoundException("Bunday fanlar mavjud emas");
                }
            }

            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                throw new CustomException("Teacher already exists");

            var teacher = new Teacher
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                FanIds = request.FanIds
            };

            var createUserResult = await _userManager.CreateAsync(teacher, request.Password);
            if (!createUserResult.Succeeded)
                throw new ValidationException($"Create teacher failed {createUserResult?.Errors?.First()?.Description}");

            if (!await _roleManager.RoleExistsAsync(IdentityRoles.TEACHER))
                await _roleManager.CreateAsync(new ApplicationRole(IdentityRoles.TEACHER));

            if (teacher != null)
                await _userManager.AddToRoleAsync(teacher, IdentityRoles.TEACHER);

            return new TeacherRegisterResponse { Success = true, Message = "Teacher registered successfully", TeacherId =teacher.Id.ToString() };
        }
        catch (CustomException ex)
        {
            return new TeacherRegisterResponse { Success = false, Message = ex.Message  };
        }
        catch (ValidationException ex)
        {
            return new TeacherRegisterResponse { Success = false, Message = ex.Message };
        }
        catch (InvalidDataException ex)
        {
            return new TeacherRegisterResponse { Success = false, Message = ex.Message };
        }
        catch (Exception ex)
        {
            return new TeacherRegisterResponse { Success = false, Message = ex.Message };
        }
    }

    public async Task<RegisterResponse> RegisterAdminAsync(RegistrationRequest request)
    {
        
        try
        {
            var userExists = await _userManager1.FindByEmailAsync(request.Email);
            if (userExists != null)
                throw new CustomException("Admin already exists");

            var user = new ApplicationUser
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Email
            };

            var createUserResult = await _userManager1.CreateAsync(user, request.Password);
            if (!createUserResult.Succeeded)
                throw new ValidationException($"Create admin failed {createUserResult?.Errors?.First()?.Description}");


            if (!await _roleManager.RoleExistsAsync(IdentityRoles.ADMIN))
                await _roleManager.CreateAsync(new ApplicationRole(IdentityRoles.ADMIN));

            await _userManager1.AddToRoleAsync(user, IdentityRoles.ADMIN);


            return new RegisterResponse { Success = true, Message = "User registered successfully" };
        }
        catch (CustomException ex)
        {
            return new RegisterResponse { Success = false, Message = ex.Message };
        }
        catch (ValidationException ex)
        {
            return new RegisterResponse { Success = false, Message = ex.Message };
        }
        catch (Exception ex)
        {
            return new RegisterResponse { Success = false, Message = ex.Message };
        }
    }

    public async Task<RegisterResponse> RegisterSuperAdminAsync(RegistrationRequest request)
    {
        try
        {
            var userExists = await _userManager1.FindByEmailAsync(request.Email);
            if (userExists != null)
                throw new CustomException("SuperAdmin already exists");

            var user = new ApplicationUser
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Email
            };

            var createUserResult = await _userManager1.CreateAsync(user, request.Password);
            if (!createUserResult.Succeeded)
                throw new ValidationException($"Create SuperAdmin failed {createUserResult?.Errors?.First()?.Description}");


            if (!await _roleManager.RoleExistsAsync(IdentityRoles.SUPER_ADMIN))
                await _roleManager.CreateAsync(new ApplicationRole(IdentityRoles.SUPER_ADMIN));

            await _userManager1.AddToRoleAsync(user, IdentityRoles.SUPER_ADMIN);


            return new RegisterResponse { Success = true, Message = "SuperAdmin registered successfully" };
        }
        catch (CustomException ex)
        {
            return new RegisterResponse { Success = false, Message = ex.Message };
        }
        catch (ValidationException ex)
        {
            return new RegisterResponse { Success = false, Message = ex.Message };
        }
        catch (Exception ex)
        {
            return new RegisterResponse { Success = false, Message = ex.Message };
        }
    }

    public async Task<IEnumerable<TeacherWithFansRequest>> GetAllTeachersWithFanAsync()
    {
        var teachers = await _userManager.GetUsersInRoleAsync("Teacher"); ;

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
