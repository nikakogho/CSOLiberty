using CSOLiberty.Data;
using CSOLiberty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace CSOLiberty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SellersController(AppDbContext context)
        {
            _context = context;
        }

        object SellerToJson(Seller s)
        {
            return new
            {
                id = s.ID,
                bossId = s.BossID,
                firstName = s.FirstName,
                lastName = s.LastName
            };
        }
        
        [HttpGet]
        public IEnumerable Get()
        {
            return _context.Sellers.Select(SellerToJson).ToArray();
        }
    }
}
