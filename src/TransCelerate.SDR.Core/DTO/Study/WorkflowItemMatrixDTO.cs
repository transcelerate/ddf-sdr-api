using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class WorkflowItemMatrixDTO
    {
        public string Id { get; set; }
        public List<MatrixDTO> Matrix { get; set; }
    }
}
