namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class IngredientDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public SubstanceDto Substance { get; set; }
        public CodeDto Role { get; set; }
    }
}
