using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Stx.DataServices.Hrm;
using Stx.Shared.Interfaces;
using Stx.Shared.Interfaces.CRM;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Services;
using Stx.Web.JP.IdentityServices;
using Stx.Web.JP.Services.CRM;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Stx.Web.JP
{
    public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			hostEnvironment = env;
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment hostEnvironment { get; }

		/// <summary>
		/// ConfigureServices
		/// </summary>
		/// <remarks>
		/// In this project:
		/// * All the authentications & authorizations hav been removed
		/// * To integrate identiryServer
		/// * API has no authentications & authorizations 
		/// </remarks>	
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{		
			//Temp Disabled Auths
			//services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation()
				.AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Candidate");
                    options.Conventions.AuthorizeFolder("/Corporate");
                    options.Conventions.AllowAnonymousToFolder("/Jobs");
                    options.Conventions.AllowAnonymousToPage("/Index");
                    options.Conventions.AllowAnonymousToPage("/Jobs/Search");
                });


			//services.AddAuthentication();
			//services.AddAuthentication(options =>
			//{
			//	options.DefaultScheme = "Cookies";
			//	options.DefaultChallengeScheme = "oidc";
			//})
			//	.AddCookie("Cookies")
			//	.AddOpenIdConnect("oidc", options =>
			//	{
			//		options.Authority = "http://localhost:5001xxxxxxxxxx";
			//		options.ClientId = "xxxx";
			//		options.ClientSecret = "xxxxxxxx";
			//		options.ResponseType = "xxxxxxxxxxxx";
			//		options.SaveTokens = true;
			//		options.Scope.Add("xxxxxxxxxxxxxxxx");
			//		if (hostEnvironment.IsDevelopment())
			//		{
			//			options.RequireHttpsMetadata = false;
			//		}
			//	});

			JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
			services.AddAuthentication(options =>
			{
				options.DefaultScheme = "Cookies";
				options.DefaultChallengeScheme = "oidc";
			})
			.AddCookie(options =>
			{
				options.Events.OnValidatePrincipal = Middleware.CookieValidater.ValidateAsync;
			})
			.AddOpenIdConnect("oidc", options =>
			{
                options.Authority = "https://localhost:5001/";
                options.ClientId = "jp_web";
                options.ClientSecret = "secret";

				options.ResponseType = "code id_token";
				//options.ResponseType = "id_token";
				
				//options.Scope.Add("api1");
				//options.Scope.Add("candid");
				//options.Scope.Add("userid");
				options.Scope.Add("usr");
				options.Scope.Add("offline_access");
				options.Scope.Add("hrm.jp.api");
				

				//options.Authority = Configuration["InteractiveServiceSettings:AuthorityUrl"];
				//options.ClientId = Configuration["InteractiveServiceSettings:ClientId"];
				//options.ClientSecret = Configuration["InteractiveServiceSettings:ClientSecret"];
				//options.ResponseType = "code";
				////options.ResponseMode = "query";
				options.UsePkce = false;
				//// options.CallbackPath = "/signin-oidc"; // default redirect URI
				//// options.Scope.Add("oidc"); // default scope
				//// options.Scope.Add("profile"); // default scope

				//options.Scope.Add(Configuration["InteractiveServiceSettings:Scopes:0"]);
				options.SaveTokens = true;
				if (hostEnvironment.IsDevelopment())
                {
                    //options.RequireHttpsMetadata = false;
                }
            });

			services.Configure<CookieAuthenticationOptions>(options =>
			{
				options.LoginPath = new PathString("/Home/Index");
			});

			#region IDENTITY SERVER
			services.AddTransient<HttpRequestHandler>();
			services.Configure<IdentityServerSettings>(Configuration.GetSection("IdentityServerSettings"));
			services.AddSingleton<ITokenService, TokenService>();
			#endregion

			#region Service HTTP Client
			//services.AddHttpClient("JPAPI", c =>
			//{
			//	c.BaseAddress = new Uri(Configuration.GetConnectionString("api"));
			//});

			services.AddHttpClient("JPAPI", c =>
				{
					c.BaseAddress = new Uri(Configuration.GetConnectionString("api"));
				}).AddHttpMessageHandler((s) => s.GetService<HttpRequestHandler>());
			#endregion

			#region System Services
			services.AddHttpContextAccessor();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
			#endregion

			#region Domain Services
			Uri apiUrl = new Uri(Configuration.GetConnectionString("api"));
            //services.AddHttpClient<IApiHelperDataService, ApiHelperDataService>(client => { client.BaseAddress = apiUrl; });
            services.AddScoped<IApiHelperDataService, ApiHelperDataService>();
            services.AddScoped<ICandidateDataService, CandidateDataService>();
            services.AddScoped<ICandidateProfileDataService, CandidateProfileDataService>();
            services.AddScoped<ICandidateSignupDataService, CandidateSignupDataService>();

            services.AddScoped<IJobOrderPreviewDataService, JobOrderPreviewDataService>();
            services.AddScoped<IJobSearchDataService, JobSearchDataService>();
            services.AddScoped<IJobSendoutDataService, JobSendoutDataService>();

            services.AddScoped<ICorporatePublicDataService, CorporatePublicDataService>();
            services.AddScoped<IHrmGeneralDataService, HrmGeneralDataService>();

            #endregion
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			//app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
            app.UseAuthentication();
			app.UseAuthorization();

			app.UseBrowserLink();

			//Middlewares
			//app.UseMiddleware<JwtMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages()
					.RequireAuthorization(); //Temp Disable Auths
			});
		}
	}
}
