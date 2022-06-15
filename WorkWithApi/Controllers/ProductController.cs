using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WorkWithApi.Data.DAL;

namespace WorkWithApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context=context;
        }

        private static List<Product> products = new List<Product>() {
            new Product { Id=1, Name="Product1",Price=60 },
            new Product { Id=2, Name="Product2",Price=70 },
            new Product { Id=3, Name="Product3",Price=80 },
            new Product { Id=4, Name="Product4",Price=90 }
        };
        //[Route("all")]
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(200, _context.Products.ToList());

        }

        //[Route("one")]

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id==id);
            if (p==null)
                return NotFound();
            return StatusCode(200, p);

        }

        [HttpPost]
        public IActionResult Add(Product p)
        {
            Product newProduct = new Product();
            newProduct.Name = p.Name;
            newProduct.Price = p.Price;

            _context.Add(newProduct);
            _context.SaveChanges();
            return StatusCode(201, $"{p.Name} Product added");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id==id);
            if (p==null)
                return NotFound();
            _context.Remove(p);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
