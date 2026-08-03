using Ecommerce.Application.contracts;
using Ecommerce.Application.Dtos.AutenticationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController    : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDtos>> LoginMethod(LoginDto login)
        {
            return  ToActionResult(await _authenticationService.Login(login));

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDtos>> CreateUser(SignUpDtos signUpDtos)
        {
            return ToActionResult(await _authenticationService.Register(signUpDtos));

        }
        [HttpPost("CheckExistEmail")]
        public async Task<ActionResult<bool>> CheckExistEmail([FromQuery]string email)
        {
            return ToActionResult(await _authenticationService.EmailIsExist(email));

        }
        [Authorize]
        [HttpPost("GetCurrentUser")]
        public async Task<ActionResult<UserDtos>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException("No Email Found");
            return ToActionResult(await _authenticationService.GetCurrentUser(email));

        }
        [Authorize]
        [HttpPost("GetUserAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException("No Email Found");
            return ToActionResult(await _authenticationService.GetUserAddress(email));

        }
        [Authorize]
        [HttpPost("UpdateOrCreateAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress([FromBody] AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException("No Email Found");
            return ToActionResult(await _authenticationService.UpSertUserAddress(email, addressDto));
        }
    }
}
