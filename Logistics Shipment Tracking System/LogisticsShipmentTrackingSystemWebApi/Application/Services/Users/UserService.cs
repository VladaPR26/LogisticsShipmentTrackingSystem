using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Contracts;
using Application.Services.Users.Requests;
using Application.Services.Users.Responses;
using Microsoft.Extensions.Configuration;

using DataBase.Repositories.Contracts;

using Domain.Models.Entities;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Users;
public class UserService:IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    public UserService(IUserRepository userRepository,IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public LoginResponse Login(LoginRequest request)
    {
        User user = _userRepository.GetUserByUsername(request.Username);

        if(user == null)
        {
            throw new ArgumentException("User with this email does not exists");
        }
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            throw new ArgumentException("Wrong password");
        }
        var token = GenerateToken(user, _configuration);

        LoginResponse response = new LoginResponse
        {
            Token = token
        };
        return response;
    }

    private string GenerateToken(User user, IConfiguration configuration)
    {
        //Generate token that is valid for 5 days
        var tokenHandler = new JwtSecurityTokenHandler();

        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
                new Claim("user_id", user.Id.ToString()),
                new Claim("user_username", user.Username),
                };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: userClaims,
            signingCredentials: credentials,
            expires: DateTime.Now.AddDays(5)
        );

        return tokenHandler.WriteToken(token);
    }
}
