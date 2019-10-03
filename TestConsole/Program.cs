using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;

using System.Threading.Tasks;
using TestConsole;
using System.Collections.Generic;
using Repository;

namespace TestConsole
{
    public interface IUserCreationService
    {
        Task CreateUser(ApplicationUser Usr, string password);
        ApplicationUser GetUser(string email);
    }
    public class UserCreationService : IUserCreationService
    {
        public readonly UserManager<ApplicationUser> userManager;

        public UserCreationService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task CreateUser(ApplicationUser Usr, string password)
        {
            // var user = new ApplicationUser { UserName = "TestUser", Email = "test@example.com" };
            var user = Usr;
            var result = await this.userManager.CreateAsync(Usr, password);

            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
            else
            {
                Console.WriteLine("Created User.");
            }
        }

        public ApplicationUser GetUser(string email)
        {
            return this.userManager.FindByNameAsync(email).Result;
        }
    }


    public class Orgdata
    {
        public TblOrganisation NewOrg(ApplicationUser AU)
        {
            TblOrganisation ToReturn = new TblOrganisation();

            ToReturn.ApplicationUser = AU;
            ToReturn.Address = "This is a new Org address";
            ToReturn.Contact = " Mr AN Other1";
            ToReturn.ContactEmail = "another@mail.com";
            ToReturn.ContactNo = "01010 010100101010";
            ToReturn.Name = "New Org one X";
            return ToReturn;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database=aspnet-PO002; Trusted_Connection = True; MultipleActiveResultSets = true");
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();
            services.AddScoped<IUserCreationService, UserCreationService>();

            var provider = services.BuildServiceProvider();

            var userService = provider.GetService<IUserCreationService>();

            Console.WriteLine("Hello World!");
            var usr = userService.GetUser("TestUser@mail.com");
            Console.WriteLine("User Id :" + usr.Id.ToString());
            UnitofWork UofW = new UnitofWork(provider.GetService<ApplicationDbContext>(), usr);
            var Org =  UofW.Organisations.GetOrganisationbyIdandItems(Guid.Parse("67D22630-6D7E-4918-5DF5-08D71FE19A19"));

            foreach (var I in Org.Items)
            {
                Console.WriteLine("Code is " + I.Code);

            }

            //TblOrganisationItem OrgItem = new TblOrganisationItem()
            //{
            //    Brand = "Ajax",
            //    Code = "a001",
            //    Description = "this is bleach",
            //    Name = "2L bleach",
            //    Price = "1.00",
            //    TaxRate = "0.20"
            //};

            //TblOrganisationItem OrgItem1 = new TblOrganisationItem()
            //{
            //    Brand = "Ajax",
            //    Code = "a002",
            //    Description = "this is bleach",
            //    Name = "4L bleach",
            //    Price = "1.00",
            //    TaxRate = "0.20"
            //};


            //TblOrganisationItem OrgItem2 = new TblOrganisationItem()
            //{
            //    Brand = "Ajax",
            //    Code = "a003",
            //    Description = "this is bleach",
            //    Name = "10L bleach",
            //    Price = "1.00",
            //    TaxRate = "0.20"
            //};


            //List<TblOrganisationItem> Items = new List<TblOrganisationItem>();
            //Items.Add(OrgItem);
            //Items.Add(OrgItem1);
            //Items.Add(OrgItem2);
            //Org.Items = Items;
            //UofW.Organisations.AddRecord(Org);
            //UofW.Complete();
            Console.WriteLine("Press any key to exit---");
            Console.ReadKey();
        }
    }
}

