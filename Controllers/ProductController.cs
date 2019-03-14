using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OData.NetCore.Models;

namespace OData.NetCore.Controllers
{
    [Produces("Application/json")]
    public class ProductController : ODataController
    {
        private readonly DatabaseContext dbContext;

        public ProductController(DatabaseContext dbContext)
            => this.dbContext = dbContext;

        [EnableQuery]
        public IQueryable<Product> Get()
            => dbContext.Products.AsQueryable();

        public IActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return Created(product);
        }
    }
}
