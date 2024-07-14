
using ML.Api.Dtos.LaboratoryConstantPhone;

namespace ML.Api.Dtos.LaboratoryConstant
{
    public class ResponseLaboratoryConstantDto : BaseLaboratoryConstantDto
    {
        public int LaboratoryConstantId { get; set; }

        public List<ResponseLaboratoryConstantPhoneDto> LaboratoryConstantPhones { get; set; }

    }
}
