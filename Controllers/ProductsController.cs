using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repo;

namespace ProductCatalog.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IProductRepo Product { get; }

        public ProductsController(IProductRepo product,ApplicationDbContext context)
        {
            Product = product;
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            if (_context.Products != null)
            {
                try
                {
                    return View(Product.GetAll().ToList());
                }
                catch(Exception ex) 
                {
                    return BadRequest(ex.Message);
                }
            }

            return View();

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if(ModelState.IsValid)
            {
                var prod=_context.Products.FirstOrDefault(p=>p.ProductID== id);
                if(prod!=null)
                {

                try
                {
                   return View(Product.GetById(id));
                }
                catch(Exception ex)
                {
                    return NotFound(ex.Message);
                }
               }
            }

            return NotFound();
        }
        [Authorize(Roles ="Admin")]
        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            ViewBag.CategoryID = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,Price,CreationDate,CreatedBy,StartDate,Duration,CategoryID")] Product product)
        {


            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,CreationDate,CreatedBy,StartDate,Duration,CategoryID")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
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
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
