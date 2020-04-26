using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using System.Threading.Tasks;
using xainport.Models;
using Xainport.Documents;
using Xainport.Models;
using Xainport.Services;

namespace xainport
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<EthereumNetworkConnectionOptions>(options =>
                Configuration.Bind(nameof(EthereumNetworkConnectionOptions), options));

            CosmosClient cosmosClient = InitializeCosmosClient(Configuration.GetSection("CosmosDb"));

            services.AddSingleton<ICosmosDbService<CitizenAccount>>(InitializeCitizenAccountCosmosClientAsync(
                    Configuration.GetSection("CosmosDb"), cosmosClient).GetAwaiter().GetResult());
            services.AddSingleton<ICitizenAccountRepository, CitizenAccountRepository>();

            services.AddSingleton<ICosmosDbService<IssuingAuthority>>(InitializeIssuingAuthorityCosmosClientAsync(
                    Configuration.GetSection("CosmosDb"), cosmosClient).GetAwaiter().GetResult());
            services.AddSingleton<IIssuingAuthorityRepository, IssuingAuthorityRepository>();

            services.AddControllersWithViews();

            //services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            //          .AddSignIn("AzureAd", Configuration, options => Configuration.Bind("AzureAd", options));

            //services.AddControllersWithViews(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //});
        }

        public static CosmosClient InitializeCosmosClient(IConfigurationSection configurationSection)
        {
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;

            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(account, key);
            CosmosClient client = clientBuilder
                                .WithConnectionModeDirect()
                                .Build();

            return client;
        }

        public static async Task<CosmosDbService<IssuingAuthority>> InitializeIssuingAuthorityCosmosClientAsync(IConfigurationSection configurationSection, CosmosClient cosmosClient)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("IssuingAuthorityContainerName").Value;

            CosmosDbService<IssuingAuthority> cosmosDbService = new CosmosDbService<IssuingAuthority>(cosmosClient, databaseName, containerName);
            DatabaseResponse database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/publicAddress");

            //InitializeIssuingAuthorityData(cosmosDbService);

            return cosmosDbService;
        }

        public static async Task<CosmosDbService<CitizenAccount>> InitializeCitizenAccountCosmosClientAsync(IConfigurationSection configurationSection, CosmosClient cosmosClient)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("CitizenAccountContainerName").Value;

            CosmosDbService<CitizenAccount> cosmosDbService = new CosmosDbService<CitizenAccount>(cosmosClient, databaseName, containerName);
            DatabaseResponse database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/publicAddress");

            //InitializeCitizenAccountData(cosmosDbService);

            return cosmosDbService;
        }

        public static void InitializeIssuingAuthorityData(ICosmosDbService<IssuingAuthority> cosmosDbService)
        {
            IssuingAuthority issuingAuthority1 = new IssuingAuthority
            {
                Id = "0x6Bd701A0D24b7c83cCe83989f6c8021e84bb60Ca",
                IssuingCountry = "Spain",
                Name = "Spanish National Health Organization",
                PublicAddress = "0x6Bd701A0D24b7c83cCe83989f6c8021e84bb60Ca"
            };

            IssuingAuthority issuingAuthority2 = new IssuingAuthority
            {
                Id = "0x726a73323FE176221311185034ED1b87EE2d7dfd",
                IssuingCountry = "Norway",
                Name = "Norwegian Institute of Public Health",
                PublicAddress = "0x726a73323FE176221311185034ED1b87EE2d7dfd"
            };

            IssuingAuthority issuingAuthority3 = new IssuingAuthority
            {
                Id = "0xf8E149A98eB1d0b30F3E4dB9474296aB5822Bb91",
                IssuingCountry = "England",
                Name = "English Institute of Public Health",
                PublicAddress = "0xf8E149A98eB1d0b30F3E4dB9474296aB5822Bb91"
            };

            IssuingAuthority issuingAuthority4 = new IssuingAuthority
            {
                Id = "0x4D48A90c9ad0fDb1F67A37A86901FBecbC2848AC",
                IssuingCountry = "Croatia",
                Name = "Croatian Natioanl Health Institution",
                PublicAddress = "0x4D48A90c9ad0fDb1F67A37A86901FBecbC2848AC"
            };

            cosmosDbService.AddItemAsync(issuingAuthority1);
            cosmosDbService.AddItemAsync(issuingAuthority2);
            cosmosDbService.AddItemAsync(issuingAuthority3);
            cosmosDbService.AddItemAsync(issuingAuthority4);
        }

        public static void InitializeCitizenAccountData(ICosmosDbService<CitizenAccount> cosmosDbService)
        {
            CitizenAccount citizenAccount1 = new CitizenAccount
            {
                CitizenAttestationsContractAddress = "",
                PublicAddress = "0x4ea01d9793cab9c9Cc0F14acDc317F157Df1617d",
                Name = "John",
                Id = "0x4ea01d9793cab9c9Cc0F14acDc317F157Df1617d"
            };

            CitizenAccount citizenAccount2 = new CitizenAccount
            {
                CitizenAttestationsContractAddress = "",
                PublicAddress = "0x16b35386d3e2d365c5b341431dA8431451b2De51",
                Name = "Mark",
                Id = "0x16b35386d3e2d365c5b341431dA8431451b2De51"
            };

            CitizenAccount citizenAccount3 = new CitizenAccount
            {
                CitizenAttestationsContractAddress = "",
                PublicAddress = "0x5b5064775005511225d79Fe6B8c4E1C62323d00B",
                Name = "Sarah",
                Id = "0x5b5064775005511225d79Fe6B8c4E1C62323d00B"
            };

            cosmosDbService.AddItemAsync(citizenAccount1);
            cosmosDbService.AddItemAsync(citizenAccount2);
            cosmosDbService.AddItemAsync(citizenAccount3);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
