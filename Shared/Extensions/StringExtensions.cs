﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class StringValidator
    {
        public static string GerarTokenJWT(this string input, string secret, int expireMinutes = 60)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, input)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static ClaimsPrincipal? ValidarJWT(this string token, string secretKey, out string result)
        {
            result = "";

            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var jwtConvertido = tokenHandler.ValidateToken(token, validationParameters, out _);
                return jwtConvertido;
            }
            catch(System.ArgumentNullException e)
            {
                result = "O Header Authorization deve ser informado.";
                return null;
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenMalformedException ex)
            {
                result = "O Token não é um JWT Válido";
                return null;
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenSignatureKeyNotFoundException ex)
            {
                result = "O Token não condiz com o sistema de origem";
                return null;
            }
            catch (Exception ex)
            {
                result = "Houve um erro na validação to token";
                return null;
            }            
        }

    }

    
}
