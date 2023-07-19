using AutoMapper;
using Prodex.Data.Models;
using Prodex.Shared.Models.Processes;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.MappingProfiles.Processes;

public class ApiSelectProfile : Profile
{
    public ApiSelectProfile()
    {
        CreateMap<IApiSelect, long>().ConvertUsing<IApiSelectConverter>();
    }
}

class IApiSelectConverter : ITypeConverter<IApiSelect, long>, ITypeConverter<IApiSelect, long?>
{
    public long Convert(IApiSelect source, long destination, ResolutionContext context)
    {
        return source.Id;
    }

    public long? Convert(IApiSelect source, long? destination, ResolutionContext context)
    {
        return source?.Id;
    }
}
