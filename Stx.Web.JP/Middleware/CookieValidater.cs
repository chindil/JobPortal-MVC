using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stx.Web.JP.Middleware
{
    public static  class CookieValidater
    {
        public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            const string accessTokenName = "access_token";
            const string refreshTokenName = "refresh_token";
            const string expirationTokenName = "expires_at";

            if (context.Principal.Identity.IsAuthenticated)
            {
                //var expiresUtc = context.Properties.ExpiresUtc;
                var expireat = context.Properties.GetTokenValue(expirationTokenName);
                if (!string.IsNullOrWhiteSpace(expireat))
                {
                    var now = DateTimeOffset.UtcNow;
                    var timeElapsed = now.Subtract(context.Properties.IssuedUtc.Value);
                    var timeRemaining = context.Properties.ExpiresUtc.Value.Subtract(now);
                    var expiresUtc = DateTime.Parse(expireat, CultureInfo.InvariantCulture).ToUniversalTime();

                    if (expiresUtc < now)
                    //if (timeElapsed > timeRemaining)
                    {
                        // If we don't have the refresh token, then check if this client has set the
                        // "AllowOfflineAccess" property set in Identity Server and if we have requested
                        // the "OpenIdConnectScope.OfflineAccess" scope when requesting an access token.
                        var refreshToken = context.Properties.GetTokenValue(refreshTokenName);
                        if (refreshToken == null)
                        {
                            context.RejectPrincipal();
                            return;
                        }

                        var cancellationToken = context.HttpContext.RequestAborted;

                        // Obtain the OpenIdConnect options that have been registered with the "AddOpenIdConnect" call.
                        // Make sure we get the same scheme that has been passed to the "AddOpenIdConnect" call.
                        //
                        // TODO: Cache the token client options
                        // The OpenId Connect configuration will not change, unless there has
                        // been a change to the client's settings. In that case, it is a good
                        // idea not to refresh and make sure the user does re-authenticate.
                        var serviceProvider = context.HttpContext.RequestServices;
                        //var openIdConnectOptions = serviceProvider.GetRequiredService<IOptionsSnapshot<OpenIdConnectOptions>>().Get(OpenIdConnectScheme);
                        //var configuration = openIdConnectOptions.Configuration ?? await openIdConnectOptions.ConfigurationManager.GetConfigurationAsync(cancellationToken).ConfigureAwait(false);

                        // Set the proper token client options
                        var tokenClientOptions = new TokenClientOptions
                        {
                            Address = "https://localhost:5001/connect/token",
                            ClientId = "stxwebjp",
                            ClientSecret = "long_secret_for_WebJP"
                        };

                        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                        using var httpClient = httpClientFactory.CreateClient();

                        var tokenClient = new TokenClient(httpClient, tokenClientOptions);
                        var tokenResponse = await tokenClient.RequestRefreshTokenAsync(refreshToken, cancellationToken: cancellationToken).ConfigureAwait(false);
                        if (tokenResponse.IsError)
                        {
                            context.RejectPrincipal();
                            return;
                        }

                        // Update the tokens
                        var expirationValue = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn).ToString("o", CultureInfo.InvariantCulture);
                        context.Properties.StoreTokens(new[]
                        {
                            new AuthenticationToken { Name = refreshTokenName, Value = tokenResponse.RefreshToken },
                            new AuthenticationToken { Name = accessTokenName, Value = tokenResponse.AccessToken },
                            new AuthenticationToken { Name = expirationTokenName, Value = expirationValue }
                        });

                        // Update the cookie with the new tokens
                        context.ShouldRenew = true;
                    }
                }
            }
        }
    }
}
