using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IndustryConnect_Week5_WebApi.Models;
using IndustryConnect_Week5_WebApi.Dtos;
using Microsoft.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace IndustryConnect_Week5_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IndustryConnectWeek2Context _context;

        public SaleController(IndustryConnectWeek2Context context)
        {
            _context = context;
        }

        // GET: api/Sale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSales()
        {
            var sales = await _context.Sales
                .Include(s => s.Store)
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .Select(s => new SaleDto
                {
                    Id = s.Id,
                    StoreName = s.Store.Name,
                    CustomerName = s.Customer.FirstName + " " + s.Customer.LastName,
                    ProductName = s.Product.Name,
                    SaleDate = (DateTime)s.DateSold

                }).ToListAsync();

            if (sales.Count > 0)
            {
                return Ok(sales);
            }
            else
            {
                return BadRequest("No sales found.");
            }
        }


        // GET: api/Sale/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetSale(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Store)    // Include related Store entity
                .Include(s => s.Customer) // Include related Customer entity
                .Include(s => s.Product)  // Include related Product entity
                .Where(s => s.Id == id)   // Find the sale by id
                .Select(s => new SaleDto  // Project the result into SaleDto
                {
                    Id = s.Id,
                    StoreName = s.Store.Name,
                    CustomerName = s.Customer.FirstName + " " + s.Customer.LastName,
                    ProductName = s.Product.Name,
                    SaleDate = (DateTime)s.DateSold
                })
                .FirstOrDefaultAsync();  // Return the first match or null

            if (sale == null)
            {
                return NotFound();  // Return 404 if the sale doesn't exist
            }

            return Ok(sale);  // Return the sale as a SaleDto
        }

        // PUT: api/Sale/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, SaleDto saleDto)
        {
            if (id != saleDto.Id)
            {
                return BadRequest("Sale ID mismatch.");
            }

            // Retrieve the store, customer, and product entities by name
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.Name == saleDto.StoreName);
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.FirstName + " " + c.LastName == saleDto.CustomerName);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == saleDto.ProductName);

            if (store == null || customer == null || product == null)
            {
                return BadRequest("Invalid store, customer, or product details.");
            }

            var existingSale = await _context.Sales.FindAsync(id);
            if (existingSale == null)
            {
                return NotFound();
            }

            // Update the sale entity with the new values
            existingSale.StoreId = store.StoreId;
            existingSale.CustomerId = customer.Id;
            existingSale.ProductId = product.Id;
            existingSale.DateSold = saleDto.SaleDate;

            _context.Entry(existingSale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; // Rethrow the exception for further handling
                }
            }

            // Map back to SaleDto for response
            var updatedSaleDto = new SaleDto
            {
                Id = existingSale.Id,
                StoreName = store.Name,
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                ProductName = product.Name,
                SaleDate = (DateTime)existingSale.DateSold
            };

            return Ok(updatedSaleDto);
        }


        // POST: api/Sale
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaleDto>> PostSale(SaleDto saleDto)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.Name == saleDto.StoreName);
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.FirstName + " " + c.LastName == saleDto.CustomerName);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == saleDto.ProductName);

            if (store == null || customer == null || product == null)
            {
                return BadRequest("Invalid store, customer, or product details.");
            }

            var sale = new Sale
            {
                StoreId = store.StoreId,
                CustomerId = customer.Id,
                ProductId = product.Id,
                DateSold = saleDto.SaleDate
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            saleDto.Id = sale.Id;

            return CreatedAtAction("GetSale", new { id = saleDto.Id }, saleDto);
        }



        // DELETE: api/Sale/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            // Find the sale by ID
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound(); // Return 404 if the sale is not found
            }

            // Remove the sale from the context
            _context.Sales.Remove(sale);

            // Attempt to save changes to the database
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content on successful deletion
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
