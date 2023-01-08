using MediatR;

namespace Prodex.Bussines.Requests
{
    public class CreateRequest<TForm, TResponse> : IRequest<TResponse>
    {
        public TForm Form { get; set; }
        
        public CreateRequest(TForm form)
        {
            Form = form;
        }
    }
}
