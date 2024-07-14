namespace ML.Api.Dtos.NaturalField
{
    public class BaseNaturalFieldDto
    {

        

        public double Max { get; set; }

        public double Min { get; set; }

        public int AnalyseId { get; set; }

        public GenderType? Gender { get; set; }

    }
}

public enum GenderType
{
    Male = 0,
    Female = 1,
    Child = 2
}



