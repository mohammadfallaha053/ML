using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.InsuranceCompany;
using ML.Core;
using ML.Core.Models;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class InsuranceCompanyController : ControllerBase
    {

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public InsuranceCompanyController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;

        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseInsuranceCompanyDto>>(await _uintOfWork.InsuranceCompanies.GetAllAsync(new[] { "InsuranceCompanyPhones" })));


        [HttpGet("WithPatients")]
        public async Task<IActionResult> GetAllWithPatientsAsync() => Ok(_mapper.Map<IEnumerable<ResponseInsuranceCompanyWithPatientDto>>(await _uintOfWork.InsuranceCompanies.GetAllAsync(new[] { "InsuranceCompanyPhones", "Patients" })));


        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var InsuranceCompany = await _uintOfWork.InsuranceCompanies.Find(b => b.InsuranceCompanyId == GetById, new[] { "InsuranceCompanyPhones" });

            if (InsuranceCompany == null) { return NotFound($"No InsuranceCompany with ID:{GetById}"); }

            return Ok(_mapper.Map<ResponseInsuranceCompanyDto>(InsuranceCompany));
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateInsuranceCompanyDto dto)
        {


            var InsuranceCompany = await _uintOfWork.InsuranceCompanies.Add(_mapper.Map<InsuranceCompany>(dto));
            _uintOfWork.Complete();

            var insuranceCompanyId = InsuranceCompany.InsuranceCompanyId;
            var Phones = dto.Phones;
            if (Phones != null)
            {
                foreach (var Phone in Phones) await _uintOfWork.InsuranceCompanyPhones.Add(new InsuranceCompanyPhone { InsuranceCompanyId = insuranceCompanyId, Phone = Phone.Phone });

            }
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseInsuranceCompanyDto>(InsuranceCompany));

        }



        
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateInsuranceCompanyDto dto)
        {
            var old_InsuranceCompany = await _uintOfWork.InsuranceCompanies.Find(b => b.InsuranceCompanyId == Id);
            if (old_InsuranceCompany == null) return NotFound($"No Doctor with Id:{Id}");
                

            old_InsuranceCompany.Email = dto.Email;
            old_InsuranceCompany.Name = dto.Name;   
            _uintOfWork.Complete();

            var update_InsuranceCompany = _uintOfWork.InsuranceCompanies.Update(old_InsuranceCompany);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponseInsuranceCompanyDto>(update_InsuranceCompany));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var InsuranceCompany = await _uintOfWork.InsuranceCompanies.Find(b => b.InsuranceCompanyId == Id);

            if (InsuranceCompany == null) return NotFound($"No InsuranceCompany with Id:{Id}");

            var delete_InsuranceCompany = _uintOfWork.InsuranceCompanies.Delete(InsuranceCompany);
            _uintOfWork.Complete();
            return Ok();

        }


    }
}
