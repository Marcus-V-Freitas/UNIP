using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UnipDLL.DAL.Database.SQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnipDLL.DAL.Repositories.Contracts;
using UnipDLL.BLL.Libraries.Email;
using UnipDLL.BLL.Libraries.Login;
using UnipDLL.BLL.Libraries.Sessao;
using UnipDLL.DAL.Database.SQL.Data;
using UnipDLL.DAL.Repositories;
using UnipDLL.BLL.Libraries.Middleware;

namespace Unip
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
            //Padrão Repository e acesso a sessão (Incluir)
            services.AddHttpContextAccessor();

            //Singletons dos repositórios e suas devidas interfaces
          
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<IPeriodoRepository, PeriodoRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            /* 
             * SMTP - Serviço Envio Emails (Incluir)
             */

            //Singleton SmtpClient - Servidor do Gmail
            services.AddScoped(options => {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:PasswordLocal")),
                    EnableSsl = true
                };
                return smtp;
            });

            //DI simples - Gerenciar Email
            services.AddScoped<GerenciarEmail>();

            //Funcionar os cookies (Incluir)
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Definir um tempo limite curto para evitar problemas relacionados ao desempenho. 
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                // Tornar o "cookie de sessão" essencial
                options.Cookie.IsEssential = true;
            });


            services.Configure<CookiePolicyOptions>(options =>
            {
                //Essa instrução lambda determina se o consentimento do usuário para os "cookies não essenciais" é necessário para uma solicitação.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Session - Configuração
            /*
             * Guardar os dados na memoria
             */
            services.AddMemoryCache(); //Usado para que a Sessão funcione (Incluir)
            services.AddSession(options => { }); //Usado para que a Sessão funcione (Incluir)

            //services.AddSession(options =>
            //{
            //    options.Cookie.IsEssential = true;
            //});

           
            services.AddScoped<SeedingService>(); //Adiciona Classe para popular base de dados
            services.AddScoped<Sessao>(); //Adicionar Sessão
            services.AddScoped<LoginCliente>(); //Adicionar Login Cliente
            services.AddScoped<LoginColaborador>(); //Adicionar Login Colaborador
            services.AddMvc().AddSessionStateTempDataProvider();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); //Indica que a compatibilidade do padrão é .NET CORE 2.1

            //Instalar pacote Microsoft.EntityFrameworkCore.Tools Version 2.1.1 (SQL SERVER) /String de Conexão com SQL Server

           // string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UnipCadastro;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            string connection = "Data Source=SQL5047.site4now.net;Initial Catalog=DB_A4EDFE_Unip;User Id=DB_A4EDFE_Unip_admin;Password=Vaders2233;";

            //DI do Context da aplicação (EntityFrameworkCore) - Definição da Pasta de Migrations para WebFrotas
            services.AddDbContext<UnipContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly(nameof(Unip))), ServiceLifetime.Transient);
        }

        // Este método é chamado em tempo de execução para configurar o pipeline de solicitação HTTP.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService) //Aplica ao depurar o código
        {
            // Middlewares do Software

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            if (env.IsDevelopment()) //Habilita mensagem de error ambiente de desenvolvimento
            {
                app.UseDeveloperExceptionPage();
                seedingService.seed(); //Popula a base de dados com os dados padrões
            }
            else
            {
               // app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}"); //Redireciona para qualquer página de erro que use HTTP (lado Cliente)
                app.UseExceptionHandler("/Error/Error500"); //Redireciona para página de erro do lado do servidor
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSession(); //Métodos usado para fazer funcionar a Sessão (Incluir)
            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>(); //AntiForgeryToken - Evitar ataques maliciosos CSRF por método Post (Valido para toda a aplicação)
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                //Rota adicionada para Area do Colaborador e Cliente (Tem a prioridade de chamada)
                routes.MapRoute(
                      name: "areas",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
