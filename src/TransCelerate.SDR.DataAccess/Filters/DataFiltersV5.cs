using MongoDB.Driver;
using System;
using System.Linq;
using TransCelerate.SDR.Core.Entities.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.DataAccess.Filters
{
    /// <summary>
    /// DataFilters for getting data from data base
    /// </summary>
    public static class DataFiltersV5
    {
        /// <summary>
        /// Get filters for GET StudyDefinitons API
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="sdruploadversion"></param>
        /// <returns></returns>
        public static FilterDefinition<StudyDefinitionsEntity> GetFiltersForGetStudy(string studyId, int sdruploadversion)
        {
            FilterDefinitionBuilder<StudyDefinitionsEntity> builder = Builders<StudyDefinitionsEntity>.Filter;
            FilterDefinition<StudyDefinitionsEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.AuditTrail.UsdmVersion == Constants.USDMVersions.V4);
            filter &= builder.Where(s => s.Study.Id == studyId);

            if (sdruploadversion != 0)
                filter &= builder.Where(x => x.AuditTrail.SDRUploadVersion == sdruploadversion);

            return filter;
        }

        /// <summary>
        /// Get filters for AuditTrail 
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="sdruploadversion"></param>
        /// <returns></returns>
        public static FilterDefinition<StudyDefinitionsEntity> GetFiltersForGetAuditTrailOfAStudy(string studyId, int sdruploadversion)
        {
            FilterDefinitionBuilder<StudyDefinitionsEntity> builder = Builders<StudyDefinitionsEntity>.Filter;
            FilterDefinition<StudyDefinitionsEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.Study.Id == studyId);

            if (sdruploadversion != 0)
                filter &= builder.Where(x => x.AuditTrail.SDRUploadVersion == sdruploadversion);

            return filter;
        }
        /// <summary>
        /// Get projectio definition for partial study elements
        /// </summary>
        /// <param name="listofelementsArray">list of study elements</param>
        /// <returns></returns>
        public static ProjectionDefinition<StudyDefinitionsEntity> GetProjectionForPartialStudyElements(string[] listofelementsArray)
        {
            ProjectionDefinitionBuilder<StudyDefinitionsEntity> projection = Builders<StudyDefinitionsEntity>.Projection;
            ProjectionDefinition<StudyDefinitionsEntity> projector = projection.Include(x => x.Study.Versions.Select(x => x.Id));
            projector = projector.Include(x => x.Study.Versions.Select(x => x.StudyType));
            projector = projector.Include(x => x.AuditTrail);
            projector = projector.Exclude(x => x.Id);

            listofelementsArray.ToList().ForEach(elements =>
            {
                if (elements is not null)
                {
                    if (elements.ToLower().Equals(nameof(StudyVersionEntity.Titles).ToLower()))
                        projector = projector.Include(x => x.Study.Versions.Select(x => x.Titles));
                    else if (elements.ToLower().Equals(nameof(StudyVersionEntity.VersionIdentifier).ToLower()))
                        projector = projector.Include(x => x.Study.Versions.Select(x => x.VersionIdentifier));
                    else if (elements.ToLower().Equals(nameof(StudyVersionEntity.DocumentVersionIds).ToLower()))
                        projector = projector.Include(x => x.Study.Versions.Select(x => x.DocumentVersionIds));
                    else if (elements.ToLower().Equals(nameof(StudyVersionEntity.StudyIdentifiers).ToLower()))
                        projector = projector.Include(x => x.Study.Versions.Select(x => x.StudyIdentifiers));
                    else if (elements.ToLower().Equals(nameof(StudyVersionEntity.StudyDesigns).ToLower()))
                        projector = projector.Include(x => x.Study.Versions.Select(x => x.StudyDesigns));
                    else if (elements.ToLower().Equals(nameof(StudyVersionEntity.BusinessTherapeuticAreas).ToLower()))
                        projector = projector.Include(x => x.Study.Versions.Select(x => x.BusinessTherapeuticAreas));
                    else if (elements.ToLower().Equals(nameof(StudyVersionEntity.Rationale).ToLower()))
                        projector = projector.Include(x => x.Study.Versions.Select(x => x.Rationale));
                    else if (elements.ToLower().Equals(nameof(StudyVersionEntity.DateValues).ToLower()))
                        projector = projector.Include(x => x.Study.Versions.Select(x => x.DateValues));
                    else if (elements.ToLower().Equals(nameof(StudyVersionEntity.Amendments).ToLower()))
                        projector = projector.Include(x => x.Study.Versions.Select(x => x.Amendments));
                }
            });
            return projector;
        }

        /// <summary>
        /// Get Study Design Projection Definition
        /// </summary>
        /// <returns></returns>
        public static ProjectionDefinition<StudyDefinitionsEntity> GetProjectionForPartialStudyDesignElementsFullStudy()
        {
            ProjectionDefinitionBuilder<StudyDefinitionsEntity> projection = Builders<StudyDefinitionsEntity>.Projection;
            ProjectionDefinition<StudyDefinitionsEntity> projector = projection.Include(x => x.Study.Versions.Select(x => x.Id));
            projector = projector.Include(x => x.Study.Versions.Select(x => x.StudyType));            
            
            projector = projector.Include(x => x.AuditTrail);
            projector = projector.Exclude(x => x.Id);

            return projector;
        }

        public static ProjectionDefinition<StudyDefinitionsEntity> GetProjectionForCheckAccessForAStudy()
        {
            ProjectionDefinitionBuilder<StudyDefinitionsEntity> projection = Builders<StudyDefinitionsEntity>.Projection;
            ProjectionDefinition<StudyDefinitionsEntity> projector = projection.Include(x => x.Study.Versions.Select(x => x.Id));
            projector = projector.Include(x => x.Study.Versions.Select(x => x.StudyType));
            projector = projector.Exclude(x => x.Id);

            return projector;
        }
    }
}
