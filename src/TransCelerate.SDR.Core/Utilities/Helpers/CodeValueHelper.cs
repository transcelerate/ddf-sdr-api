using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class CodeValueHelper
    {
        public static string GetStudyTitle(this List<Core.DTO.StudyV4.StudyTitleDto> studyTitles, string decode)
        {
            if (studyTitles is not null && studyTitles.Any() && studyTitles.Any(x => x.Type.Decode == decode))
            {
                return studyTitles.Find(x => x.Type?.Decode == decode).Text;
            }
            return null;
        }

        public static string GetStudyTitle(this List<Core.Entities.StudyV4.StudyTitleEntity> studyTitles, string decode)
        {
            if (studyTitles is not null && studyTitles.Any() && studyTitles.Any(x => x.Type.Decode == decode))
            {
                return studyTitles.Find(x => x.Type?.Decode == decode).Text;
            }
            return null;
        }

        public static string GetStudyTitle(this List<Core.Entities.Common.CommonStudyTitle> studyTitles, string decode)
        {
            if (studyTitles is not null && studyTitles.Any() && studyTitles.Any(x => x.Type.Decode == decode))
            {
                return studyTitles.Find(x => x.Type?.Decode == decode).Text;
            }
            return null;
        }
        public static string GetProtocolEffectiveDate(this Core.DTO.StudyV4.StudyProtocolDocumentVersionDto protocolVersion, string decode)
        {
            if (protocolVersion is not null && protocolVersion.DateValues is not null && protocolVersion.DateValues.Any() && protocolVersion.DateValues.Any(x => x.Type.Decode == decode))
            {
                return protocolVersion.DateValues.Find(x => x.Type?.Decode == decode).DateValue;
            }
            return null;
        }

        public static bool IsSponsorDecode(string decode)
        {
            if (string.IsNullOrEmpty(decode))
                return false;

            return Constants.IdType.SponsorIdentifierConstants.Contains(decode, StringComparer.OrdinalIgnoreCase);
        }
    }
}
