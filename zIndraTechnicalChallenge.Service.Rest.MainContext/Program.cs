using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using zIndraTechnicalChallenge.Service.Rest.MainContext;

CreateWebHostBuilder(args).Build().Run();

static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
