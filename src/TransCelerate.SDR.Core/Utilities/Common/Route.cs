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
        public const string PostElements = "study";

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
        public const string PostElementsV1 = "v1/studydefinitions";

        public const string SearchElementsV1 = "v1/search";

        public const string SearchTitleV1 = "v1/searchstudytitle";
        #endregion
        #endregion

        #region Study V2 Routes
        #region GET Method Routes        
        public const string StudyV2 = "v2/studydefinitions/{studyId}";

        public const string StudyDesignV2 = "v2/studydesigns";

        public const string AuditTrailV2 = "v2/audittrail/{studyId}";

        public const string StudyHistoryV2 = "/v2/studydefinitions/history";
        #endregion

        #region POST Method Routes
        public const string PostElementsV2 = "v2/studydefinitions";

        public const string SearchElementsV2 = "v2/search";

        public const string SearchTitleV2 = "v2/searchstudytitle";
        #endregion
        #endregion

        #region User Group Routes
        public const string GetGroups = "v1/usergroups/getgroups";

        public const string GetUsers = "v1/usergroups/getusers";

        public const string GetGroupList = "v1/usergroups/getgrouplist";

        public const string GetUsersFromAD = "v1/usergroups/listusers";

        public const string CheckGroupName = "v1/usergroups/checkgroupname";

        public const string PostAGroup = "v1/usergroups/postgroup";

        public const string PostUserToGroups = "v1/usergroups/postuser";
        #endregion

        #region Reports
        public const string SystemUsageReport = "v1/reports/usage";
        #endregion

        #region Token
        public const string Token = "v1/auth/token";
        #endregion

        #region ChangeAudit
        public const string ChangeAudit = "v2/changeaudit/{study_uuid}";
        #endregion

        #region Common Routes
        public const string GetRawJson = "studydefinitions/{studyId}/rawdata";
        #endregion
    }
}
