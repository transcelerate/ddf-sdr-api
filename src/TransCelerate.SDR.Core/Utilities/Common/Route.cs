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
        public const string Token = "auth/token";
    }
}
