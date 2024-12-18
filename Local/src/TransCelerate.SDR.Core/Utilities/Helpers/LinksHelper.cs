using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class LinksHelper
    {
        public static LinksDto GetLinks(string studyId, List<string> studyDesignIds, string usdmVersion, int sdruploadversion)
        {
            LinksDto links = new()
            {
                RevisionHistory = $"/studydefinitions/{studyId}/revisionhistory"
            };
            if (usdmVersion == Constants.USDMVersions.MVP)
            {
                links.StudyDefinitions = $"/study/{studyId}?sdruploadversion={sdruploadversion}";
                links.StudyDesigns = GetDesignLinks(studyId, studyDesignIds, usdmVersion, sdruploadversion);
            }
            else
            {
                links.StudyDefinitions = $"/{ApiUsdmVersionMapping.SDRVersions.Where(x => x.UsdmVersions.Contains(usdmVersion)).Select(x => x.ApiVersion).First()}" +
                                         $"/studydefinitions/{studyId}?sdruploadversion={sdruploadversion}";
                links.StudyDesigns = GetDesignLinks(studyId, studyDesignIds, usdmVersion, sdruploadversion);
            }
            return links;
        }
        public static LinksDto GetLinks(string studyId, IEnumerable<string> studyDesignIds, string usdmVersion, int sdruploadversion)
        {
            LinksDto links = new()
            {
                RevisionHistory = $"/studydefinitions/{studyId}/revisionhistory"
            };
            if (usdmVersion == Constants.USDMVersions.MVP)
            {
                links.StudyDefinitions = $"/study/{studyId}?sdruploadversion={sdruploadversion}";
                links.StudyDesigns = GetDesignLinks(studyId, studyDesignIds?.ToList(), usdmVersion, sdruploadversion);
            }
            else
            {
                links.StudyDefinitions = $"/{ApiUsdmVersionMapping.SDRVersions.Where(x => x.UsdmVersions.Contains(usdmVersion)).Select(x => x.ApiVersion).First()}" +
                                         $"/studydefinitions/{studyId}?sdruploadversion={sdruploadversion}";
                links.StudyDesigns = GetDesignLinks(studyId, studyDesignIds?.ToList(), usdmVersion, sdruploadversion);
            }
            return links;
        }

        public static List<StudyDesignLinks> GetDesignLinks(string studyId, List<string> studyDesignIds, string usdmVersion, int sdruploadversion)
        {
            if (studyDesignIds != null && studyDesignIds.Any())
            {
                List<StudyDesignLinks> links = new();
                studyDesignIds.ForEach(designId =>
                {
                    if (usdmVersion == Constants.USDMVersions.MVP)
                    {
                        links.Add(new StudyDesignLinks
                        {
                            StudyDesignId = designId,
                            StudyDesignLink = $"/{studyId}/studydesign/{designId}?sdruploadversion={sdruploadversion}"
                        });
                    }
                    else if (usdmVersion == Constants.USDMVersions.V1)
                    {
                        links.Add(new StudyDesignLinks
                        {
                            StudyDesignId = designId,
                            StudyDesignLink = $"/{ApiUsdmVersionMapping.SDRVersions.Where(x => x.UsdmVersions.Contains(usdmVersion)).Select(x => x.ApiVersion).First()}" +
                                              $"/studydesigns?study_uuid={studyId}&sdruploadversion={sdruploadversion}"
                        });
                    }
                    else
                    {
                        links.Add(new StudyDesignLinks
                        {
                            StudyDesignId = designId,
                            StudyDesignLink = $"/{ApiUsdmVersionMapping.SDRVersions.Where(x => x.UsdmVersions.Contains(usdmVersion)).Select(x => x.ApiVersion).First()}" +
                                              $"/studydesigns?studyid={studyId}&sdruploadversion={sdruploadversion}&studydesignid={designId}"
                        });
                    }
                });
                return links;
            }
            else
                return null;
        }

        public static LinksForUIDto GetLinksForUi(string studyId, List<string> studyDesignIds, string usdmVersion, int sdruploadversion)
        {
            LinksForUIDto links = new()
            {
                RevisionHistory = $"/studydefinitions/{studyId}/revisionhistory"
            };
            if (usdmVersion == Constants.USDMVersions.MVP)
            {
                links.StudyDefinitions = $"/study/{studyId}?sdruploadversion={sdruploadversion}";
                links.StudyDesigns = GetDesignLinks(studyId, studyDesignIds, usdmVersion, sdruploadversion);
                links.SoA = null;
            }
            else
            {
                string apiVersion = ApiUsdmVersionMapping.SDRVersions.Where(x => x.UsdmVersions.Contains(usdmVersion)).Select(x => x.ApiVersion).First();
                links.StudyDefinitions = $"/{apiVersion}" + $"/studydefinitions/{studyId}?sdruploadversion={sdruploadversion}";
                links.StudyDesigns = GetDesignLinks(studyId, studyDesignIds, usdmVersion, sdruploadversion);
                links.SoA = usdmVersion != Constants.USDMVersions.V1 ? $"/{apiVersion}/studydefinitions/{studyId}/studydesigns/soa?sdruploadversion={sdruploadversion}" : null;
            }
            return links;
        }

        public static LinksEndpointDto GetLinksForEndpoint(string studyId, string usdmVersion, int sdruploadversion)
        {
            LinksEndpointDto links = new()
            {
                RevisionHistory = $"/studydefinitions/{studyId}/revisionhistory"
            };
            if (usdmVersion == Constants.USDMVersions.MVP)
            {
                links.StudyDefinitions = $"/study/{studyId}?sdruploadversion={sdruploadversion}";
                links.SoA = null;
            }
            else
            {
                string apiVersion = ApiUsdmVersionMapping.SDRVersions.Where(x => x.UsdmVersions.Contains(usdmVersion)).Select(x => x.ApiVersion).First();
                links.StudyDefinitions = $"/{ApiUsdmVersionMapping.SDRVersions.Where(x => x.UsdmVersions.Contains(usdmVersion)).Select(x => x.ApiVersion).First()}" +
                                         $"/studydefinitions/{studyId}?sdruploadversion={sdruploadversion}";
                links.SoA = usdmVersion != Constants.USDMVersions.V1 ? $"/{apiVersion}/studydefinitions/{studyId}/studydesigns/soa?sdruploadversion={sdruploadversion}" : null;
            }
            return links;
        }
    }
}
