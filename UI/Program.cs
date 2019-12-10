using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //using (var context = new ApplicationDbContext()) {
            //    context.Database.Migrate();
            //}

            try
            {
                CreateWebHostBuilder(args).Build().MigrateDatabase().Run();

            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error : " + Ex.Message);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

    }

       
}
