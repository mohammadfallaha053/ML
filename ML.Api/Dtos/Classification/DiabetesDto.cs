namespace ML.Api.Dtos.Classification
{
    public class DiabetesDto:BaseClassificationDto
    {

        public double? Glucose { get; set; }

        public double? HbA1cLevel { get; set; }

        public int? Hypertension { get; set; } 

        public int? HeartDisease { get; set; }

       


      

    }
}
