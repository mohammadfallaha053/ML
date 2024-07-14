using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.Category
{
    public class BaseCategoryDto
    {
        [MaxLength(100)]
        public string CategoryName { get; set; }
    }
}
