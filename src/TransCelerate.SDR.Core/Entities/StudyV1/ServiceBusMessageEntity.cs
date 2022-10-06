using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class ServiceBusMessageEntity
    {
        public string Study_uuid { get; set; }
        public int CurrentVersion { get; set; }
    }
}
