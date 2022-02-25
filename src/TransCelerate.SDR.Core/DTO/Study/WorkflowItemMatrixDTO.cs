using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class WorkflowItemMatrixDTO
    {
        public string id { get; set; }
        public List<MatrixDTO> matrix { get; set; }
    }
}
