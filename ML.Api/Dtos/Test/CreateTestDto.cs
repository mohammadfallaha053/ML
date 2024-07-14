namespace ML.Api.Dtos.Test
{
    public class CreateTestDto : BaseTestDto
    {
        public List<AnalyseIdDto>? TestDetailes { get; set; }

    }
}


public class AnalyseIdDto
{

    public int AnalyseId { get; set; }

    public string? Result { get; set; }

}
