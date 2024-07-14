namespace ML.Api.Dtos.Classification
{
    public class HeartAttackDto:BaseClassificationDto
    {
        public double? Troponin { get; set; }

        public double? Glucose { get; set; }

        public int? Impluse { get; set; }

        public bool? Class { get; set; }
    }
}
