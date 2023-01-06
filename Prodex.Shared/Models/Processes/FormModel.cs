using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Models.Processes
{
    public class FormModel : IRequest
    {
        public string Name { get; set; }
        public string Xml { get; set; }
    }
}
