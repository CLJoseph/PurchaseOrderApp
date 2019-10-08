using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Models.Common;
using Models.JsonModels;
using Models.ViewModels;

namespace Models
{
    // methods to do the data transfer from one model to another
    public class PurchaseOrderDTO
    {
        // takes view model and returns the table model
        public TblPurchaseOrder ToTableModel(PurchaseOrderViewModel Model)
        {
            TblPurchaseOrder ToReturn = new TblPurchaseOrder();
            ToReturn.ApplicationUser.Id = Guid.Parse(Model.ApplicationUserId);
            ToReturn.Budget = Model.Budget;
            ToReturn.Code = Model.Code;
            ToReturn.DateFullfilled = Model.DateFullfilled;
            ToReturn.DateRaised = Model.DateRaised;
            ToReturn.DateRequired = Model.DateRequired;
            ToReturn.DeliverTo = Model.DeliverTo;
            ToReturn.ID = Guid.Parse(Model.Id);
            ToReturn.InvoiceTo = Model.InvoiceTo;
            ToReturn.Note = Model.Note;
            ToReturn.Price = Model.Price;
            ToReturn.RowVersionNo = Model.RowVersionNo;
            ToReturn.Status = Model.Status;
            ToReturn.Tax = Model.Tax;
            ToReturn.To = Model.To;
            ToReturn.Total = Model.Total;
            if (Model.Items.Count > 0)
            {
                foreach (var I in Model.Items)
                {
                    ToReturn.Items.Add(new TblPurchaseOrderItem()
                    {
                        Brand = I.Brand,
                        Code = I.Code,
                        Description = I.Description,
                        ID = Guid.Parse(I.Description),
                        Name = I.Name,
                        Price = I.Price,
                        Quantity = I.Quantity,
                        RowVersionNo = I.RowVersion,
                        Tax = I.Tax,
                        TaxCode = I.TaxCode,
                        Total = I.Total
                    });
                }
            }
            return ToReturn;
        }      
        public PurchaseOrderViewModel ToViewModel(TblPurchaseOrder Model, List<string> BudgetCodes, List<OrgDetail> Organisations, TblOrganisation OrgItems  )
        {
            PurchaseOrderViewModel ToReturn = new PurchaseOrderViewModel();
            ToReturn.ApplicationUserId = Model.ApplicationUser.Id.ToString();
            ToReturn.Budget = Model.Budget;
            ToReturn.BudgetCodes = BudgetCodes;
            ToReturn.Code = Model.Code;
            ToReturn.DateFullfilled = Model.DateFullfilled;
            ToReturn.DateRaised = Model.DateRaised;
            ToReturn.DateRequired = Model.DateRequired;
            ToReturn.DeliverTo = Model.DeliverTo;
            ToReturn.DeliverToDetail = Model.DeliverToDetail;
            ToReturn.DeliverToOptions = Organisations;
            ToReturn.Id = Model.ID.ToString();
            ToReturn.InvoiceTo = Model.InvoiceTo;
            ToReturn.InvoiceToDetail = Model.InvoiceToDetail;
            ToReturn.InvoiceToOptions = Organisations;
            ToReturn.Note = Model.Note;
            ToReturn.Price = Model.Price;
            ToReturn.RowVersionNo = Model.RowVersionNo;
            ToReturn.Status = Model.Status;
            ToReturn.Tax = Model.Tax;
            ToReturn.To = Model.To;
            ToReturn.ToDetail = Model.ToDetail;
            ToReturn.ToOptions = Organisations;
            ToReturn.Total = Model.Total;
            ToReturn.HTMLSelectOrganisationItems.Add(new SelectOrgItem()
            {
                Text = "Select Item",
                Value = "SelectItem",
                Selected = true
            });
            ToReturn.HTMLSelectOrganisationItems.Add(new SelectOrgItem()
            {
                Text = "New Item",
                Value = "NewItem",
                Selected = true
            });

            if (OrgItems != null)
            {
                foreach (var I in OrgItems.Items)
                {
                    ToReturn.HTMLSelectOrganisationItems.Add(new SelectOrgItem()
                    {
                        Text = I.Code,
                        Value = I.ID.ToString(),
                        Selected = false
                    });

                }
            }



            //if (OrgItems != null)
            //{ 
            //    foreach (var I in OrgItems.Items)
            //    {
            //        ToReturn.HTMLSelectOrganisationItems.Add(new SelectOrgItem()
            //        {
            //             Text = I.Name,
            //             Value = I.ID.ToString()
            //        });
            //    }
            //}   
            if (Model.Items != null)
            {

                foreach (var I in Model.Items)
                {
                    ToReturn.Items.Add(new PurchaseOrderItemViewModel()
                    {
                        Brand = I.Brand,
                        Code = I.Code,
                        Description = I.Description,
                        Id = I.ID.ToString(),
                        Name = I.Name,
                        Price = I.Price,
                        Quantity = I.Quantity,
                        RowVersion = I.RowVersionNo,
                        Tax = I.Tax,
                        TaxCode = I.TaxCode,
                        Total = I.Total
                    });
                }
            }
            return ToReturn;
        }
        public PurchaseOrderViewModel ToViewModel(TblPurchaseOrder Model,ApplicationUser AU)
        {
            PurchaseOrderViewModel ToReturn = new PurchaseOrderViewModel();
            if (Model.ApplicationUser == null && Model.ApplicationUserId != null)
            {
                ToReturn.ApplicationUserId = Model.ApplicationUserId.ToString();
            }
            else
            {
                ToReturn.ApplicationUserId = Model.ApplicationUser.Id.ToString();
            }

            ToReturn.FromDetail = AU.PersonName + "<BR />" + AU.Email + "<BR />" + AU.Organisation; 

            ToReturn.Budget = Model.Budget;
            ToReturn.Code = Model.Code;
            ToReturn.DateFullfilled = Model.DateFullfilled;
            ToReturn.DateRaised = Model.DateRaised;
            ToReturn.DateRequired = Model.DateRequired;
            ToReturn.DeliverTo = Model.DeliverTo;
            ToReturn.DeliverToDetail = Model.DeliverToDetail;
            ToReturn.Id = Model.ID.ToString();
            ToReturn.InvoiceTo = Model.InvoiceTo;
            ToReturn.InvoiceToDetail = Model.InvoiceToDetail;
            ToReturn.Note = Model.Note;
            ToReturn.Price = Model.Price;
            ToReturn.RowVersionNo = Model.RowVersionNo;
            ToReturn.Status = Model.Status;
            ToReturn.Tax = Model.Tax;
            ToReturn.To = Model.To;
            ToReturn.ToEmail = Model.ToEmail;
            ToReturn.ToDetail = Model.ToDetail;
            ToReturn.Total = Model.Total;
            if (Model.Items != null)
            {
                foreach (var I in Model.Items)
                {
                    ToReturn.Items.Add(new PurchaseOrderItemViewModel()
                    {
                        Brand = I.Brand,
                        Code = I.Code,
                        Description = I.Description,
                        Id = I.ID.ToString(),
                        Name = I.Name,
                        Price = I.Price,
                        Quantity = I.Quantity,
                        RowVersion = I.RowVersionNo,
                        Tax = I.Tax,
                        TaxCode = I.TaxCode,
                        Total = I.Total
                    });
                }
            }
            return ToReturn;
        }
        public PurchaseOrderViewModel ToViewModel(TblPurchaseOrder Model)
        {
            PurchaseOrderViewModel ToReturn = new PurchaseOrderViewModel();
            if (Model.ApplicationUser == null && Model.ApplicationUserId != null)
            {
                ToReturn.ApplicationUserId = Model.ApplicationUserId.ToString();
            }
            else
            {
                ToReturn.ApplicationUserId = Model.ApplicationUser.Id.ToString();
            }

            ToReturn.Budget = Model.Budget;
            ToReturn.Code = Model.Code;
            ToReturn.DateFullfilled = Model.DateFullfilled;
            ToReturn.DateRaised = Model.DateRaised;
            ToReturn.DateRequired = Model.DateRequired;
            ToReturn.DeliverTo = Model.DeliverTo;
            ToReturn.DeliverToDetail = Model.DeliverToDetail;       
            ToReturn.Id = Model.ID.ToString();
            ToReturn.InvoiceTo = Model.InvoiceTo;
            ToReturn.InvoiceToDetail = Model.InvoiceToDetail;       
            ToReturn.Note = Model.Note;
            ToReturn.Price = Model.Price;
            ToReturn.RowVersionNo = Model.RowVersionNo;
            ToReturn.Status = Model.Status;
            ToReturn.Tax = Model.Tax;
            ToReturn.To = Model.To;
            ToReturn.ToDetail = Model.ToDetail;
            ToReturn.Total = Model.Total;
            if (Model.Items != null)
            {
                foreach (var I in Model.Items)
                {
                    ToReturn.Items.Add(new PurchaseOrderItemViewModel()
                    {
                        Brand = I.Brand,
                        Code = I.Code,
                        Description = I.Description,
                        Id = I.ID.ToString(),
                        Name = I.Name,
                        Price = I.Price,
                        Quantity = I.Quantity,
                        RowVersion = I.RowVersionNo,
                        Tax = I.Tax,
                        TaxCode = I.TaxCode,
                        Total = I.Total
                    });
                }
            }
            return ToReturn;
        }
        public PurchaseOrderStatusViewModel ToViewModel(TblPurchaseOrder Model, List<TblLookup> Status)
        {
            PurchaseOrderStatusViewModel ToReturn = new PurchaseOrderStatusViewModel();

            ToReturn.ApplicationUserId = Model.ApplicationUser.Id.ToString();
            ToReturn.Id = Model.ID.ToString();

            foreach (var S in Status)
            {
                ToReturn.NewStatus.Add(S.Value);
            }

            ToReturn.Budget = Model.Budget;
            ToReturn.Code = Model.Code;
            ToReturn.DateFullfilled = Model.DateFullfilled;
            ToReturn.DateRaised = Model.DateRaised;
            ToReturn.DateRequired = Model.DateRequired;
            ToReturn.InvoiceToDetail = Model.InvoiceToDetail;
            ToReturn.Note = Model.Note;
            ToReturn.Price = Model.Price;           
            ToReturn.Status = Model.Status;
            ToReturn.Tax = Model.Tax;           
            ToReturn.Total = Model.Total;            
            return ToReturn;
        }
        public PurchaseOrderIndexViewModel ToIndexModel(IEnumerable<TblPurchaseOrder> FromDB)
        {
            PurchaseOrderIndexViewModel ToReturn = new PurchaseOrderIndexViewModel();
            ToReturn.Order = "";
            ToReturn.OrderBy = "";
            foreach (var line in FromDB)
            {
                ToReturn.Index.Add(new PurchaseOrderIndexItem()
                {
                    ApplicationUserid = line.ApplicationUserId.ToString(),
                    Code = line.Code,
                    DateRaised = line.DateRaised,
                    DateRequiredBy = line.DateRequired,
                    PurchaseOrderID = line.ID.ToString(),
                    Status = line.Status,
                    Value = line.Total
                });
            }
            return ToReturn;
        }
        public PurchaseOrderIndexViewModel ToIndexModel(IEnumerable<TblPurchaseOrder> FromDB,string OrderBy,string Order)
        {
            PurchaseOrderIndexViewModel ToReturn = new PurchaseOrderIndexViewModel();
            ToReturn.Order = Order;
            ToReturn.OrderBy = OrderBy;
            foreach (var line in FromDB)
            {
                ToReturn.Index.Add(new PurchaseOrderIndexItem()
                {
                    ApplicationUserid = line.ApplicationUserId.ToString(),
                    Code = line.Code,
                    DateRaised = line.DateRaised,
                    DateRequiredBy =line.DateRequired,
                    PurchaseOrderID = line.ID.ToString(),
                    Status = line.Status,
                    Value = line.Total
                });
            }
            return ToReturn;
        }
        public TblPurchaseOrder UpdatePO(TblPurchaseOrder PO, PurchaseOrderJsonModel FromView)
        {
            PO.Budget = FromView.BudgetCode;
            PO.DateRaised = FromView.DateRaised;
            PO.DateRequired = FromView.DateRequiredBy;
            PO.DeliverTo = FromView.DeliverTo;
            PO.InvoiceTo = FromView.InvoiceTo;
            PO.To = FromView.To;
            PO.ToEmail = FromView.ToEmail;
            PO.ToDetail = FromView.ToDetail;
            PO.DeliverTo = FromView.DeliverTo;
            PO.DeliverToDetail = FromView.DeliverToDetail;
            PO.InvoiceTo = FromView.InvoiceTo;
            PO.InvoiceToDetail = FromView.InvoiceToDetail;
            PO.Note = FromView.Note;
            PO.Price = FromView.Price;
            PO.Tax = FromView.Tax;
            PO.Total = FromView.Total;
            // remove items from PO if they have been removed from View
            List<TblPurchaseOrderItem> Toremove = new List<TblPurchaseOrderItem>(); 

            foreach (TblPurchaseOrderItem I in PO.Items)
            {
                var result = FromView.LineItem.Find(x => x.DBLineId == I.ID.ToString());
                if (result == null)
                {
                    
                    Toremove.Add(I);
                }
            }
            foreach (var I in Toremove)
            {
                PO.Items.Remove(I);
            }
            if (PO.Items == null)
            {
                PO.Items = new List<TblPurchaseOrderItem>();
                foreach (var L in FromView.LineItem)
                {
                    PO.Items.Add(new TblPurchaseOrderItem()
                    {
                         
                        Brand = L.brand,
                        Code = L.code,
                        Description = L.description,
                        Name = L.name,
                        Price = L.price,
                        Quantity = L.quantity,
                        Tax = L.tax,
                        TaxCode = L.taxcode,
                        Total = L.total
                    });
                }
            }
            else
            {           
                foreach (var L in FromView.LineItem)
                {
                    TblPurchaseOrderItem result = new TblPurchaseOrderItem();
                    try
                    {
                        var Lineid = Guid.Parse(L.DBLineId);
                        result =  PO.Items.Where(x => x.ID == Guid.Parse(L.DBLineId)).Single();
                    }
                    catch
                    {
                        result = null;
                    }                   
                    if (result != null)
                    {
                        result.Brand = L.brand;
                        result.Code = L.code;
                        result.Description = L.description;
                        result.Name = L.name;
                        result.Price = L.price;
                        result.Quantity = L.quantity;
                        result.Tax = L.tax;
                        result.TaxCode = L.taxcode;
                        result.Total = L.total;
                    }
                    else
                    {
                        PO.Items.Add(new TblPurchaseOrderItem()
                        {
                            Brand = L.brand,
                            Code = L.code,
                            Description = L.description,
                            Name = L.name,
                            Price = L.price,
                            Quantity = L.quantity,
                            Tax = L.tax,
                            TaxCode = L.taxcode,
                            Total = L.total
                        });
                    };
                }
            }
            return PO;
        }
    }


    public class OrganisationsDTO
    {
        public TblOrganisation ToTableModel(OrganisationViewModel Model)
        {
            TblOrganisation ToReturn = new TblOrganisation();
            ToReturn.ID = Guid.Parse(Model.Id);         
            ToReturn.ApplicationUserId = Guid.Parse(Model.ApplicationUserId);
            ToReturn.Name = Model.Name;
            ToReturn.Address = Model.Address;
            ToReturn.Contact = Model.Contact;
            ToReturn.ContactEmail = Model.ContactEmail;
            ToReturn.ContactNo = Model.ContactNo;            
            return ToReturn;
        }

        public void ToTableModel(TblOrganisation ToReturn,  OrganisationViewModel Model)
        {
            ToReturn.Name = Model.Name;
            ToReturn.Address = Model.Address;
            ToReturn.Contact = Model.Contact;
            ToReturn.ContactEmail = Model.ContactEmail;
            ToReturn.ContactNo = Model.ContactNo;
        }


        public OrganisationIndexViewModel ToIndexModel(IEnumerable<TblOrganisation> FromDB)
        {
            OrganisationIndexViewModel  ToReturn = new OrganisationIndexViewModel();

            foreach (var line in FromDB)
            {
                ToReturn.Index.Add(new  OrganisationIndexItemViewModel()
                {
                    Id = line.ID.ToString(),
                    Name = line.Name
                });
            }
            return ToReturn;
        }

        public OrganisationViewModel ToViewModel(TblOrganisation Model)
        {
            OrganisationViewModel ToReturn = new OrganisationViewModel();
            ToReturn.Address = Model.Address;
            ToReturn.Contact = Model.Contact;
            ToReturn.ContactEmail = Model.ContactEmail;
            ToReturn.ContactNo = Model.ContactNo;
            ToReturn.Name = Model.Name;
            return ToReturn;
        }
    }
    public class OrganisationItemDTO
    {
        public TblOrganisationItem ToTableModel(OrganisationItemViewModel Model)
        {
            TblOrganisationItem ToReturn = new TblOrganisationItem();
            ToReturn.ID = Guid.Parse(Model.Id);
            ToReturn.Name = Model.Name;
            ToReturn.Brand = Model.Brand;
            ToReturn.Code = Model.Code;
            ToReturn.Description = Model.Description;
            ToReturn.Price = Model.Price;
            ToReturn.TaxRate = Model.TaxRate;
            return ToReturn;
        }

        public void ToTableModel(TblOrganisationItem ToReturn, OrganisationItemViewModel Model)
        {            
            ToReturn.Name = Model.Name;
            ToReturn.Brand = Model.Brand;
            ToReturn.Code = Model.Code;
            ToReturn.Description = Model.Description;
            ToReturn.Price = Model.Price;
            ToReturn.TaxRate = Model.TaxRate;
        }


        public OrganisationItemIndexViewModel ToIndexModel(TblOrganisation FromDB)
        {
            OrganisationItemIndexViewModel ToReturn = new OrganisationItemIndexViewModel();
            ToReturn.OrganisationId = FromDB.ID.ToString();
            ToReturn.OrganisationName = FromDB.Name;
            foreach (var line in FromDB.Items)
            {
                ToReturn.Index.Add(new OrganisationItemIndexItemViewModel() 
                {
                    Id = line.ID.ToString(),
                    Name = line.Name,
                    Code = line.Code
                });
            }
            return ToReturn;
        }

        public OrganisationItemViewModel ToViewModel(TblOrganisationItem Model)
        {
            OrganisationItemViewModel ToReturn = new OrganisationItemViewModel();
            ToReturn.Brand = Model.Brand;
            ToReturn.Code = Model.Code;
            ToReturn.Description = Model.Description;
            ToReturn.Id = Model.ID.ToString();
            ToReturn.Name = Model.Name;
            ToReturn.OrganisationId = Model.TblOrganisationId.ToString();
            ToReturn.Price = Model.Price;
            ToReturn.RowVersionNo = Model.RowVersionNo.ToString();
            ToReturn.TaxRate = Model.TaxRate;
            return ToReturn;
        }
    }
}
