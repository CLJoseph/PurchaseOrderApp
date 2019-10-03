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
using Microsoft.AspNetCore.Authorization;
using Models.ViewModels;

namespace UI
{
    [Authorize]
    public class OrganisationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private OrganisationsDTO OrgDTO = new OrganisationsDTO(); 
        public OrganisationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Organisations
        public async Task<IActionResult> Index()
        {
            UnitofWork _uofw = new UnitofWork(_context, _userManager.GetUserAsync(User).Result);
            var result = _uofw.Organisations.GetAllOrganisations();
            return View(OrgDTO.ToIndexModel(result));
        }

        // GET: Organisations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrganisation = await _context.Organisations
                .Include(t => t.ApplicationUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tblOrganisation == null)
            {
                return NotFound();
            }

            return View(tblOrganisation);
        }

        // GET: Organisations/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = _userManager.GetUserId(User);
            return View();
        }

        // POST: Organisations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Contact,ContactEmail,ContactNo,Address,ApplicationUserId")] OrganisationViewModel FromView)
        {
            if (ModelState.IsValid)
            {
                FromView.Id = Guid.NewGuid().ToString();                
                TblOrganisation result =  OrgDTO.ToTableModel(FromView);                
                _context.Organisations.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }           
            return View(FromView);
        }

        // GET: Organisations/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            UnitofWork _uofw = new UnitofWork(_context, _userManager.GetUserAsync(User).Result);
            var result = _uofw.Organisations.GetOrganisationbyId(id);
            if (result == null)
            {
                return NotFound();
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", tblOrganisation.ApplicationUserId);
            return View(OrgDTO.ToViewModel(result));
        }

        // POST: Organisations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Contact,ContactEmail,ContactNo,Address")] OrganisationViewModel FromView)
        {
            if (ModelState.IsValid)
            {
                UnitofWork _uofw = new UnitofWork(_context, _userManager.GetUserAsync(User).Result);
                var result = _uofw.Organisations.GetOrganisationbyId(id);
                if (result == null)
                {
                    return NotFound();
                }
                OrgDTO.ToTableModel(result, FromView);
               _uofw.Complete();               
                return RedirectToAction(nameof(Index));
            }
            return View(FromView);
        }

        // GET: Organisations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrganisation = await _context.Organisations
                .Include(t => t.ApplicationUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tblOrganisation == null)
            {
                return NotFound();
            }

            return View(tblOrganisation);
        }

        // POST: Organisations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tblOrganisation = await _context.Organisations.FindAsync(id);
            _context.Organisations.Remove(tblOrganisation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOrganisationExists(Guid id)
        {
            return _context.Organisations.Any(e => e.ID == id);
        }
    }
}
