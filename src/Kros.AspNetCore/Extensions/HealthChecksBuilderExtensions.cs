﻿using Kros.Identity.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Kros.AspNetCore.Extensions
{
    /// <summary>
    /// Health checks builder extension.
    /// </summary>
    public static class HealthChecksBuilderExtensions
    {
        /// <summary>
        /// Adds identity server health check.
        /// </summary>
        /// <param name="healthChecksBuilder">Health check builder</param>
        /// <param name="configuration">Configuration</param>
        /// <returns></returns>
        public static IHealthChecksBuilder AddIdentityServerHealthChecks(this IHealthChecksBuilder healthChecksBuilder, IConfiguration configuration)
        {
            IEnumerable<IdentityServerOptions> identityServerOptions = configuration.GetSection<IdentityServerOptions[]>("IdentityServerHandlers");
            foreach (IdentityServerOptions option in identityServerOptions)
            {
                healthChecksBuilder.AddUrlGroup(
                    new Uri(option.AuthorityUrl),
                    name: string.Format(Properties.Resources.IdentityHealthCheckBuilderName, option.AuthenticationScheme),
                    tags: new string[] { "url" });
            }

            return healthChecksBuilder;
        }
    }
}
