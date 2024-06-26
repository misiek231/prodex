﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Interfaces;
using Prodex.Data.Models;
using Prodex.Server.Extensions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Pagination;
using System.Security.Claims;

namespace Prodex.Server.EndpointDefinitions
{
    public abstract class SimpleRequestsBaseDefinition
    {
        public void DefineGetList<TEntity, TFilterModel, TListItemModel>(RouteGroupBuilder group) where TEntity : class
        {
            group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] TFilterModel model) =>
                await mediator.Send(new SimpleGetList.Request<TEntity, TFilterModel, TListItemModel>(pager, model)))
                .RequireAuthorization();
        }

        public void DefineGetDetails<TEntity, TDetailsModel>(RouteGroupBuilder group) where TEntity : class
        {
            group.MapGet("{id}", async (IMediator mediator, ClaimsPrincipal user, [FromRoute] long id) => 
                await mediator.Send(new SimpleGetDetails.Request<TEntity, TDetailsModel>(id, user.Id())))
                .RequireAuthorization();
        }

        public void DefineCreate<TEntity, TFormModel>(RouteGroupBuilder group) where TEntity : class, IEntity
        {
            group.MapPost("", async (IMediator mediator, ClaimsPrincipal user, [FromBody] TFormModel model) => {
                var result = await mediator.Send(new SimpleCreate.Request<TEntity, TFormModel>(model, user.Id()));

                return result.Match(p => Results.Ok(p.Id), p => Results.BadRequest(p));
            })
            .RequireAuthorization();
        }

        public void DefineUpdate<TEntity, TFormModel>(RouteGroupBuilder group) where TEntity : class, IEntity
        {
            group.MapPut("{id}", async (IMediator mediator, ClaimsPrincipal user, [FromRoute] long id, [FromBody] TFormModel model) =>
            {
                var result = await mediator.Send(new SimpleUpdate.Request<TEntity, TFormModel>(id, model, user.Id()));

                return result.Match(p => Results.Ok(p.Id), p => Results.BadRequest(p));
            })
            .RequireAuthorization();
        }
    }
}
