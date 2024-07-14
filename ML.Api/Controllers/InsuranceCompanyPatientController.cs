using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.InsuranceCompanyPatient;
using ML.Core;
using ML.Core.Models;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class InsuranceCompanyPatientController : ControllerBase
    {

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public InsuranceCompanyPatientController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;

        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseInsuranceCompanyPatientDto>>(await _uintOfWork.InsuranceCompanyPatients.GetAllAsync()));




        [HttpGet("{InsuranceCompanyId}/{PatientId}")]
        public async Task<IActionResult> GetByIdAsync( int InsuranceCompanyId, int PatientId)
        {
            var InsuranceCompanyPatient = await _uintOfWork.InsuranceCompanyPatients.Findcompositekey(InsuranceCompanyId, PatientId);

            if (InsuranceCompanyPatient == null) { return NotFound($"No InsuranceCompanyPatient with PatientId:{PatientId}and InsuranceCompanyId:{InsuranceCompanyId}"); }

            return Ok(_mapper.Map<ResponseInsuranceCompanyPatientDto>(InsuranceCompanyPatient));
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateInsuranceCompanyPatientDto dto)
        {


            var InsuranceCompanyPatient = await _uintOfWork.InsuranceCompanyPatients.Add(_mapper.Map<InsuranceCompanyPatient>(dto));
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseInsuranceCompanyPatientDto>(InsuranceCompanyPatient));

        }


        [HttpPut("{InsuranceCompanyId}/{PatientId}")]
        public async Task<IActionResult> Update(int InsuranceCompanyId, int PatientId, [FromBody] UpdateInsuranceCompanyPatientDto dto)
        {

            var old_InsuranceCompanyPatient = await _uintOfWork.InsuranceCompanyPatients.Findcompositekey(InsuranceCompanyId, PatientId);

            if (old_InsuranceCompanyPatient == null) { return NotFound($"No InsuranceCompanyPatient with PatientId:{PatientId}and InsuranceCompanyId:{InsuranceCompanyId}"); }



            old_InsuranceCompanyPatient.Status = dto.Status;
            old_InsuranceCompanyPatient.RigesterDate = dto.RigesterDate;
          
          
            _uintOfWork.Complete();

            var update_InsuranceCompanyPatient = _uintOfWork.InsuranceCompanyPatients.Update(old_InsuranceCompanyPatient);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponseInsuranceCompanyPatientDto>(update_InsuranceCompanyPatient));

        }


        [HttpDelete("{InsuranceCompanyId}/{PatientId}")]
        public async Task<IActionResult> Delete(int InsuranceCompanyId, int PatientId)
        {

            var InsuranceCompanyPatient = await _uintOfWork.InsuranceCompanyPatients.Findcompositekey(InsuranceCompanyId, PatientId);

            if (InsuranceCompanyPatient == null) { return NotFound($"No InsuranceCompanyPatient with PatientId:{PatientId}and InsuranceCompanyId:{InsuranceCompanyId}"); }

            var delete_InsuranceCompanyPatient = _uintOfWork.InsuranceCompanyPatients.Delete(InsuranceCompanyPatient);
            _uintOfWork.Complete();
            return Ok();

        }
      

    }
}
