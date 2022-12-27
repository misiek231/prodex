using Microsoft.AspNetCore.Mvc;
using Prodex.Data;
using Prodex.Shared;
using Prodex.Shared.Models.Processes;

namespace Prodex.Server.Controllers
{
    [ApiController]
    [Route("api/processes")]
    public class ProcessesController : ControllerBase
    {
        private readonly DataContext Context;

        public ProcessesController(DataContext context)
        {
            Context = context;
        }

        [HttpGet]
        public List<ListItemModel> GetList()
        {
            return Context.Processes.Select(p => new ListItemModel
            {
                Id= p.Id,
                Name= p.Name,
                Xml = p.Xml,
            }).ToList();
        }

        [HttpPost]
        public ActionResult Create(FormModel model)
        {
            var p = new Process
            {
                Name = model.Name,
                Xml = model.Xml,
            };

            Context.Processes.Add(p);

            Context.SaveChanges();

            return Ok();
        }
    }
}