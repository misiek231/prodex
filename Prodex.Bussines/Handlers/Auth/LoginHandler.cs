using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using OneOf.Types;
using Prodex.Bussines.Services;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prodex.Bussines.Handlers.Auth;

public class Login
{

    public class Request : IRequest<OneOf<TokenModel, Error<string>>>
    {
        public LoginModel LoginModel { get; set; }

        public Request(LoginModel loginModel)
        {
            LoginModel = loginModel;
        }
    }

    public class LoginHandler : IRequestHandler<Request, OneOf<TokenModel, Error<string>>>
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        private readonly PasswordHasher hasher;

        public LoginHandler(DataContext context, IConfiguration configuration, PasswordHasher hasher)
        {
            this.context = context;
            this.configuration = configuration;
            this.hasher = hasher;
        }

        public async Task<OneOf<TokenModel, Error<string>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(p => p.Username == request.LoginModel.Username);

            if (user is null || !hasher.VerifyHashedPassword(user.Password, request.LoginModel.Password)) 
                return new Error<string>("Nieprawidłowy login lub hasło");

            return new TokenModel
            {
                Token = GenerateToken(user)
            };
        }

        private string GenerateToken(User user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Email, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
