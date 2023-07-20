using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Bussines.Services;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prodex.Bussines.Handlers.Processes;

public class LoginHandler : BaseCreateHandler<LoginModel, TokenModel>
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

    public override async Task<TokenModel> Create(LoginModel form, CancellationToken cancellationToken)
    {
        var user = await context.Users.SingleOrDefaultAsync(p => p.Username == form.Username);

        if (user is null) return null;

        if(!hasher.VerifyHashedPassword(user.Password, form.Password)) return null;

        return new TokenModel
        {
            Token = GenerateToken(user)
        };
    }

    public override Task<TokenModel> Update(long id, LoginModel form, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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
