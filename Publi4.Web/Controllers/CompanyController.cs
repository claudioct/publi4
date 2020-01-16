using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Publi4.Domain;
using Publi4.Domain.Entities;

namespace Publi4.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _companyLogger;
        private readonly Publi4DbContext _context;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.PageTitle = "Empresas";
            ViewBag.ContentName = "Empresa";
            base.OnActionExecuting(context);
        }

        public CompanyController(Publi4DbContext context, ILogger<CompanyController> companyLogger)
        {
            _companyLogger = companyLogger;
            _context = context;
        }

        // GET: Company
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyEntity = await _context.Companies
                .SingleOrDefaultAsync(m => m.IdCompany == id);
            if (companyEntity == null)
            {
                return NotFound();
            }

            return View(companyEntity);
        }

        // GET: Company/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompany,Nome")] CompanyEntity companyEntity)
        {
            if (ModelState.IsValid)
            {
                companyEntity.IdCompany = Guid.NewGuid();
                _context.Add(companyEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyEntity);
        }

        // GET: Company/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyEntity = await _context.Companies.SingleOrDefaultAsync(m => m.IdCompany == id);
            if (companyEntity == null)
            {
                return NotFound();
            }
            return View(companyEntity);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdCompany,Nome")] CompanyEntity companyEntity)
        {
            if (id != companyEntity.IdCompany)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {						
                    _context.Update(companyEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyEntityExists(companyEntity.IdCompany))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(companyEntity);
        }

        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyEntity = await _context.Companies
                .SingleOrDefaultAsync(m => m.IdCompany == id);
            if (companyEntity == null)
            {
                return NotFound();
            }

            return View(companyEntity);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyEntity = await _context.Companies.SingleOrDefaultAsync(m => m.IdCompany == id);
            _context.Companies.Remove(companyEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CompanyEntityExists(Guid id)
        {
            return _context.Companies.Any(e => e.IdCompany == id);
        }
    }
}
