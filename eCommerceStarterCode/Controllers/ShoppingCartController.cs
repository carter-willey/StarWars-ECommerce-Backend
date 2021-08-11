using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/shoppingcart")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _context;
        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<shoppingcartController>
        //GETS ALL ITEMS IN USERS CART 
        [HttpGet, Authorize]
        public IActionResult Get([FromBody]ShoppingCart value)
        {

            var shoppingCart = _context.ShoppingCarts;
            var specificUserCart = shoppingCart.Where(sc => sc.UserId == value.UserId);
            return Ok(specificUserCart);
        }

        // POST api/shoppingcart
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] ShoppingCart value)
        {
            _context.ShoppingCarts.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        //// PATCH api/<ShoppingCartController>/5
        [HttpPatch("{id}"), Authorize]
        public IActionResult Put(int id, [FromBody] ShoppingCart value)
        {

            var itemInCart = _context.ShoppingCarts.Where(sc => sc.ShoppingCartId == id).FirstOrDefault(sc => sc.UserId == value.UserId);
            
            itemInCart.Quantity = value.Quantity;
            _context.SaveChanges();
            return Ok(itemInCart);
        }

        //// DELETE api/<ShoppingCartController>/5
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id, [FromBody] ShoppingCart value)
        {
            var itemInCart = _context.ShoppingCarts.Where(sc => sc.ShoppingCartId == id).FirstOrDefault(sc => sc.UserId == value.UserId);
            _context.Remove(itemInCart);
            _context.SaveChanges();
            return Ok();

        }
    }
}
