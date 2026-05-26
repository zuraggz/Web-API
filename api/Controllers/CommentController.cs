using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepo, IMapper mapper, IStockRepository stockRepository)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            var comments = await _commentRepo.GetAllAsync();
            var commentDtos = _mapper.Map<List<CommentDto>>(comments);

            return Ok(commentDtos);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _commentRepo.GetByIdAsync(id);
            if(result == null)
            {
                return NotFound();
            }

            var resultDto = _mapper.Map<CommentDto>(result);
            return Ok(resultDto);
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!await _stockRepository.StockExists(stockId))
            {
                return BadRequest("The specified stock does not exist.");
            }

            var comment = _mapper.Map<Comment>(commentDto);
            comment.StockId = stockId;
            await _commentRepo.CreateAsync(comment);

            var CommentDto =  _mapper.Map<CommentDto>(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, CommentDto);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment1 = _mapper.Map<Comment>(updateDto);
            var comment =  await _commentRepo.UpdateAsync(id, comment1);

            if(comment == null)
            {
                return NotFound("The specified comment could not be found.");
            }

            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentModel = await _commentRepo.DeleteAsync(id);

            if(commentModel == null)
            {
                return NotFound();
            }

            return Ok(commentModel);
        }
    }
}
