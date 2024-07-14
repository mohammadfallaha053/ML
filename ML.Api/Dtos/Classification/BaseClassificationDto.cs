namespace ML.Api.Dtos.Classification
{
    public class BaseClassificationDto
    {
        public int ? Id { get; set; }
        public int ? Age { get; set; }

        public int ? Gender { get; set; }

        public bool? Class { get; set; }
    }
}
