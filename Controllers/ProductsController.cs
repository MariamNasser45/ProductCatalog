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
        private IProductRepo Product { get; }
        private ICategoryRepo CategoryRepo { get; }

        public ProductsController(IProductRepo product,ICategoryRepo categoryRepo,ApplicationDbContext context)
        {
            Product = product;
            CategoryRepo = categoryRepo;
            _context = context;
        }

        // GET: Products
        public IActionResult Index()
        {
            ViewBag.CategoryID = CategoryRepo.GetAll();

            if (Product.GetAll().Count != 0)
            {
                try
                {
                    if(User.IsInRole("Admin"))
                    {
                         return View(Product.GetAll());
                    }
                    else
                    {
                        return View(Product.GetAllAvail());
                    }
                }
                catch(Exception ex) 
                {
                    return BadRequest("Some Error Occured Please try Again Latter");
                }
            }
            else
            {
            return View();
            }
        }

        [HttpPost]
        public IActionResult Index(int CategoryId)
        {
            ViewBag.CategoryID =CategoryRepo.GetAll();

            if (CategoryRepo.CheckCategoryExistance(CategoryId))
            {
                if(Product.CheckProductExistance(CategoryId))
                {
                    if(User.IsInRole("Admin"))
                    {
                        return View(Product.GetAll().Where(p=>p.CategoryID==CategoryId));
                    }
                    else
                    {
                        return View(Product.GetAllAvail().Where(c => c.CategoryID==CategoryId));
                    }
                }
                return BadRequest("Oops! There are no Products in selected category .... \nPlease try again leter or select other ");
            }
            return View(Product.GetAllAvail());
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
                if(Product.GetById(id)!=null)
                {
                    try
                    {
                       return View(Product.GetById(id));
                    }
                    catch(Exception ex)
                    {
                        return BadRequest("These Product is not Available");
                    }
               }

            return BadRequest("These Product is not Available");
        }

        [Authorize(Roles ="Admin")]
        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(CategoryRepo.GetAll(), "CategoryID" , "CategoryName");

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            //var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

             product.createdBy = HttpContext.User.Identity.Name; // get current user name
             product.creationDate = DateTime.Now;
            if(product != null)
            {
                try
                {
                    Product.Insert(product);
                    Details(product.ProductID);
                    return View("Details");
                }
                catch
                {
                    return BadRequest("Some Error Occure While Creating");
                }
            }
            
            return BadRequest("Some Data are missed");
        }

        [Authorize(Roles = "Admin")]
        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryID =new SelectList(CategoryRepo.GetAll(),"CategoryID","CategoryName");
 
            if (Product.GetById(id)!=null)
            {
                return View(Product.GetById(id));
            }
            return NotFound("Invalid Product To Edit");

        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
                try
                {
                    Product.Update(product.ProductID , product);
                    Details(product.ProductID);
                    return View("Details");
                }
                catch (Exception ex)
                {
                        return NotFound($"This product Invalid to Edit, Error Details {ex.Message}");
                }

            return BadRequest();
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            if (Product.GetById(id)!=null)
            {
              return View(Product.GetById(id));
            }
            return NotFound();

        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (Product.GetById(id) != null)
            {
                try
                {
                    Product.Delete(id);
                    Index(0);
                    return View("Index");
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            
            return View("Index");
        }

    }
}
