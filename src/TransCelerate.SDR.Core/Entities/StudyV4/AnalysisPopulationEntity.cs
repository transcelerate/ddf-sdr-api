namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AnalysisPopulationEntity : IId
    {        
        public string Id { get; set; }
        public string PopulationDescription { get; set; }
    }
}
