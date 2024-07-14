using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.InsuranceCompanyPhone;
using ML.Core;
using ML.Core.Interfaces;
using ML.Core.Models;
using System.Xml.Linq;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class InsuranceCompanyPhoneController : ControllerBase
    {


        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;


        public InsuranceCompanyPhoneController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }




        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseInsuranceCompanyPhoneDto>>(await _uintOfWork.InsuranceCompanyPhones.GetAllAsync()));
       




        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetById(int GetById)
        {
            var Phone = await _uintOfWork.InsuranceCompanyPhones.Find(p => p.InsuranceCompanyPhoneId == GetById);
            if(Phone == null) { return NotFound($"No Phone with Id:{GetById}"); }
           
            return Ok(_mapper.Map<ResponseInsuranceCompanyPhoneDto>(Phone));
        }


       [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateInsuranceCompanyPhoneDto dto)
        {
            var InsuranceCompany = await _uintOfWork.InsuranceCompanies.Find(p => p.InsuranceCompanyId == dto.InsuranceCompanyId);
            if (InsuranceCompany == null) { return NotFound($"No InsuranceCompany with Id:{dto.InsuranceCompanyId} in data base are You sure?"); }
            var phone = await _uintOfWork.InsuranceCompanyPhones.Add(_mapper.Map<InsuranceCompanyPhone>(dto));
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseInsuranceCompanyPhoneDto>(phone));

        }





        [HttpPut("{InsuranceCompanyPhoneId}")]

        public async Task<IActionResult> Update(int InsuranceCompanyPhoneId, [FromBody] UpdateInsuranceCompanyPhoneDto dto)
        {
            var old_InsuranceCompanyPhone = await _uintOfWork.InsuranceCompanyPhones.Find(b => b.InsuranceCompanyPhoneId == InsuranceCompanyPhoneId);
            if (old_InsuranceCompanyPhone == null) return NotFound($"No Phone with Id:{InsuranceCompanyPhoneId}");

            var InsuranceCompany = await _uintOfWork.InsuranceCompanies.Find(p => p.InsuranceCompanyId == dto.InsuranceCompanyId);
            if (InsuranceCompany == null) { return NotFound($"No InsuranceCompany with Id:{dto.InsuranceCompanyId} in data base are You sure?"); }

            old_InsuranceCompanyPhone.InsuranceCompanyId = dto.InsuranceCompanyId;
            old_InsuranceCompanyPhone.Phone= dto.Phone;

            var update_InsuranceCompanyPhone = _uintOfWork.InsuranceCompanyPhones.Update(old_InsuranceCompanyPhone);
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseInsuranceCompanyPhoneDto>( update_InsuranceCompanyPhone));
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Phone = await _uintOfWork.InsuranceCompanyPhones.Find(b => b.InsuranceCompanyPhoneId == Id);

            if (Phone == null) return NotFound($"No Phone with Id:{Id}");
                
            Phone = _uintOfWork.InsuranceCompanyPhones.Delete(Phone);
            _uintOfWork.Complete();

            return Ok();
        } 


    }
}
