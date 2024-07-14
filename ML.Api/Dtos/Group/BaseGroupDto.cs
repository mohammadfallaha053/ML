using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.Group
{
    public class BaseGroupDto
    {
        [MaxLength(100)]
        public string GroupName { get; set; }
    }
}
