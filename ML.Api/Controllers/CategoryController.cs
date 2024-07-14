using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ML.Api.Dtos.Category;
using ML.Core;
using ML.Core.Models;
using ML.EF;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CategoryController(IUintOfWork uintOfWork, IMapper mapper, AppDbContext context)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
            _context = context;

        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseCategoryDto>>(await _uintOfWork.Categories.GetAllAsync()));


        [HttpGet("WithAnalyses")]
        public async Task<IActionResult> GetAllWithAnalysesAsync() => Ok(_mapper.Map<IEnumerable<ResponseCategoryDto>>(await _uintOfWork.Categories.GetAllAsync(new[] { "Analyses" })));


        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var Category = await _uintOfWork.Categories.Find(b => b.CategoryId == GetById);

            if (Category == null) { return NotFound($"No Category with ID:{GetById}"); }

            return Ok(_mapper.Map<ResponseCategoryDto>(Category));
        }


       


        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateCategoryDto dto)
        {


            var Category = await _uintOfWork.Categories.Add(_mapper.Map<Category>(dto));
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseCategoryDto>(Category));

        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateCategoryDto dto)
        {
            var old_Category = await _uintOfWork.Categories.Find(b => b.CategoryId == Id);
            if (old_Category == null) return NotFound($"No Category with Id:{Id}");
                

            old_Category.CategoryName = dto.CategoryName;
          
          
            _uintOfWork.Complete();

            var update_Category = _uintOfWork.Categories.Update(old_Category);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponseCategoryDto>(update_Category));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Category = await _uintOfWork.Categories.Find(b => b.CategoryId == Id);

            if (Category == null) return NotFound($"No Category with Id:{Id}");

            var delete_Category = _uintOfWork.Categories.Delete(Category);
            _uintOfWork.Complete();
            return Ok();

        }


    }
}
