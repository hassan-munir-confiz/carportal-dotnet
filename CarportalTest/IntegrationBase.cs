using carportal;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using carportal.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CarportalTest
{
    public class IntegrationBase
    {

        protected readonly HttpClient httpClient;

        protected IntegrationBase()
        {

            var appFactory = new WebApplicationFactory<Startup>()
                                    .WithWebHostBuilder(builder =>
                                    {
                                        builder.ConfigureServices(services =>
                                          {
                                             var descriptor = services.SingleOrDefault
                                             (d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                                             if (descriptor != null)
                                             {
                                                 services.Remove(descriptor);
                                             }

                                             services.AddDbContext<DataContext>
                                               ((_, context) => context.UseInMemoryDatabase("TestDB"));

                                             var sp = services.BuildServiceProvider();
                                             using (var scope = sp.CreateScope())
                                               {
                                                 var scopedServices = scope.ServiceProvider;
                                                 var db = scopedServices.GetRequiredService<DataContext>();
                                                 db.Database.EnsureCreated();
                                                 try
                                                 {
                                                     _ = InitializeDbForTests(db);
                                                 }
                                                 catch (Exception ex)
                                                 {
                                                     Console.Write(ex.Message);
                                                 }
                                             }
                                         });
                                     });

            httpClient = appFactory.CreateClient();

        }

        private async Task InitializeDbForTests(DataContext dataContext)
        {
            dataContext.AddRange(TestDataProvider.getCars());
            await dataContext.SaveChangesAsync();
        }

    }
}
