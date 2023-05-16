using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4Mvc.Data;
using Lab4Mvc.Models;

namespace Lab4Mvc.Controllers
{
    public class CustomerBookListController : Controller
    {
        private readonly Lab4DbContext _context;

        public CustomerBookListController(Lab4DbContext context)
        {
            _context = context;
        }

        // GET: CustomerBookList
        public async Task<IActionResult> Index()
        {
            var lab4DbContext = _context.CuBoLists.Include(c => c.Books).Include(c => c.Customers);
            return View(await lab4DbContext.ToListAsync());
        }

        // GET: Search a specific customer
        public async Task<IActionResult> Search(string SearchCustomer)
        {
            var customer = await _context.Customers
                .Include(bl => bl.CuBoLists)
                .ThenInclude(b => b.Books)
                .Where(c => c.FirstName.Contains(SearchCustomer))
                .ToListAsync();

            return View("Index", customer.SelectMany(c => c.CuBoLists));
        }

        // GET: CustomerBookList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CuBoLists == null)
            {
                return NotFound();
            }

            var customerBookList = await _context.CuBoLists
                .Include(c => c.Books)
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.CuBoListId == id);
            if (customerBookList == null)
            {
                return NotFound();
            }

            return View(customerBookList);
        }

        // GET: CustomerBookList/Create
        public IActionResult Create()
        {
            ViewData["FK_BookId"] = new SelectList(_context.Books, "BookId", "Author");
            ViewData["FK_CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName");
            return View();
        }

        // POST: CustomerBookList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CuBoListId,FK_CustomerId,FK_BookId,BookStart,BookEnd,Retrieved,Returned")] CustomerBookList customerBookList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerBookList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_BookId"] = new SelectList(_context.Books, "BookId", "Author", customerBookList.FK_BookId);
            ViewData["FK_CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", customerBookList.FK_CustomerId);
            return View(customerBookList);
        }

        // GET: CustomerBookList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CuBoLists == null)
            {
                return NotFound();
            }

            var customerBookList = await _context.CuBoLists.FindAsync(id);
            if (customerBookList == null)
            {
                return NotFound();
            }
            ViewData["FK_BookId"] = new SelectList(_context.Books, "BookId", "Author", customerBookList.FK_BookId);
            ViewData["FK_CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", customerBookList.FK_CustomerId);
            return View(customerBookList);
        }

        // POST: CustomerBookList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CuBoListId,FK_CustomerId,FK_BookId,BookStart,BookEnd,Retrieved,Returned")] CustomerBookList customerBookList)
        {
            if (id != customerBookList.CuBoListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerBookList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerBookListExists(customerBookList.CuBoListId))
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
            ViewData["FK_BookId"] = new SelectList(_context.Books, "BookId", "Author", customerBookList.FK_BookId);
            ViewData["FK_CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", customerBookList.FK_CustomerId);
            return View(customerBookList);
        }

        // GET: CustomerBookList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CuBoLists == null)
            {
                return NotFound();
            }

            var customerBookList = await _context.CuBoLists
                .Include(c => c.Books)
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.CuBoListId == id);
            if (customerBookList == null)
            {
                return NotFound();
            }

            return View(customerBookList);
        }

        // POST: CustomerBookList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CuBoLists == null)
            {
                return Problem("Entity set 'Lab4DbContext.CuBoLists'  is null.");
            }
            var customerBookList = await _context.CuBoLists.FindAsync(id);
            if (customerBookList != null)
            {
                _context.CuBoLists.Remove(customerBookList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerBookListExists(int id)
        {
          return (_context.CuBoLists?.Any(e => e.CuBoListId == id)).GetValueOrDefault();
        }
    }
}
