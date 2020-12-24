using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Charts.Data;
using Charts.Models;

namespace Charts.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesRecords
        public async Task<IActionResult> Index()
        {
            return View(await _context.salesRecords.ToListAsync());
        }

        // GET: SalesRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecords = await _context.salesRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesRecords == null)
            {
                return NotFound();
            }

            return View(salesRecords);
        }

        // GET: SalesRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SaleDate,Electronics,BookAndMedia,HomeAndKitchen")] SalesRecords salesRecords)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesRecords);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesRecords);
        }

        // GET: SalesRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecords = await _context.salesRecords.FindAsync(id);
            if (salesRecords == null)
            {
                return NotFound();
            }
            return View(salesRecords);
        }

        // POST: SalesRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SaleDate,Electronics,BookAndMedia,HomeAndKitchen")] SalesRecords salesRecords)
        {
            if (id != salesRecords.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesRecords);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesRecordsExists(salesRecords.Id))
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
            return View(salesRecords);
        }

        // GET: SalesRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecords = await _context.salesRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesRecords == null)
            {
                return NotFound();
            }

            return View(salesRecords);
        }

        // POST: SalesRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesRecords = await _context.salesRecords.FindAsync(id);
            _context.salesRecords.Remove(salesRecords);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesRecordsExists(int id)
        {
            return _context.salesRecords.Any(e => e.Id == id);
        }
    }
}
