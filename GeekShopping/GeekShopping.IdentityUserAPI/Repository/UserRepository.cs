using AutoMapper;
using GeekShopping.IdentityUserAPI.Domain;
using GeekShopping.IdentityUserAPI.Dto;
using GeekShopping.IdentityUserAPI.Infrastructure.Context;
using GeekShopping.IdentityUserAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GeekShopping.IdentityUserAPI.Repository;

public sealed class UserRepository : IUser
{
    private readonly IConfiguration _configuration;
    private readonly AppUserContext _context;
    private readonly IMapper _mapper;

    public UserRepository(AppUserContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<UserTokenDto> Login(UserLoginDto loginDto)
    {
        var userExists = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == loginDto.Email && x.Password == loginDto.Password);

        if(userExists is null)
        {
            BusinessException.When(true, "Nonexistent user.");
        }

        return GenerateTokenUser(_mapper.Map<UserDto>(userExists));
    }

    public async Task<UserDto> RegisterUser(UserDto userDto)
    {
        var user = new User(userDto.Name, userDto.Email, userDto.Password, userDto.Role);

        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);

    }

    public UserTokenDto GenerateTokenUser(UserDto userDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, userDto.Name.ToString()),
                    new Claim(ClaimTypes.Role, userDto.Role.ToString()),
                    new Claim(ClaimTypes.Email, userDto.Email.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!)), SecurityAlgorithms.HmacSha256),
            Audience = _configuration["Jwt:Audience"],
            Issuer = _configuration["Jwt:Issuer"]
        };
     
        var jsonToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(jsonToken);

        return new UserTokenDto(token, tokenDescriptor.Expires.Value);
    }
}
