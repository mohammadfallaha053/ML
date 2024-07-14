using ML.Api.Dtos.Analyse;
using ML.Api.Dtos.Test;

namespace ML.Api.Dtos.TestDetail
{
    public class ResponseTestDetailDto: BaseTestDetailDto
    {
        public int TestDetailId { get; set; }


       public ResponseAnalyseDto Analyse { get; set; }

       public ResponseTestDto Test { get; set; }
        

    }
}
