using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using ProductCatalog.Data;
using ProductCatalog.Models;
using System.Diagnostics.Metrics;
using System.Drawing;
using System;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace ProductCatalog.Repo.RepoServices
{
    public class ProductServices : IProductRepo
    {
        public ApplicationDbContext Context { get; }
        public ProductServices(ApplicationDbContext context)
        {
            Context = context;
        }
        
        public void Delete(int id)
        {
            //using Finde or SingleOrDefault instead SingleOrDefault because id is unique identifier
            var deleteProduct = Context.Products.SingleOrDefault(p => p.ProductID == id);

            if (deleteProduct != null)
            {
                Context.Remove(deleteProduct);
                Context.SaveChanges();
              
            }
        }

        public List<Product> GetAllAvail()
        {
            //dont't need to convert 
            //return Context.Products.Where(c => Convert.ToInt32(DateTime.Now.Day- c.startDate.Day) < c.duration).ToList(); 

            return Context.Products.Where(c => DateTime.Now >= c.startDate
                                                && ((DateTime.Now.Day - c.startDate.Day) <= (c.duration))
                                                &&(DateTime.Now.Month - c.startDate.Month) <= (c.startDate.AddDays(c.duration).Month)
                                                && (DateTime.Now.Year - c.startDate.Year) <= (c.startDate.AddDays(c.duration).Year))
                                                .ToList();
        }


        public List<Product> GetAll()
        {
            return Context.Products.ToList();
        }

        public Product GetById(int id)
        {

            return Context.Products.SingleOrDefault(p => p.ProductID == id);
        }

        public void Insert(Product product)
        {
            Context.Add(product);
            Context.SaveChanges();
        }
        public bool CheckProductExistance(int CategoryId)
        {
            return Context.Products.Any(p => p.CategoryID == CategoryId);
        }

        public void Update(int id, Product product)
        {
            var Editproduct = Context.Products.SingleOrDefault(p => p.ProductID == id);

            Editproduct.productName = product.productName;
            Editproduct.price = product.price;
            Editproduct.duration = product.duration;
            Editproduct.startDate = product.startDate;
            Editproduct.CategoryID = product.CategoryID;

            //Context.Update(product);
            Context.SaveChanges();

        }


    }
}
