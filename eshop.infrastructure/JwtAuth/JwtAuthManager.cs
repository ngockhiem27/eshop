﻿using eshop.core.JwtSettings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eshop.infrastructure.JwtAuth
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshTokens;
        private readonly Byte[] _secret;
        public IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary => _userRefreshTokens.ToImmutableDictionary();

        public JwtAuthManager(JwtTokenConfig jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _userRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
        }

        public JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                shouldAddAudienceClaim ? _jwtTokenConfig.Issuer : string.Empty,
                claims,
                expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = new RefreshToken
            {
                UserName = username,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)
            };
            _userRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            try
            {
                var principal = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secret),
                    ValidAudience = _jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                }, out var validatedToken);
                return (principal, validatedToken as JwtSecurityToken);
            }
            catch (Exception)
            {
                throw new SecurityTokenException("Invalid Token");
            }
        }

        public JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now)
        {
            try
            {
                var (principal, jwtToken) = DecodeJwtToken(accessToken);
                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                {
                    throw new SecurityTokenException("Invalid Token");
                }
                var username = principal.Identity.Name;
                if (!_userRefreshTokens.TryGetValue(refreshToken, out var existingRefreshToken))
                {
                    throw new SecurityTokenException("Invalid Token");
                }
                if (existingRefreshToken.UserName != username || existingRefreshToken.ExpireAt < now)
                {
                    throw new SecurityTokenException("Invalid Token");
                }
                return GenerateTokens(username, principal.Claims.ToArray(), now);
            }
            catch (SecurityTokenException e)
            {
                throw e;
            }
        }

        public void RemoveExpiredRefreshTokens(DateTime now)
        {
            var expiredTokens = UsersRefreshTokensReadOnlyDictionary.Where(x => x.Value.ExpireAt < now).ToList();
            foreach (var item in expiredTokens)
            {
                _userRefreshTokens.TryRemove(item.Key, out _);
            }
        }

        public void RemoveRefreshTokenByUserName(string userName)
        {
            var tokens = UsersRefreshTokensReadOnlyDictionary.Where(x => x.Value.UserName == userName).ToList();
            foreach (var item in tokens)
            {
                _userRefreshTokens.TryRemove(item.Key, out _);
            }
        }

        public string GetRefreshTokenWithUserName(string userName)
        {
            var tokens = UsersRefreshTokensReadOnlyDictionary.Where(x => x.Value.UserName == userName).FirstOrDefault();
            if (tokens.Value != null)
            {
                return tokens.Value.TokenString;
            }
            return String.Empty;
        }
    }
}
