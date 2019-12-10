using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Models;
using Repository;
using Models.ViewModels;

namespace UI.Controllers
{
    public class OrganisationItemController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private OrganisationItemDTO OrgItemDTO = new OrganisationItemDTO(); 
        public OrganisationItemController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }
       

        // GET: OrganisationItem
        public async Task<IActionResult> Index(Guid id )
        {
            UnitofWork _uofw = new UnitofWork(_context, _userManager.GetUserAsync(User).Result);
            var result = _uofw.Organisations.GetOrganisationbyIdandItems(id);

            return View(OrgItemDTO.ToIndexModel(result));
        }

        // GET: OrganisationItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _context.OrganisationItems.FirstOrDefaultAsync(m => m.ID == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(OrgItemDTO.ToViewModel(result));
        }

        // GET: OrganisationItem/Create
        public IActionResult Create(string Id)
        {
            OrganisationItemViewModel Item = new OrganisationItemViewModel();
            Item.OrganisationId = Id;
            Item.Brand = "";
            Item.Code = "";
            Item.Description = "";
            Item.Name = "";
            Item.Price = "";
            Item.RowVersionNo = "";
            Item.TaxRate = "";            
            return View(Item);
        }

        // POST: OrganisationItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Brand,Code,Description,Price,TaxRate,ID,RowVersionNo,OrganisationId")] OrganisationItemViewModel Item)
        {
            if (ModelState.IsValid)
            {               
                Item.Id = Guid.NewGuid().ToString();
                var result =   OrgItemDTO.ToTableModel(Item);           
                result.TblOrganisationId = Guid.Parse(Item.OrganisationId);
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = Item.OrganisationId });
            }
            return View(Item);
        }

        // GET: OrganisationItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblOrganisationItem = await _context.OrganisationItems.FindAsync(id);
            if (tblOrganisationItem == null)
            {
                return NotFound();
            }
            var result =  OrgItemDTO.ToViewModel(tblOrganisationItem);
            return View(result);
        }

        // POST: OrganisationItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Brand,Code,Description,Price,TaxRate,RowVersionNo,OrganisationId")] OrganisationItemViewModel Item)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = _context.OrganisationItems.Find(id);
                    OrgItemDTO.ToTableModel(result, Item);
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {                    
                   return NotFound();
                }
                return RedirectToAction(nameof(Index), new {id = Item.OrganisationId });
            }
            return View(Item);
        }

        // GET: OrganisationItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.OrganisationItems.FirstOrDefaultAsync(m => m.ID == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(OrgItemDTO.ToViewModel(result));
        }

        // POST: OrganisationItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _context.OrganisationItems.FindAsync(id);
            _context.OrganisationItems.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = result.TblOrganisationId });
        }

        private bool TblOrganisationItemExists(Guid id)
        {
            return _context.OrganisationItems.Any(e => e.ID == id);
        }
    }
}
