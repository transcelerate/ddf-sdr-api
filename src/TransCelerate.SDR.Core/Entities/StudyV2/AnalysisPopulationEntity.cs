namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AnalysisPopulationEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.AnalysisPopulationId)]
        public string Id { get; set; }
        public string PopulationDescription { get; set; }
    }
}
