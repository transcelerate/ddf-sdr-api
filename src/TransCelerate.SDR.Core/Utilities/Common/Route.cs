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

        #region Study V1 Routes
        #region GET Method Routes        
        public const string StudyV1 = "v1/studydefinitions/{studyId}";

        public const string StudyDesignV1 = "/v1/studydesigns";

        public const string AuditTrailV1 = "v1/audittrail/{studyId}";

        public const string StudyHistoryV1 = "/v1/studydefinitions/history";
        #endregion

        #region POST Method Routes
        public const string PostElementsV1 = "/v1/studydefinitions";

        public const string SearchElementsV1 = "v1/search";

        public const string SearchTitleV1 = "v1/searchstudytitle";
        #endregion
        #endregion

        #region Study V2 Routes
        #region GET Method Routes        
        public const string StudyV2 = "v2/studydefinitions/{studyId}";

        public const string StudyDesignV2 = "v2/studydesigns";

        public const string SoAV2 = "v2/studydefinitions/{studyId}/studydesigns/soa";

        public const string AuditTrailV2 = "v2/audittrail/{studyId}";

        public const string StudyHistoryV2 = "/v2/studydefinitions/history";

        public const string GeteCPTV2 = "v2/studyDefinitions/{studyId}/studydesigns/eCPT";
        #endregion

        #region POST Method Routes
        public const string PostElementsV2 = "/v2/studydefinitions";

        public const string SearchElementsV2 = "v2/search";

        public const string SearchTitleV2 = "v2/searchstudytitle";
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

        #region User Group Routes
        public const string GetGroups = "usergroups/getgroups";

        public const string GetUsers = "usergroups/getusers";

        public const string GetGroupList = "usergroups/getgrouplist";

        public const string GetUsersFromAD = "usergroups/listusers";

        public const string CheckGroupName = "usergroups/checkgroupname";

        public const string PostAGroup = "usergroups/postgroup";

        public const string PostUserToGroups = "usergroups/postuser";

        public const string RemoveInActiveUsers = "usergroups/removeinactiveusers";
        #endregion

        #region Reports
        public const string SystemUsageReport = "reports/usage";
        #endregion

        #region Token
        public const string Token = "v1/auth/token";
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
        public const string CommonToken = "auth/token";        
        #endregion
    }
}
