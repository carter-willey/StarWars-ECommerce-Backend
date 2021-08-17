using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public IActionResult Get()
        {
            var shoppingCart = _context.ShoppingCarts.Include(sc => sc.Product.User);
            var specificUserCart = shoppingCart.Where(sc => sc.UserId == User.FindFirstValue("id"));
            return Ok(specificUserCart);
        }

        // PUT api/shoppingcart
        [HttpPut, Authorize]
        public IActionResult Post([FromBody] ShoppingCart value)
        {
            var usersShoppingCart = _context.ShoppingCarts.Where(sc => sc.UserId == User.FindFirstValue("id"));
            
            if (usersShoppingCart.Where(sc => sc.ProductId == value.ProductId).Count() > 0 ){
                var itemInCart = usersShoppingCart.FirstOrDefault(sc => sc.ProductId == value.ProductId);
                itemInCart.UserId = User.FindFirstValue("id");
                itemInCart.Quantity += 1;
                _context.SaveChanges();
                return Ok( itemInCart);
            }
            else
            {
                value.Quantity = 1;
                value.UserId = User.FindFirstValue("id");
                _context.ShoppingCarts.Add(value);
                _context.SaveChanges();
                return StatusCode(201, value);

            }

            
        }

        //// PATCH api/<ShoppingCartController>/5
        [HttpPatch("{id}"), Authorize]
        public IActionResult Patch(int id, [FromBody] ShoppingCart value)
        {

            var itemInCart = _context.ShoppingCarts.Where(sc => sc.ShoppingCartId == id).FirstOrDefault(sc => sc.UserId == User.FindFirstValue("id"));
            
            itemInCart.Quantity = value.Quantity;
            if(itemInCart.Quantity <= 0)
            {
                _context.Remove(itemInCart);
                _context.SaveChanges();
            }
            _context.SaveChanges();
            return Ok(itemInCart);
        }

        //// DELETE api/<ShoppingCartController>/5
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            var itemInCart = _context.ShoppingCarts.Where(sc => sc.ShoppingCartId == id).FirstOrDefault(sc => sc.UserId == User.FindFirstValue("id"));
            _context.Remove(itemInCart);
            _context.SaveChanges();
            return Ok();

        }
    }
}
