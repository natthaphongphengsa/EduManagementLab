using EduManagementLab.Core.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace EduManagementLab.Core.Configuration
{
    public static class DependencyInjectionExtensions
    {
        public static IIdentityServerBuilder AddLtiJwtBearerClientAuthentication(this IIdentityServerBuilder builder)
        {
            builder.AddSecretValidator<PrivatePemKeyJwtSecretValidator>();
            return builder;
        }
    }
}
