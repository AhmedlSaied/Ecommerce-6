using Ecommerce.Application.common.Results;
using Ecommerce.Application.Dtos.AutenticationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.contracts
{
    public interface IAuthenticationService
    {
        Task<Result<UserDtos>> Login(LoginDto loginDto,CancellationToken ct=default);
        Task<Result<UserDtos>> Register(SignUpDtos signUpDtos,CancellationToken ct=default);
        Task<Result<bool>> EmailIsExist(string email, CancellationToken ct=default);
        Task<Result<UserDtos>> GetCurrentUser(string email, CancellationToken ct = default);
        Task<Result<AddressDto>> GetUserAddress(string email, CancellationToken ct = default);
        Task<Result<AddressDto>> UpSertUserAddress(string email, AddressDto addressDto, CancellationToken ct=default);

    }
}
