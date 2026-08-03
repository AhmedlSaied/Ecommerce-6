using Azure.Core;
using Ecommerce.Application.common;
using Ecommerce.Application.common.Results;
using Ecommerce.Application.contracts;
using Ecommerce.Application.Dtos.AutenticationDtos;
using Ecommerce.infrastructure.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.Repostory
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _user;

        public IdentityService(UserManager<ApplicationUser> user)
        {
            _user = user;
        }
        public async Task<Result<bool>> CheckUserCorect(string email, string password, CancellationToken ct)
        {
            var user = await _user.FindByEmailAsync(email);
            if (user == null)
                return Result<bool>.Failure(new Error("404", "This User Not Exist", ErrorType.NotFound));
            else
                return Result<bool>.Success(await _user.CheckPasswordAsync(user, password));

        }

        public async Task<Result<IdentityUserResult>> CreateUser(SignUpDtos signUpDtos, CancellationToken ct = default)
        {
            var user = await _user.FindByEmailAsync(signUpDtos.Email);
            if(user is not null)
            {
                return Result<IdentityUserResult>.Failure(
                new Error(
                    "409",
                    "A user with this email already exists.",
                    ErrorType.Conflict));
            }
            var newUser = new ApplicationUser()
            {
                Email = signUpDtos.Email,
                DisplayName = signUpDtos.DisplayName,
                UserName = signUpDtos.UserName,
                PhoneNumber = signUpDtos.PhoneNumber,
            };

            var createResult = await _user.CreateAsync(
           newUser,
           signUpDtos.Password);
            var description = string.Join(" | ",   createResult.Errors.Select(error => error.Description));
            if (!createResult.Succeeded)
            {
                return Result<IdentityUserResult>.Failure
                    (
                     new Error(
                            "400",
                   description,
                   ErrorType.Failure)
                    );
            }

            return Result<IdentityUserResult>.Success(
                new IdentityUserResult(newUser.Id, newUser.DisplayName, newUser.Email, newUser.UserName));
        }

        public async Task<Result<bool>> EmailIsExist(string email, CancellationToken ct)
        {
            return Result<bool>.Success(await _user.FindByEmailAsync(email) is not null);
        }

        public async Task<Result<IdentityUserResult>> FindUserByEmail(string email, CancellationToken ct)
        {
            var user =await _user.FindByEmailAsync(email);
            if (user == null)
                return Result<IdentityUserResult>.Failure(new Error("404", "This User Not Exist", ErrorType.NotFound));
            return Result<IdentityUserResult>.Success
                (
                new IdentityUserResult
                    (
                    user.Id,
                    user.DisplayName,
                    user.Email,
                    user.UserName
                    )
                 );
        }

        public async Task<Result<AddressDto>> GetUserAddress(string email, CancellationToken ct)
        {
            var  user = await _user.Users.Include(user=>user.UserAddress).FirstOrDefaultAsync(user=>user.Email == email);
            if (user?.UserAddress is null)
                return Result<AddressDto>.Failure(new Error("404", "Address not found", ErrorType.NotFound));
            return Result<AddressDto>.Success(
               new AddressDto()
               {
                   FirstName = user.UserAddress.FirstName,
                   LastName = user.UserAddress.LastName,
                   City = user.UserAddress.City,
                   Street= user.UserAddress.Street
               });

        }

        public async Task<Result<IReadOnlyList<string>>> GetUserRolesByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<IReadOnlyList<string>>.Failure(
                    new Error(
                        "400",
                        "Email is required.",
                        ErrorType.Validation));
            }

            var user = await _user.FindByEmailAsync(email.Trim());

            if (user is null)
            {
                return Result<IReadOnlyList<string>>.Failure(
                    new Error(
                        "404",
                        "This user does not exist.",
                        ErrorType.NotFound));
            }

            var roles = await _user.GetRolesAsync(user);
            if (roles is null || roles.Count<=0)
            {
                return Result<IReadOnlyList<string>>.Failure(
                    new Error(
                        "404",
                        "This user havenot Roles.",
                        ErrorType.NotFound));
            }

            return Result<IReadOnlyList<string>>.Success(
                roles.ToList());
        }

        public async Task<Result<AddressDto>> UpSertUserAddress(string email, AddressDto addressDto, CancellationToken ct)
        {
            var user = await _user.Users.Include(user => user.UserAddress).FirstOrDefaultAsync(user => user.Email == email);
            if (user?.UserAddress is null)
            {
                user.UserAddress = new Address()
                {
                    FirstName = addressDto.FirstName,
                    LastName = addressDto.LastName,
                    Street = addressDto.Street,
                    City = addressDto.City,
                };

            }
            else
            {
                user.UserAddress.FirstName = addressDto.FirstName;
                user.UserAddress.LastName = addressDto.LastName;
                user.UserAddress.Street = addressDto.Street;
                user.UserAddress.City = addressDto.City;
            }
          var result =await  _user.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var discription = string.Join(",", result.Errors.Select(error => error.Description));
                var ree= result.Errors.First();
                return Result<AddressDto>.Failure(new Error($"{ree.Code}", discription, ErrorType.Failure));

            }
            return Result<AddressDto>.Success(addressDto);
        }
    }
}
