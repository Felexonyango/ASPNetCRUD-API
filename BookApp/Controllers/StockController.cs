using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookApp.Errors;
using BookApp.DTos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;

namespace BookApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        public StockService _stockService;
        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetStock()
        {
            var result = await _stockService.GetAllStocks();
            return Ok(result);

        }
    }
}