﻿using Carter;
using pocApiSefaz.Services.Interfaces;

namespace pocApiSefaz.Endpoints
{
    public class SoapEndpoints : CarterModule
    {
        public SoapEndpoints() : base("/soap") { }


        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", (ISoapService soapService) =>
            {
                var todos = soapService.GetAll();

                return TypedResults.Ok(todos);
            });
        }
    }
}