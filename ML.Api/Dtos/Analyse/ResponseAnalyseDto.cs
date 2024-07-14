    using ML.Api.Dtos.Category;
using ML.Api.Dtos.Group;
using ML.Api.Dtos.NaturalField;

namespace ML.Api.Dtos.Analyse
{
    public class ResponseAnalyseDto :BaseAnalyseDto
    {
        public int AnalyseId { get; set; }

        public ResponseGroupDto  Group { get; set; }

        public ResponseCategoryDto Category { get; set; }

        public ICollection<ResponseNaturalFieldDto> NaturalFields { get; set; }

    }
}
