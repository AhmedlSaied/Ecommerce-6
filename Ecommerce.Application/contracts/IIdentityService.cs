using Ecommerce.Application.common;
using Ecommerce.Application.common.Results;
using Ecommerce.Application.Dtos.AutenticationDtos;


namespace Ecommerce.Application.contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindUserByEmail(string email, CancellationToken ct);
        Task<Result<bool>> CheckUserCorect(string email, string password,CancellationToken ct);
        Task<Result<IReadOnlyList<string>>> GetUserRolesByEmailAsync(string email);

        Task<Result<IdentityUserResult>> CreateUser(SignUpDtos signUpDtos, CancellationToken ct = default);
        Task<Result<bool>> EmailIsExist(string email, CancellationToken ct);
        Task<Result<AddressDto>> GetUserAddress(string email, CancellationToken ct);
        Task<Result<AddressDto>> UpSertUserAddress(string email,AddressDto addressDto, CancellationToken ct);

    }
}
