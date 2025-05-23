using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TscLibCore.DB;

namespace MVC_Demo2
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment _hostEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = _hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        private IHostEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            const string syskind = "AA";
            ConnectionStrings cs = ConnectionStrings.CreateInstance(syskind);

            TscLibCore.Startup.ConfigureServices(
              services,
              new TscLibCore.ProjEnvironments()
              {
                  sysKind = syskind,
                  outterServiceColl = services,
                  outterConfig = Configuration,
                  hostEnvironment = HostingEnvironment,
                  isTW = true,
                  shouldRecordWebServiceLog = false, //¼È¤£¼g¤JWSDB
           webServiceLogDbName = "WSDB"
              });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            TscLibCore.Startup.Configure(app);

            app.UseEndpoints(endpoints =>
            {
                if (env.IsDevelopment())
                {
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                );

                }

                endpoints.MapControllerRoute(
                  name: "MVC_Demo_Route",
                  pattern: "MVCWeb/{controller=Home}/{action=Index}"
                );
            });
        }
    }
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
   
    
}
