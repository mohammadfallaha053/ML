using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ML.Api.Dtos.Analyse;
using ML.Core;
using ML.Core.Models;
using ML.EF;
using Rotativa.AspNetCore;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class AnalyseController : ControllerBase
    {

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public AnalyseController(IUintOfWork uintOfWork, IMapper mapper, AppDbContext context)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
            _context = context;

        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseAnalyseDto>>(await _uintOfWork.Analyses.GetAllAsync()));
        [HttpGet("GetAllWithGroup")]
        public async Task<IActionResult> GetAllWithGroupAsync() => Ok(_mapper.Map<IEnumerable<ResponseAnalyseDto>>(await _uintOfWork.Analyses.GetAllAsync(new[] { "Group" })));


        [HttpGet("GetAllWithNaturalField")]
        public async Task<IActionResult> GetAllWithNaturalFieldAsync() => Ok(_mapper.Map<IEnumerable<ResponseAnalyseDto>>(await _uintOfWork.Analyses.GetAllAsync(new[] { "NaturalField" })));

        [HttpGet("GetAllWithCategory")]
        public async Task<IActionResult> GetAllWithCategoryAsync() => Ok(_mapper.Map<IEnumerable<ResponseAnalyseDto>>(await _uintOfWork.Analyses.GetAllAsync(new[] { "Category" })));

        [HttpGet("GetAllWithCategoryAndGroupAndNaturalField")]
        public async Task<IActionResult> GetAllWithCategoryAndGroupAsync() => Ok(_mapper.Map<IEnumerable<ResponseAnalyseDto>>(await _uintOfWork.Analyses.GetAllAsync(new[] { "Category", "Group", "NaturalFields" })));




        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var Analyse = await _uintOfWork.Analyses.Find(b => b.AnalyseId == GetById);

            if (Analyse == null) { return NotFound($"No Analyse with ID:{GetById}"); }

            return Ok(_mapper.Map<ResponseAnalyseDto>(Analyse));
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateAnalyseDto dto)
        {


            var Analyse = await _uintOfWork.Analyses.Add(_mapper.Map<Analyse>(dto));
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseAnalyseDto>(Analyse));

        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateAnalyseDto dto)
        {
            var old_Analyse = await _uintOfWork.Analyses.Find(b => b.AnalyseId == Id);
            if (old_Analyse == null) return NotFound($"No Analyse with Id:{Id}");
                

            old_Analyse.Name = dto.Name;
            old_Analyse.NUint = dto.NUint;
            old_Analyse.CategoryId = dto.CategoryId;
            old_Analyse.GroupId=dto.GroupId;
          
          
            _uintOfWork.Complete();

            var update_Analyse = _uintOfWork.Analyses.Update(old_Analyse);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponseAnalyseDto>(update_Analyse));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Analyse = await _uintOfWork.Analyses.Find(b => b.AnalyseId == Id);

            if (Analyse == null) return NotFound($"No Analyse with Id:{Id}");

            var delete_Analyse = _uintOfWork.Analyses.Delete(Analyse);
            _uintOfWork.Complete();
            return Ok();

        }


    }
}
