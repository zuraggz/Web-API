using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        private readonly IMapper _mapper;
        public StockController(IMapper mapper, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = _mapper.Map<List<StockDto>>(stocks);


            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepo.GetByIdAsync(id);
            if(stock == null)
            {   
                return NotFound();
            }

            var stockDto = _mapper.Map<StockDto>(stock);

            return Ok(stockDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = _mapper.Map<Stock>(stockDto);
            await _stockRepo.CreateAsync(stockModel);

            var stockToReturn = _mapper.Map<StockDto>(stockModel);

            return CreatedAtAction(nameof(GetById),
                new { id = stockModel.Id },
                stockToReturn);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepo.UpdateAsync(id, stockDto);

            if (stockModel == null)
            {
                return NotFound();
            }

            var stockToReturn = _mapper.Map<StockDto>(stockModel);

            return Ok(stockToReturn);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _stockRepo.DeleteAsync(id);
            if(result == null)
            {
                return NotFound();  
            }

            return NoContent();

        }

    }
}
