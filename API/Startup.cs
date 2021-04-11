using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using API.Controllers;
using API.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MongoCRUD db = new MongoCRUD();
            //db.Insert(new USER_model { EMAIL = "asd", BIRTH = new DateTime(), COUNTRY = "", USERNAME = "asd" });
            var user_list = db.getList<USER_model>();

            var user = db.getRecord<USER_model>(new Guid("172a92d3-c063-44e7-9fdc-ad3c067d418a"));
            user.USERNAME = "Doctor PDH Salambasis";
            user.EMAIL = "XDDDDDDDDDDDDD@XD.com";
            user.BIRTH = new DateTime(1666, 6, 13, 0, 0, 0, DateTimeKind.Local);
            db.Upsert<USER_model>(user.ID, user);

            var res = TVcontroller.getTrendingMovies(TMDbLib.Objects.Trending.TimeWindow.Week);
            var r = TVcontroller.searchMedia("game of thrones", new TVcontroller.SearchMediaOptions { type = TMDbLib.Objects.General.MediaType.Tv });
        }
    }
}
