using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Utilities.Common
{
    public static class Route
    {
        #region GET Method Routes
        #region Depricated Routes
        //public const string InterventionModel = "{study}/elements/interventionmodel";

        //public const string Investigationalinterventions = "{study}/elements/investigationalinterventions";

        //public const string StudyIdentifiers = "{study}/elements/studyidentifiers";

        //public const string StudyPhase = "{study}/elements/studyphase";

        //public const string StudyProtocol = "{study}/elements/studyprotocol";

        //public const string StudyObjectives = "{study}/elements/studyobjectives";

        //public const string StudyTargetPopulation = "{study}/elements/studytargetpopulations";

        //public const string StudyTitle = "{study}/elements/studytitle";

        //public const string StudyType = "{study}/elements/studytype";

        //public const string StudyIndication = "{study}/elements/studyindications"; 
        #endregion

        public const string Study = "study/{studyId}"; 

        public const string StudyDesign = "{studyId}/studydesign/{studyDesignId}"; 

        public const string AuditTrail = "audittrail/{studyId}";
        #endregion

        #region POST Method Routes
        public const string PostElements = "study";
        
        public const string SearchElements = "search";
        #endregion
    }
}
