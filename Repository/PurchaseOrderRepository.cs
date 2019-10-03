using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Repository
{
    public interface IPurchaseOrderRepository : IRepository<TblPurchaseOrder>
    {
        IEnumerable<TblPurchaseOrder> GetAllPurchaseOrders();
        IEnumerable<TblPurchaseOrder> GetAllPurchaseOrders(string OrderBy, string Order);
        string GeneratePurchaseOrderCode();
        TblPurchaseOrder GetPurchaseOrderbyId(Guid Id);
        TblPurchaseOrder GetPurchaseOrderbyIdIncludeItems(Guid Id);
        TblPurchaseOrder GetPurchaseOrderbyCode(string Code);
        TblPurchaseOrder RaisePurchaseOrder(); 
    }

    public class PurchaseOrderRepository : Repository<TblPurchaseOrder>, IPurchaseOrderRepository
    {
        private ApplicationDbContext _DbContext;
        private new ApplicationUser _applicationUser;
        public PurchaseOrderRepository(ApplicationDbContext Context, ApplicationUser applicationUser) : base(Context, applicationUser)
        {
            _DbContext = Context;
            _applicationUser = applicationUser;
        }
        public string GeneratePurchaseOrderCode()
        {
            string ToReturn; 
            string lookup = "POCode_" + _applicationUser.Id.ToString();
            // check entry has been set in the lookup table

            var check = _DbContext.Lookups.Where(X => X.Lookup == lookup).SingleOrDefault();
            if (check == null)
            {
                // set the code
                TblLookup NewPOCode = new TblLookup();
                NewPOCode.Lookup = lookup;
                NewPOCode.Value = "000";
                NewPOCode.Note = "";
                // Update database
                ToReturn = "PO_A000";
                _DbContext.Lookups.Add(NewPOCode);
            }
            else
            {
                // inc code by 1
                var code = int.Parse(check.Value);
                code++;
                check.Value = code.ToString("000");
                _DbContext.Lookups.Update(check);
                ToReturn = "PO_A" + check.Value;
            }

            _DbContext.SaveChanges();

            return ToReturn;
        }
        public IEnumerable<TblPurchaseOrder> GetAllPurchaseOrders()
        {
            return FindRecords(x => x.ApplicationUser.Id == _applicationUser.Id);
        }
        public IEnumerable<TblPurchaseOrder> GetAllPurchaseOrders(string OrderBy, string Order)
        {
            switch (OrderBy)
            {
                case "Code":
                    switch (Order)
                    {
                        case "Ascending":
                            return _DbContext.PurchaseOrders.Include(a => a.ApplicationUser).OrderBy(o => o.Code).ToList();
                        case "Descending":
                            return _DbContext.PurchaseOrders.Include(a => a.ApplicationUser).OrderByDescending(o => o.Code).ToList();
                        default:
                            return _DbContext.PurchaseOrders.Include(a => a.ApplicationUser).OrderBy(o => o.Code).ToList();
                    }
                case "Status":
                    switch (Order)
                    {
                        case "Ascending":
                            return _DbContext.PurchaseOrders.Include(a => a.ApplicationUser).OrderBy(o => o.Status).ToList();
                        case "Descending":
                            return _DbContext.PurchaseOrders.Include(a => a.ApplicationUser).OrderByDescending(o => o.Status).ToList();
                        default:
                            return _DbContext.PurchaseOrders.Include(a => a.ApplicationUser).OrderBy(o => o.Status).ToList();
                    }
                case "DateRaised":
                    switch (Order)
                    {
                        case "Ascending":
                            return _DbContext.PurchaseOrders.Include(a => a.ApplicationUser).OrderBy(o => o.DateRaised).ToList();
                        case "Descending":
                            return _DbContext.PurchaseOrders.Include(a => a.ApplicationUser).OrderByDescending(o => o.DateRaised).ToList();
                        default:
                            return _DbContext.PurchaseOrders.Include(a => a.ApplicationUser).OrderBy(o => o.DateRaised).ToList();
                    }
                default:
                    return _DbContext.PurchaseOrders.Include(a => a.ApplicationUser).ToList();
            }
        }
        public TblPurchaseOrder GetPurchaseOrderbyCode(string Code)
        {
            //var result = FindRecord(x => x.ApplicationUser.Id == _applicationUser.Id && x.Code == Code);
            var result = _DbContext.PurchaseOrders                                   
                                   .Where(x => x.ApplicationUser.Id == _applicationUser.Id && x.Code == Code)
                                   .Include(x => x.Items)
                                   .Single();
            //_DbContext.Entry(result).Collection(c => c.Items).Load();
            return result;
        }
        public TblPurchaseOrder GetPurchaseOrderbyId(Guid Id)
        {            
            return GetRecord(Id);
        }
        public TblPurchaseOrder GetPurchaseOrderbyIdIncludeItems(Guid Id)
        {
             var result = _DbContext.PurchaseOrders.Find(Id);
             _DbContext.Entry(result).Collection(c => c.Items).Load();
            return result;
        }
        public TblPurchaseOrder RaisePurchaseOrder()
        {
            TblPurchaseOrder ToReturn = new TblPurchaseOrder();
            // create PO stub 
            ToReturn.ID = Guid.NewGuid();
            ToReturn.ApplicationUserId = _applicationUser.Id;
            ToReturn.ApplicationUser = _applicationUser;
            ToReturn.Code = GeneratePurchaseOrderCode();
            ToReturn.Status = "Open";
            ToReturn.DateRaised = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
            ToReturn.DateRequired = "ASAP";
            ToReturn.To = "";
            

            // put data stub in 

            //List<TblPurchaseOrderItem> TestItems = new List<TblPurchaseOrderItem>();
            //var TestItem = new TblPurchaseOrderItem()
            //{
            //    Brand = "brand test",
            //    Code = "Code test",
            //    Description = "description test",
            //    Name = "Name test",
            //    Price = "10.00",
            //    Quantity = "1",
            //    Total = "12.00",
            //    Tax = "2.00",
            //    TaxCode = "0.20"
            //};

            //var TestItem1 = new TblPurchaseOrderItem()
            //{
            //    Brand = "brand test",
            //    Code = "Code test 1",
            //    Description = "description test",
            //    Name = "Name test",
            //    Price = "10.00",
            //    Quantity = "1",
            //    Total = "12.00",
            //    Tax = "2.00",
            //    TaxCode = "0.20"
            //};

            //TestItems.Add(TestItem);
            //TestItems.Add(TestItem1);

            //ToReturn.Items = TestItems;

            _DbContext.PurchaseOrders.Add(ToReturn);
            try
            {
                _DbContext.SaveChanges();
            }
            catch (DbUpdateException EX)
            {
                Debug.WriteLine(EX.InnerException.Message); 
                ToReturn = null;
            }
            return ToReturn;
        }       
    }
}
