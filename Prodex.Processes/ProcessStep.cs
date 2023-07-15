using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Processes
{
    public class ProcessStep
    {
        public string Id { get; set; }
        public long StepId { get; set; }
        public string Name { get; set; }
        public StepType StepType { get; set; }
        public List<long> NextSteps { get; set; }
    }

    public enum StepType
    {
        Start,
        ExclusiveGateway,
        UserTask,
        ServiceTask,
        End
    }
}
