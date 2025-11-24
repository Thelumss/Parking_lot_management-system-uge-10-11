using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Parking_lot_management_system_uge_10_11.Data;
using Parking_lot_management_system_uge_10_11.DTO;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Parking_lot_management_system_uge_10_11.Repository
{
    public class AuthService(DataContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<string> LoginAsync(UserDTO request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return null;
            }
            if (new PasswordHasher<Users>().VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return CreateToken(user);
        }

        public async Task<Users> RegisterAsync(UserDTO request)
        {
            if (await context.Users.AnyAsync(u => u.Name == request.Name))
            {
                return null;
            }

            var user = new Users();

            var hashedPassword = new PasswordHasher<Users>().HashPassword(user, request.Password);

            user.Name = request.Name;
            user.Password = hashedPassword;
            user.Email = request.Email;
            user.UserTypeID = request.UserTypeID;
            user.OrganisationId = request.OrganisationId;

            
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }


        private string CreateToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Email.ToString()),
            };
            claims.Add(new Claim("UserID", user.UserID.ToString()));
            claims.Add(new Claim("userTypeID",user.UserTypeID.ToString()));
            claims.Add(new Claim("organisationId", user.OrganisationId.ToString()));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512); // requires a token of 64 chars

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}

