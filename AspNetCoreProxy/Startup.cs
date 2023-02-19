using Microsoft.Identity.Web;
using System.IdentityModel.Tokens.Jwt;

namespace AspNetCoreProxy;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        // IdentityModelEventSource.ShowPII = true;
        // JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        services.AddMicrosoftIdentityWebApiAuthentication(Configuration);

        services.AddReverseProxy()
            .LoadFromConfig(Configuration.GetSection("ReverseProxy"));
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapReverseProxy();
        });
    }
}