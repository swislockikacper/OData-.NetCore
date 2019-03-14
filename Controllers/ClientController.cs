using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OData.NetCore.Models;

namespace OData.NetCore.Controllers
{
    [Produces("Application/json")]
    public class ClientController : ODataController
    {
        private readonly DatabaseContext dbContext;

        public ClientController(DatabaseContext dbContext)
            => this.dbContext = dbContext;

        [EnableQuery]
        public IQueryable<Client> Get()
            => dbContext.Clients.AsQueryable();

        public IActionResult Post([FromBody]Client client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dbContext.Clients.Add(client);
            dbContext.SaveChanges();

            return Created(client);
        }
    }
}
