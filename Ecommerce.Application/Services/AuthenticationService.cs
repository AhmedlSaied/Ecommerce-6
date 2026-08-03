using Ecommerce.Application.common.Results;
using Ecommerce.Application.contracts;
using Ecommerce.Application.Dtos.AutenticationDtos;

namespace Ecommerce.Application.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtToken _jwtToken;

        public AuthenticationService( IIdentityService identityService,IJwtToken jwtToken)
        {
            _identityService = identityService;
            _jwtToken = jwtToken;
        }

        public async Task<Result<bool>> EmailIsExist(string email, CancellationToken ct)
        {
            var result = await _identityService.EmailIsExist(email, ct);
            return Result<bool>.Success(result.Value);
        }

        public async Task<Result<UserDtos>> GetCurrentUser(string email, CancellationToken ct = default)
        {
            var user = await _identityService.FindUserByEmail(email, ct);
            var roles = _identityService.GetUserRolesByEmailAsync(user.Value.Email);
            var rolesResult = roles.Result.Value;
            var token = _jwtToken.CreateToken(user.Value.Email, user.Value.UserName, user.Value.Id, rolesResult);

            return Result<UserDtos>.Success(new UserDtos()
            {
                Email = email,
                DisplayName = user.Value.DisplayName,
                Token = token
            });

        }

        public async Task<Result<AddressDto>> GetUserAddress(string email, CancellationToken ct = default)
        {
            var userAddress=await _identityService.GetUserAddress(email, ct);
            return userAddress;
        }

        public async Task<Result<UserDtos>> Login(LoginDto loginDto, CancellationToken ct = default)
        {
            var user = await _identityService.FindUserByEmail(loginDto.Email, ct);

            if (!user.IsSuccess)
            {
                return Result<UserDtos>.Failure(user.Error);
            }
            var password=await _identityService.CheckUserCorect(loginDto.Email,loginDto.Password,ct);
            if(!password.IsSuccess)
                return Result<UserDtos>.Failure(password.Error);
            if (!password.Value)
            {
                return Result<UserDtos>.Failure(new Error("401", "Email OR Password is not Correct", ErrorType.Unauthorized));
            }
            var roles = _identityService.GetUserRolesByEmailAsync(user.Value.Email);
            var rolesResult=roles.Result.Value;
            var token = _jwtToken.CreateToken(user.Value.Email, user.Value.UserName, user.Value.Id, rolesResult);

            return Result<UserDtos>.Success(new UserDtos()
            {
                Email = loginDto.Email,
                DisplayName = user.Value.DisplayName,
                Token = token
            });

        }

        public async Task<Result<UserDtos>> Register(SignUpDtos signUpDtos, CancellationToken ct = default)
        {
           var user = await _identityService.CreateUser(signUpDtos, ct);
            if (!user.IsSuccess)
            {
                return Result<UserDtos>.Failure(user.Error);
            }
            //var roles = _identityService.GetUserRolesByEmailAsync(signUpDtos.Email);
            //var rolesResult = roles.Result.Value;
            var token = _jwtToken.CreateToken(user.Value.Email, user.Value.UserName, user.Value.Id, Array.Empty<string>());

            return Result<UserDtos>.Success(new UserDtos()
            {
                Email = signUpDtos.Email,
                DisplayName = user.Value.DisplayName,
                Token = token
            });



        }

        public async Task<Result<AddressDto>> UpSertUserAddress(string email, AddressDto addressDto, CancellationToken ct = default)
        {
            return await _identityService.UpSertUserAddress(email, addressDto, ct);
        }
    }
}
