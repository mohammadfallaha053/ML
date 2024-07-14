using ML.Api.Dtos.Analyse;

namespace ML.Api.Dtos.NaturalField
{
    public class ResponseNaturalFieldDto: BaseNaturalFieldDto
    {
        public int NaturalFieldId { get; set; }

        public ResponseAnalyseDto Analyse { get; set; }

    
    }

}
