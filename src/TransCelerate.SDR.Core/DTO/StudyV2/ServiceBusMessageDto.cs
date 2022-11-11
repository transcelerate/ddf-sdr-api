using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ServiceBusMessageDto
    {
        public string Study_uuid { get; set; }
        public int CurrentVersion { get; set; }
    }
}
