using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.BigData;
using ML.Api.Dtos.Doctor;
using ML.Api.Dtos.InsuranceCompany;
using ML.Api.Dtos.Patient;
using ML.Core;
using ML.Core.Interfaces;
using ML.Core.Models;
using ML.EF;
using System.Linq;
using System.Xml.Linq;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class GetBigDataController : ControllerBase

    {


        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GetBigDataController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper=mapper;

        }



        [HttpGet("GetAllDoctorsAndPatientsAndInsuranceCompanies")]
        public async Task<IActionResult> GetAllAsync()
        {
             
            var patients = _mapper.Map<IEnumerable<ResponsePatientDto>>(await _uintOfWork.Patients.GetAllAsync());

            var doctors= _mapper.Map<IEnumerable<ResponseDoctorDto>>(await _uintOfWork.Doctors.GetAllAsync());

            var insuranceCompanies = _mapper.Map<IEnumerable<ResponseInsuranceCompanyDto>>(await _uintOfWork.InsuranceCompanies.GetAllAsync());

            var  all = new ResponseBigDataDto { Doctors = (ICollection<ResponseDoctorDto>)doctors,
                                               Patiens = (ICollection<ResponsePatientDto>)patients,
                                               InsuranceCompanys= (ICollection<ResponseInsuranceCompanyDto>)insuranceCompanies

            };
            
            return Ok(all);

        }




    }
}
