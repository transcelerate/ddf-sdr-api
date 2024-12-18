using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class CodeValueHelperV5
    {
        public static string GetStudyTitleV5(this List<Core.DTO.StudyV5.StudyTitleDto> studyTitles, string decode)
        {
            if (studyTitles is not null && studyTitles.Any() && studyTitles.Any(x => x.Type.Decode == decode))
            {
                return studyTitles.Find(x => x.Type?.Decode == decode).Text;
            }
            return null;
        }

        public static string GetStudyTitleV5(this List<Core.Entities.StudyV5.StudyTitleEntity> studyTitles, string decode)
        {
            if (studyTitles is not null && studyTitles.Any() && studyTitles.Any(x => x.Type.Decode == decode))
            {
                return studyTitles.Find(x => x.Type?.Decode == decode).Text;
            }
            return null;
        }

        public static string GetStudyTitleV5(this List<Core.Entities.Common.CommonStudyTitle> studyTitles, string decode)
        {
            if (studyTitles is not null && studyTitles.Any() && studyTitles.Any(x => x.Type.Decode == decode))
            {
                return studyTitles.Find(x => x.Type?.Decode == decode).Text;
            }
            return null;
        }
        public static string GetProtocolEffectiveDate(this Core.DTO.StudyV5.StudyDefinitionDocumentVersionDto protocolVersion, string decode)
        {
            if (protocolVersion is not null && protocolVersion.DateValues is not null && protocolVersion.DateValues.Any() && protocolVersion.DateValues.Any(x => x.Type.Decode == decode))
            {
                return protocolVersion.DateValues.Find(x => x.Type?.Decode == decode).DateValue;
            }
            return null;
        }
    }
}
