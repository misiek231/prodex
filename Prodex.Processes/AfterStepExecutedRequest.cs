using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Processes
{
    public class AfterStepExecutedRequest : IRequest<object>
    {
        public IProcessable Processable { get; set; }
        public ProcessStep ExecutedStep { get; set; }
        public long UserId { get; set; }
        
        public AfterStepExecutedRequest(IProcessable processable, ProcessStep executedStep, long userId)
        {
            Processable = processable;
            ExecutedStep = executedStep;
            UserId = userId;
        }
    }
}
