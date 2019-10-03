using DataAccess;

using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{

    public interface IUnitofWork :IDisposable
    {
        IPurchaseOrderRepository PurchaseOrders { get; }
        IOrganisationRepository Organisations { get; }
        ILookupRepository Lookups { get; }
        int Complete();
        string TestResult();
    }

    public class UnitofWork :IUnitofWork
    {
        private readonly ApplicationDbContext _DbContext;
        private ApplicationUser _applicationUser;
        public UnitofWork(ApplicationDbContext DbContext, ApplicationUser User)
        {
            _DbContext = DbContext;
            _applicationUser = User;
            PurchaseOrders = new PurchaseOrderRepository(DbContext,User);
            Organisations = new OrganisationRepository(DbContext,User);
            Lookups = new LookupRepository(DbContext,User);
        }
        public IPurchaseOrderRepository PurchaseOrders { get; private set; }
        public IOrganisationRepository Organisations { get; private set; }
        public ILookupRepository Lookups { get; private set; }
       

        public int Complete()
        {
           return  _DbContext.SaveChanges();
        }
        public void Dispose()
        {
            _DbContext.Dispose();
        }
        public string TestResult()
        {
            return "this is a test";
        }
    }
}
