using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.JsonModels;
using Models.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UI.Controllers;

namespace UI
{
    [Authorize]
    public class PurchaseOrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly Secrets _appSecrets;


        private PurchaseOrderDTO PODTO = new PurchaseOrderDTO();
        public PurchaseOrderController(ApplicationDbContext context,
                                       UserManager<ApplicationUser> userManager,
                                       Secrets AppSecrets )
        {
            _context = context;
            _userManager = userManager;
            _appSecrets = AppSecrets; 

        }

        // GET: PurchaseOrder
        public IActionResult Index()
        {
            UnitofWork _uofw = new UnitofWork(_context, _userManager.GetUserAsync(User).Result);  
            var result = _uofw.PurchaseOrders.GetAllPurchaseOrders();           
            return View(PODTO.ToIndexModel(result,"Code","Ascending"));
        }

        public IActionResult POIndex()
        {
            UnitofWork _uofw = new UnitofWork(_context, _userManager.GetUserAsync(User).Result);
            var result = _uofw.PurchaseOrders.GetAllPurchaseOrders();
            return View("Index",PODTO.ToIndexModel(result, "Code", "Ascending"));
        }

        public IActionResult IndexSorted(string OrderBy,string Order)
        {
            if (Order == "Ascending") { Order = "Descending"; } else { Order = "Ascending"; };
           
            UnitofWork _uofw = new UnitofWork(_context, _userManager.GetUserAsync(User).Result);
            var result = _uofw.PurchaseOrders.GetAllPurchaseOrders(OrderBy, Order);
            return View("Index",PODTO.ToIndexModel(result,OrderBy,Order));
        }
        // GET: PurchaseOrder/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _context.PurchaseOrders.Include(t => t.ApplicationUser).Include(x => x.Items).FirstOrDefaultAsync(m => m.ID == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(PODTO.ToViewModel(result));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Display(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _context.PurchaseOrders.Include(t => t.ApplicationUser).Include(x => x.Items).FirstOrDefaultAsync(m => m.ID == id);
            if (result == null)
            {
                return NotFound();
            }
            return View("Details",PODTO.ToViewModel(result));
        }


        // GET: PurchaseOrder/Details/5
        [HttpGet, ActionName("Status")]
        public async Task<IActionResult> Status(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _context.PurchaseOrders.Include(t => t.ApplicationUser).Include(x => x.Items).FirstOrDefaultAsync(m => m.ID == id);
            List<TblLookup> StatusList = await _context.Lookups.Where(x => x.Lookup == "POStatus").ToListAsync();

            if (StatusList.Count == 0 || StatusList == null)
            {
                // Lookup table has not been populated with the codes so enter them here
                // An initial set that can be altered later.
                StatusList = new List<TblLookup>()
                {
                    new TblLookup(){ Lookup="POStatus", Value="Open", Note="PO open for modification" },
                    new TblLookup(){ Lookup="POStatus", Value="Sent", Note="PO emailed out to recipient" },
                    new TblLookup(){ Lookup="POStatus", Value="Complete", Note="PO all items ordered recieved" },
                    new TblLookup(){ Lookup="POStatus", Value="PartComplete", Note="PO some of items ordered recieved" },
                    new TblLookup(){ Lookup="POStatus", Value="Cancelled", Note="PO Cancelled" }
                }; 
                try
                {
                    _context.Lookups.AddRange(StatusList);
                    _context.SaveChanges();
                }
                catch
                {
                    return NotFound();
                }
            }
            if (result == null)
            {
                return NotFound();
            }
            return View(PODTO.ToViewModel(result,StatusList));
        }


        [HttpPost, ActionName("Status")]
        public async Task<IActionResult> NewStatus([Bind("Id,RowVersionNo")] string Id, string NewStatus)
        {
            // get the PO 
            var result = await _context.PurchaseOrders.FindAsync(Guid.Parse(Id));
            result.Status = NewStatus;
            _context.SaveChanges();
            return  RedirectToAction("Index");
        }

        // GET: PurchaseOrder/Create
        public IActionResult Create()
        {            
            UnitofWork _uofw = new UnitofWork(_context,_userManager.GetUserAsync(User).Result );
            var ViewModel = PODTO.ToViewModel(
                                               _uofw.PurchaseOrders.RaisePurchaseOrder(),
                                               _uofw.Lookups.GetLookuplist("BudgetCode"),
                                               _uofw.Organisations.GetOrganisations(),
                                               null 
                                             );
            return View(ViewModel);
        }

        // POST: PurchaseOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Status,Budget,DateRaised,DateFullfilled,DateRequired,Note,To,DeliverTo,InvoiceTo,Price,Tax,Total,ApplicationUserId,ID,RowVersionNo")] TblPurchaseOrder tblPurchaseOrder)
        {
            if (ModelState.IsValid)
            {
                tblPurchaseOrder.ID = Guid.NewGuid();
                _context.Add(tblPurchaseOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", tblPurchaseOrder.ApplicationUserId);
            return View(tblPurchaseOrder);
        }

        // GET: PurchaseOrder/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            UnitofWork _uofw = new UnitofWork(_context, _userManager.GetUserAsync(User).Result);
            var PO = _uofw.PurchaseOrders.GetPurchaseOrderbyIdIncludeItems(id);
            Guid guidOutput = new Guid();
            try
            {
                guidOutput = Guid.Parse(PO.To);
            }
            catch
            {
                guidOutput = Guid.NewGuid();
            }

            var ViewModel = PODTO.ToViewModel(
                                               PO,
                                               _uofw.Lookups.GetLookuplist("BudgetCode"),
                                               _uofw.Organisations.GetOrganisations(),
                                               _uofw.Organisations.GetOrganisationbyIdandItems(guidOutput) );

            if (ViewModel.Status == "Open")
            {                                
                return View(ViewModel);
            }
            else
            {
                return View("Details",ViewModel);
            }
        }

        // POST: PurchaseOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Code,Status,Budget,DateRaised,DateFullfilled,DateRequired,Note,To,DeliverTo,InvoiceTo,Price,Tax,Total,ApplicationUserId,ID,RowVersionNo")] TblPurchaseOrder tblPurchaseOrder)
        {
            if (id != tblPurchaseOrder.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPurchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPurchaseOrderExists(tblPurchaseOrder.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", tblPurchaseOrder.ApplicationUserId);
            return View(tblPurchaseOrder);
        }


        private bool TblPurchaseOrderExists(Guid id)
        {
            return _context.PurchaseOrders.Any(e => e.ID == id);
        }

        [HttpGet, ActionName("GetOrganisationItems")]
        public string GetOrganisationItems(string Organisationid)
        {
            UnitofWork _uofw = new UnitofWork(_context, _userManager.GetUserAsync(User).Result);
            var result = _uofw.Organisations.GetOrganisationbyIdandItems(Guid.Parse(Organisationid));
            // convert result to a JSON object to return to the browser. 
            var ToReturn = JsonConvert.SerializeObject(result.Items);
            return ToReturn;
        }

        [HttpGet, ActionName("GetItemFromDatabase")]
        public string GetItemFromDatabase(string Itemid)
        {          
            var result = _context.OrganisationItems.Where(x => x.ID.ToString() == Itemid).SingleOrDefault();
            string ToReturn = "";
            if (result != null)
            {
                ToReturn = JsonConvert.SerializeObject(result);
            }
            else
            {
                ToReturn = "Not Found";
            }
            return ToReturn;
        }

        [HttpGet, ActionName("SaveItem")]
        public string SaveItem(string Code,
                               string Description,
                               string Name,
                               string Price,
                               string Brand,
                               string TaxCode,
                               string Organisationid)
        {

            // save the Organisation details to the database
            string OpperationStatus = "";
            TblOrganisationItem Item = new TblOrganisationItem();
            Item.ID = Guid.NewGuid();
            Item.TblOrganisationId = Guid.Parse(Organisationid);
            Item.Code = Code;
            Item.Description = Description;
            Item.Name = Name;
            Item.Brand = Brand;
            Item.Price = Price;
            Item.TaxRate = TaxCode;
            _context.OrganisationItems.Add(Item);
            try
            {
                _context.SaveChanges();
                OpperationStatus = Item.ID.ToString();
            }
            catch
            {
                OpperationStatus = "Failure";
            }
            return OpperationStatus;
        }


        [HttpPost, ActionName("SavePO")]
        //public string SavePO([FromQuery]string PurchaseOrder)
        public string SavePO([FromBody]JObject PurchaseOrder)
        {
            string ToReturn = " processing PO  ";
            UnitofWork _uofw = new UnitofWork(_context, _userManager.GetUserAsync(User).Result);
            var result = JsonConvert.DeserializeObject<PurchaseOrderJsonModel>(PurchaseOrder.ToString());
            //// get PO from database 
            var PO = _uofw.PurchaseOrders.GetPurchaseOrderbyCode(result.Code);
            //// update it with the new details 
            PODTO.UpdatePO(PO, result);
            //// send to the database
             _uofw.Complete();
            // report result.
            return ToReturn;
        }

        [HttpGet, ActionName("SaveOrganisation")]
        public string SaveOrganisation(string Organisation,
                                string Email,
                                string Contact,
                                string line1,
                                string line2,
                                string line3,
                                string line4,
                                string line5,
                                string Code)
        {

            // save the Organisation details to the database
            string OpperationStatus = "";
            var user = _userManager.GetUserAsync(User).Result;
            TblOrganisation     Org = new TblOrganisation();
            Org.ID = Guid.NewGuid();
            Org.ApplicationUserId = user.Id;
            Org.Name = Organisation;
            Org.ContactEmail = Email;
            Org.Contact = Contact;
            Org.Address = line1 + "\n" + line2 + "\n" + line3 + "\n" + line4 + "\n" + line5 + "\n" + Code;

            _context.Organisations.Add(Org);
            try
            {
                _context.SaveChanges();
                OpperationStatus = Org.ID.ToString();
            }
            catch
            {
                OpperationStatus = "Failure";
            }
            return OpperationStatus;
        }






        [HttpGet, ActionName("EmailPO")]
        public async  Task<IActionResult> EmailAsync(Guid? id)
        {
            var PO = _context.PurchaseOrders.Where(x => x.ID == id).Include(i => i.Items).Single();
            // convert the PO into view Model
            var ToView = PODTO.ToViewModel(PO, _userManager.GetUserAsync(User).Result);  
            var result = this.RenderViewAsync<PurchaseOrderViewModel>("Email", ToView, true);

            StreamWriter sw = new StreamWriter("C:\\Users\\Carl\\Source\\Repos\\test.html");
            sw.Write(result.Result);
            sw.Close();

            var apiKey = _appSecrets.SendGridKey;
            var client = new SendGridClient(apiKey);
            var From = new EmailAddress("41tp41@gmail.com", "Me");
            var To = new EmailAddress("cljoseph@yahoo.co.uk", "Me");

            var Subject = "test send from sendgrid";
            var plainTextcontent = " this is a test message";
            var HtmlContent = result.Result;
            var msg = MailHelper.CreateSingleEmail(From, To, Subject, plainTextcontent, HtmlContent);
            var response = await client.SendEmailAsync(msg);


            // set the status as beeing sent
            PO.Status = "Sent";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}

