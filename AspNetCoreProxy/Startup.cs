using Microsoft.Identity.Web;
using Microsoft.IdentityModel.JsonWebTokens;

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
        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
        // IdentityModelEventSource.ShowPII = true;

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