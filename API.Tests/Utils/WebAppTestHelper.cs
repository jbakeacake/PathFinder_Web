using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API.Tests.Utils
{
    public static class WebAppTestHelper
    {
        public static void ResetWebAppDataContext(CustomWebApplicationFactory factory)
        {
            using (var scope = factory.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var context = sp.GetRequiredService<DataContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                try
                {
                    Seed.SeedData(context);
                    context.SaveChanges();
                } catch (Exception ex)
                {
                    sp.GetRequiredService<ILogger<CustomWebApplicationFactory>>()
                        .LogError(ex, "An error occured while trying to reset the in memory database");
                }
            }
        }
    }
}