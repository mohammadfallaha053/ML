using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.Group;
using ML.Core;
using ML.Core.Models;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GroupController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;

        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseGroupDto>>(await _uintOfWork.Groups.GetAllAsync()));




        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var Group = await _uintOfWork.Groups.Find(b => b.GroupId == GetById);

            if (Group == null) { return NotFound($"No Group with ID:{GetById}"); }

            return Ok(_mapper.Map<ResponseGroupDto>(Group));
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateGroupDto dto)
        {


            var Group = await _uintOfWork.Groups.Add(_mapper.Map<Group>(dto));
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseGroupDto>(Group));

        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateGroupDto dto)
        {
            var old_Group = await _uintOfWork.Groups.Find(b => b.GroupId == Id);
            if (old_Group == null) return NotFound($"No Group with Id:{Id}");
                

            old_Group.GroupName = dto.GroupName;
          
          
            _uintOfWork.Complete();

            var update_Group = _uintOfWork.Groups.Update(old_Group);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponseGroupDto>(update_Group));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Group = await _uintOfWork.Groups.Find(b => b.GroupId == Id);

            if (Group == null) return NotFound($"No Group with Id:{Id}");

            var delete_Group = _uintOfWork.Groups.Delete(Group);
            _uintOfWork.Complete();
            return Ok();

        }


    }
}
