using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;

namespace ContactsApp.Backend.Models
{
    public static class PrepDb
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ContactsAppContext>();

            dbContext.Database.Migrate();
        }

    }
}
