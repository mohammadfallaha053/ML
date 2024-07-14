namespace ML.Api.Dtos.Classification
{
    public class CKDDto :BaseClassificationDto
    {
        public double? SerumCreatinine { get; set; }

        public double? Potassium { get; set; }

        public double? Hemoglobin { get; set; }

        public bool? DiabetesMellitus { get; set; }

        public bool? Appetite { get; set; }

        public int? BloodUrea { get; set; }
    }
}
