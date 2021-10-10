using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.Interfaces.Services;
using SupplyChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyChain.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("health")]
        public IActionResult GetAllx()
        {
            return Ok("up");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _productService.GetAll();
            return Ok(res);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _productService.GetById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] ProductRequest product)
        {
            var res = await _productService.Insert(product);
            return Ok(res);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductRequest product)
        {
            var res = await _productService.Update(id, product);
            return Ok(res);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _productService.Delete(id);
            return Ok(res);
        }
    }
}
