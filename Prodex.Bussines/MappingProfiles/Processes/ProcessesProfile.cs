using AutoMapper;
using Prodex.Data.Models;
using Prodex.Shared.Models.Processes;

namespace Prodex.Bussines.MappingProfiles.Processes;

public class ProcessesProfile : Profile
{
    public ProcessesProfile()
    {
        CreateMap<Process, ListItemModel>();

        CreateMap<FormModel, Process>()
            .ForMember(p => p.Id, o => o.Ignore());
    }
}
