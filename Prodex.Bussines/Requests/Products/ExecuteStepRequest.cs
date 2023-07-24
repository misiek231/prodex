using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Bussines.Requests.Products
{
    public class ExecuteStepRequest : IRequest<object>
    {
        public long ProductId { get; set; }
        public long StepId { get; set; }

        public ExecuteStepRequest(long productId, long stepId)
        {
            ProductId = productId;
            StepId = stepId;
        }
    }
}
