using Akka.Actor;
using Akka.Configuration;
using  A=Akka.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Shop.Comments;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.AspNetCore;
using Utility.AspNetCore.Extensions;
using Utility.AspNetCore.Filter;
using Utility.Config;
using Utility.Domain.Uow;
using Utility.Ef;
using Utility.Ef.Uow;
using Akka.Routing;

namespace Shop.Akka.Comment.Api
{
    public class Startup
    {
        public static ActorSystem ActorSystem { get; private set; }
        public static IActorRef RouterActor { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //迁移时必须 声明 数据库 不然容易出错
            DbConfig.Flag = DbFlag.MySql;
            //注册微服务 
            services.AddRegisterService(Configuration, ServiceConfig.Flag);

            Utility.AspNetCore.Extensions.ServiceCollectionExtensions.AddApiVersioning(services);

            services.AddFilter()
            //全局配置Json序列化处理 方案1
            .AddJson()
          .SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddControllers().AddControllersAsServices();
            services.AddSwagger<EmptySwaggerOperationFilter>("V1", "Shop.Comment.Api");
            string sqlConnectionString = Configuration.GetConnectionString($"{DbConfig.Flag}ConnectionString");
            
            //迁移时候使用
            //string key=Configuration["ConsulKey"];
            //string url = Configuration["ConsulUrl"];
            //ConfigManager.ConsulAddress = url;
           // sqlConnectionString = ConfigManager.GetByConsul($"ShopComment/{DbConfig.Flag}ConnectionString");

            services.UseEf<CommentDbContent>(sqlConnectionString); //abp 起冲突 多数据库 sqlite sqlserver mysql oracle postgre //直接 dbcontext 直接 声明数据库使用类型

            services.AddScoped<IUnitWork, EfUnitWork>(it=>new EfUnitWork(it.GetService<CommentDbContent>()));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop.Akka.Comment.Api", Version = "v1" });
            });

            // set up a simple service we're going to hash
            services.AddScoped<ICommentService, CommentServiceImpl>();

            // creates instance of IPublicHashingService that can be accessed by ASP.NET
            services.AddSingleton<IPublicCommentService, PublicHashingServiceImpl>();

            //IHostedService 只能启动一个
            //多个演员 难道 定义多个 ?
            // starts the IHostedService, which creates the ActorSystem and actors
            //services.AddHostedService<PublicHashingServiceImpl>(sp => (PublicHashingServiceImpl)sp.GetRequiredService<IPublicCommentService>());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CS0618 // 类型或成员已过时
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.Extensions.Hosting.IApplicationLifetime lifetime)
#pragma warning restore CS0618 // 类型或成员已过时
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop.Akka.Comment.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            StartHelper.ApplicationStarted = lifetime.ApplicationStarted;
            StartHelper.ApplicationStopped = lifetime.ApplicationStopped;
            StartHelper.ApplicationStopping = lifetime.ApplicationStopping;
            lifetime.ApplicationStarted.Register(()=> {
                var hocon = ConfigurationFactory.ParseString(File.ReadAllText("app.conf"));
                var bootstrap = BootstrapSetup.Create().WithConfig(hocon);
                var di = A.ServiceProviderSetup.Create(app.ApplicationServices);
                var actorSystemSetup = bootstrap.And(di);
                Startup.ActorSystem = ActorSystem.Create("AspNetDemo", actorSystemSetup);
                // </AkkaServiceSetup>

                // <ServiceProviderFor>
                // props created via IServiceProvider dependency injection
                var hasherProps = A.ServiceProvider.For(Startup.ActorSystem).Props<CommentActor>();
                Startup.RouterActor = Startup.ActorSystem.ActorOf(hasherProps.WithRouter(FromConfig.Instance), "hasher");
                // </ServiceProviderFor>
            });

            lifetime.ApplicationStopped.Register(() => {

                // theoretically, shouldn't even need this - will be invoked automatically via CLR exit hook
                // but it's good practice to actually terminate IHostedServices when ASP.NET asks you to
                 CoordinatedShutdown.Get(Startup.ActorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance).Wait();
            });
            app.UseService(Configuration, ServiceConfig.Flag);
            app.Use(env, "Shop.Akka.Comment.Api");
        }
    }
}
