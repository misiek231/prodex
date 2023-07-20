using AutoMapper;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Bussines.Services;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Users;

namespace Prodex.Bussines.Handlers.Users;

public class CreateHandler : BaseCreateHandler<FormModel, object>
{
    private readonly DataContext context;
    private readonly IMapper mapper;
    private readonly PasswordHasher hasher;

    public CreateHandler(DataContext context, IMapper mapper, PasswordHasher hasher)
    {
        this.context = context;
        this.mapper = mapper;
        this.hasher = hasher;
    }

    public override async Task<object> Create(FormModel form, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(form);

        user.Password = hasher.HashPassword(form.Password);
        
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        return null; // Todo: return detils model
    }

    public override Task<object> Update(long id, FormModel form, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
