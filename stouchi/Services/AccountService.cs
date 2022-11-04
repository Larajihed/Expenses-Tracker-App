using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using stouchi.Context;
using stouchi.Dtos;
using stouchi.Models;
using stouchi.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace stouchi.Services
{
    public class AccountService : IAccountService
    {

        private readonly ApplicationDbContext _myAuthContext;
        private readonly TokenSettings _tokenSettings;


        public AccountService(ApplicationDbContext myAuthContext,
        IOptions<TokenSettings> tokenSettings)
        {
            _myAuthContext = myAuthContext;
            _tokenSettings = tokenSettings.Value;
        }


		//Function to create JWT Token & return the token
		private string CreateJwtToken(User user)
		{
			var symmetricSecurityKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_tokenSettings.SecretKey)
			);
			var credentials = new SigningCredentials(
				symmetricSecurityKey,
				SecurityAlgorithms.HmacSha256
			);

			var userCliams = new Claim[]{
			new Claim("email", user.Email),
	};

			var jwtToken = new JwtSecurityToken(
				issuer: _tokenSettings.Issuer,
				expires: DateTime.Now.AddMinutes(20),
				signingCredentials: credentials,
				claims: userCliams,
				audience: _tokenSettings.Audience
			);

			string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
			return token;
		}


		public async Task<TokenDto> GetAuthTokens(LoginDto login)
		{

			User user = await _myAuthContext.Users
			.Where(_ => _.Email.ToLower() == login.Email.ToLower() &&
			_.Password == login.Password).FirstOrDefaultAsync();

			if (user != null)
			{
				var accessToken = CreateJwtToken(user);
				var refreshToken = CreateRefreshToken();
				await InsertRefreshToken(user.UserId, refreshToken);
				return new TokenDto
				{
					AccessToken = accessToken,
					RefreshToken = refreshToken
				};
			}
			return null;
		}
		private string CreateRefreshToken()
		{
			var tokenBytes = RandomNumberGenerator.GetBytes(64);
			var refreshtoken = Convert.ToBase64String(tokenBytes);

			var tokenIsInUser = _myAuthContext.RefreshToken
			.Any(_ => _.Token == refreshtoken);

			if (tokenIsInUser)
			{
				return CreateRefreshToken();
			}
			return refreshtoken;
		}

		private async Task InsertRefreshToken(int userId, string refreshtoken)
		{
			var newRefreshToken = new RefreshToken
			{
				UserId = userId,
				Token = refreshtoken,
				ExpirationDate = DateTime.Now.AddDays(7)
			};
			_myAuthContext.RefreshToken.Add(newRefreshToken);
			await _myAuthContext.SaveChangesAsync();
		}

		public async Task<TokenDto> RenewTokens(RefreshTokenDto refreshToken)
		{
			var userRefreshToken = await _myAuthContext.RefreshToken
			.Where(_ => _.Token == refreshToken.Token
			&& _.ExpirationDate >= DateTime.Now).FirstOrDefaultAsync();

			if (userRefreshToken == null)
			{
				return null;
			}

			var user = await _myAuthContext.Users
			.Where(_ => _.UserId == userRefreshToken.UserId).FirstOrDefaultAsync();



			var newJwtToken = CreateJwtToken(user);
			var newRefreshToken = CreateRefreshToken();

			userRefreshToken.Token = newRefreshToken;
			userRefreshToken.ExpirationDate = DateTime.Now.AddDays(7);
			await _myAuthContext.SaveChangesAsync();

			return new TokenDto
			{
				AccessToken = newJwtToken,
				RefreshToken = newRefreshToken
			};
		}
	}
}
