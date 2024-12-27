using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Reflection;
using System.Threading;
using System;
using quan_ly_tai_nguyen_rung.DATA;
using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.Models.section3;

namespace quan_ly_tai_nguyen_rung.Controllers
{

        public class WoodProcessingFacilitiesController : Controller
        {
            private readonly ApplicationDbContext _context;

            public WoodProcessingFacilitiesController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: WoodProcessingFacilities
            public async Task<IActionResult> Index()
            {
                var facilities = await _context.WoodProcessingFacilities.ToListAsync();
                return View(facilities);
            }

            // GET: WoodProcessingFacilities/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null) return NotFound();

                var facility = await _context.WoodProcessingFacilities
                    .Include(w => w.Commune)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (facility == null) return NotFound();

                return View(facility);
            }

            // GET: WoodProcessingFacilities/Create
            public IActionResult Create()
            {
                ViewData["IdCommune"] = new SelectList(_context.Communes, "Id", "Name");
                return View();
            }

            // POST: WoodProcessingFacilities/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Name,Status,Address,ContactFace,ContactMail,ContactPhone,Labor,Acreage,Yield,WoodSpeciesProvided,Product,ProductionType,ActivityForm,ImageWoodProcessingFacility,IdCommune")] WoodProcessingFacility facility)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(facility);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdCommune"] = new SelectList(_context.Communes, "Id", "Name", facility.IdCommune);
                return View(facility);
            }

            // GET: WoodProcessingFacilities/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) return NotFound();

                var facility = await _context.WoodProcessingFacilities.FindAsync(id);
                if (facility == null) return NotFound();

                ViewData["IdCommune"] = new SelectList(_context.Communes, "Id", "Name", facility.IdCommune);
                return View(facility);
            }

            // POST: WoodProcessingFacilities/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,Address,ContactFace,ContactMail,ContactPhone,Labor,Acreage,Yield,WoodSpeciesProvided,Product,ProductionType,ActivityForm,ImageWoodProcessingFacility,IdCommune")] WoodProcessingFacility facility)
            {
                if (id != facility.Id) return NotFound();

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(facility);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!WoodProcessingFacilityExists(facility.Id)) return NotFound();
                        else throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdCommune"] = new SelectList(_context.Communes, "Id", "Name", facility.IdCommune);
                return View(facility);
            }

            // GET: WoodProcessingFacilities/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null) return NotFound();

                var facility = await _context.WoodProcessingFacilities
                    .Include(w => w.Commune)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (facility == null) return NotFound();

                return View(facility);
            }

            // POST: WoodProcessingFacilities/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var facility = await _context.WoodProcessingFacilities.FindAsync(id);
                if (facility != null)
                {
                    _context.WoodProcessingFacilities.Remove(facility);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }

            private bool WoodProcessingFacilityExists(int id)
            {
                return _context.WoodProcessingFacilities.Any(e => e.Id == id);


            }
        
    }
}  