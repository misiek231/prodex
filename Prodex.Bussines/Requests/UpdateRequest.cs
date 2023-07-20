using MediatR;

namespace Prodex.Bussines.Requests
{
    public class UpdateRequest<TForm, TResponse> : IRequest<TResponse>
    {
        public long Id { get; set; }
        public TForm Form { get; set; }
        
        public UpdateRequest(long id, TForm form)
        {
            Id = id;
            Form = form;
        }
    }
}
