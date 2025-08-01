﻿using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User_API.Models;

namespace JwtAuthenticationManager
{
	public class JwtTokenHandler
	{
		public const string JWT_SECURITY_KEY = "b7F!2vK9x!sQ3mWzP@r8LkDfUe9H#zVp";
		private const int JWT_TOKEN_VALIDITY_MINS = 20;
		private readonly List<User> userAccountList;

		public JwtTokenHandler()
		{
			userAccountList = new List<User>()
			{
				new User { UserName = "admin", Password = "admin", Role = "Administrator" },
				new User { UserName = "user01", Password = "user01", Role = "User" },
			};
		}

		public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
		{
			if (string.IsNullOrEmpty(authenticationRequest.UserName) || string.IsNullOrEmpty(authenticationRequest.Password))
				return null;

			var userAccount = userAccountList.Where(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password).FirstOrDefault();
			if (userAccount == null)
				return null;

			var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
			var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
			var claimsIdentity = new ClaimsIdentity(new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
				new Claim(ClaimTypes.Role, userAccount.Role),
			});

			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(tokenKey),
				SecurityAlgorithms.HmacSha256Signature);

			var securityTokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = claimsIdentity,
				Expires = tokenExpiryTimeStamp,
				SigningCredentials = signingCredentials
			};

			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
			var token = jwtSecurityTokenHandler.WriteToken(securityToken);

			return new AuthenticationResponse
			{
				UserName = userAccount.UserName,
				ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
				JwtToken = token
			};
		}
	}
}
