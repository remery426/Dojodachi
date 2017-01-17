using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace Dojodachi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSession();
            app.UseMvc();
        }
    }
}