using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return null;
            }

            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks =  _context.Stock.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s=> s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s=> s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    if (query.IsDescending)
                    {
                        stocks = stocks.OrderByDescending(s=> s.Symbol);
                    }
                    else
                    {
                        stocks = stocks.OrderBy(s=>s.Symbol);
                    }
                }
            }

            var skipnumber = (query.PageNumber-1) * query.PageSize;

            return await stocks.Skip(skipnumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            return stock;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stock.AnyAsync(s=>s.Id==id);
        }


        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;
        }

        
    }
}
