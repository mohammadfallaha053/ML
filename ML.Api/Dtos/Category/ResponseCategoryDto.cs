using ML.Api.Dtos.Analyse;
using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.Category
{
    public class ResponseCategoryDto: BaseCategoryDto
    {
        public int CategoryId { get; set; }
       
       public ICollection<ResponseAnalyseDto>Analyses { get; set; }



    }
}
