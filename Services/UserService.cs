using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SupplyChain.Entities;
using SupplyChain.Helpers;
using SupplyChain.Interfaces.Repository;
using SupplyChain.Interfaces.Services;
using SupplyChain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow;

        public UserService(IOptions<AppSettings> appSettings, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            try
            {
                var user = await _uow.UserRepository.AuthenticateAsync(model.Username, model.Password);
                // return null if user not found
                if (user == null) return null;

                // authentication successful so generate jwt token
                var token = generateJwtToken(user);

                return new AuthenticateResponse(user, token);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<int> Register(RegisterRequest model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                var res = _uow.UserRepository.Add(user);

                await _uow.CompleteAsync();
                return user.Id;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _uow.UserRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<User> GetById(int id)
        {
            try
            {
                return await _uow.UserRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.Username)
            };
            var token = new JwtSecurityToken(_appSettings.Issuer,
                          _appSettings.Issuer,
                          claims,
                          expires: DateTime.Now.AddMinutes(30),
                          signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
