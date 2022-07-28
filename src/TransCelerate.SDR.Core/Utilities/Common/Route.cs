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

        #region User Group Routes
        public const string GetGroups = "usergroups/getgroups";

        public const string GetUsers = "usergroups/getusers";

        public const string GetGroupList = "usergroups/getgrouplist";

        public const string GetUsersFromAD = "usergroups/listusers";

        public const string CheckGroupName = "usergroups/checkgroupname";

        public const string PostAGroup = "usergroups/postgroup";

        public const string PostUserToGroups = "usergroups/postuser";
        #endregion

        #region Reports
        public const string SystemUsageReport = "reports/usage";
        #endregion

        #region Token
        public const string Token = "auth/token";
        #endregion
    }
}
