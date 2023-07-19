using MediatR;
using Prodex.Bussines.Requests;
using Prodex.Bussines.Services;
using Prodex.Data;
using Prodex.Shared.Pagination;
using Prodex.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Bussines.HandlersHelpers
{
    public abstract class BaseGetSelectValuesHandler<TSelect> : IRequestHandler<GetSelectValuesRequest<TSelect>, List<KeyValueResult>>
    {
        private ApiSelectCacheService _apiSelectCache;

        public BaseGetSelectValuesHandler(ApiSelectCacheService apiSelectCache)
        {
            _apiSelectCache = apiSelectCache;
        }

        public Task<List<KeyValueResult>> Handle(GetSelectValuesRequest<TSelect> request, CancellationToken cancellationToken)
            => GetValues(request, cancellationToken);

        public abstract Task<List<KeyValueResult>> GetValues(GetSelectValuesRequest<TSelect> request, CancellationToken cancellationToken);

    }
}
