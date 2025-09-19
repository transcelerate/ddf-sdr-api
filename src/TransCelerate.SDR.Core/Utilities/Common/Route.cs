namespace TransCelerate.SDR.Core.Utilities.Common
{
    /// <summary>
    /// This class holds list of routes for all the endpoints
    /// </summary>
    public static class Route
    {
        #region Study Routes
        #region GET Method Routes        
        public const string Study = "study/{studyId}";

        public const string StudyDesign = "{studyId}/studydesign/{studyDesignId}";

        public const string AuditTrail = "audittrail/{studyId}";

        public const string StudyHistory = "studyhistory";
        #endregion

        #region POST Method Routes
        public const string PostElements = "/study";

        public const string SearchElements = "search";

        public const string SearchTitle = "searchstudytitle";
        #endregion
        #endregion

        #region Study V3 Routes
        #region GET Method Routes
        public const string StudyV3 = "v3/studydefinitions/{studyId}";

        public const string StudyDesignV3 = "v3/studydesigns";

        public const string SoAV3 = "v3/studydefinitions/{studyId}/studydesigns/soa";

        public const string AuditTrailV3 = "v3/audittrail/{studyId}";

        public const string GeteCPTV3 = "v3/studyDefinitions/{studyId}/studydesigns/eCPT";

        public const string VersionCompareV3 = "v3/studydefinitions/{studyId}/version-comparison";
        #endregion

        #region POST Method Routes
        public const string PostElementsV3 = "/v3/studydefinitions";

        public const string ValidateUsdmConformanceV3 = "/v3/studydefinitions/validate-usdm-conformance";
        #endregion
        #endregion

        #region Study V4 Routes
        #region GET Method Routes
        public const string StudyV4 = "v4/studydefinitions/{studyId}";

        public const string StudyDesignV4 = "v4/studydesigns";

        public const string SoAV4 = "v4/studydefinitions/{studyId}/studydesigns/soa";

        public const string AuditTrailV4 = "v4/audittrail/{studyId}";

        public const string GeteCPTV4 = "v4/studyDefinitions/{studyId}/studydesigns/eCPT";

        public const string VersionCompareV4 = "v4/studydefinitions/{studyId}/version-comparison";
        #endregion

        #region POST Method Routes
        public const string PostElementsV4 = "/v4/studydefinitions";

        public const string ValidateUsdmConformanceV4 = "/v4/studydefinitions/validate-usdm-conformance";
        #endregion
        #endregion

        #region Study V5 Routes
        #region GET Method Routes
        public const string StudyV5 = "v5/studydefinitions/{studyId}";

        public const string StudyDesignV5 = "v5/studydesigns";

        public const string SoAV5 = "v5/studydefinitions/{studyId}/studydesigns/soa";

        public const string AuditTrailV5 = "v5/audittrail/{studyId}";

        public const string GeteCPTV5 = "v5/studyDefinitions/{studyId}/studydesigns/eCPT";

        public const string VersionCompareV5 = "v5/studydefinitions/{studyId}/version-comparison";
        #endregion

        #region POST Method Routes
        public const string PostElementsV5 = "/v5/studydefinitions";

        public const string ValidateUsdmConformanceV5 = "/v5/studydefinitions/validate-usdm-conformance";
        #endregion
        #endregion

        #region ChangeAudit
        public const string ChangeAudit = "studydefinitions/{studyId}/changeaudit";
        #endregion

        #region Common Routes
        public const string GetRawJson = "studydefinitions/{studyId}/rawdata";
        public const string GetApiUsdmMapping = "versions";
        public const string GetRevisionHistory = "studydefinitions/{studyId}/revisionhistory";
        public const string SearchStudyTitle = "studydefinitions/searchstudytitle";
        public const string GetStudyHistory = "studydefinitions/history";
        public const string CommonSearch = "studydefinitions/search";
        public const string GetLinksForAStudy = "studydefinitions/{studyId}/links";
        #endregion
    }
}
