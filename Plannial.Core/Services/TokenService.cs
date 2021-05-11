﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Services
{
    public class TokenService : ITokenService
    {
        //key will never leave our server
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            //get a string of text using its key then convert it into a byte array
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)                                                                                                                                               
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
            };

            //validation credentials, algorithm to use for securing the creds
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //building the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            }
        }    
}
