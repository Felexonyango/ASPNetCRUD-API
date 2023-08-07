using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Context;
using BookApp.DTos;
using Mapster;
using BookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services
{
    public class StockService
    {

             
    private readonly ApplicationDbContext _dbContext;
      public StockService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<StockDto> AddStock(StockDto stockDto)
        {
          
          var  stockEntity= stockDto.Adapt<Stock>();
           var addedStock = await _dbContext.Stocks.AddAsync(stockEntity);
                await _dbContext.SaveChangesAsync();

                 var addedStockDto = addedStock.Entity.Adapt<StockDto>();
               return addedStockDto;
        }
            public async Task<IEnumerable<Stock>> GetAllStocks()
        {
            var stocks = await _dbContext.Stocks.ToListAsync();

            return stocks;
        }


    }
}