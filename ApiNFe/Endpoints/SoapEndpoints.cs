using Carter;
using ApiNFe.Services.Interfaces;

namespace ApiNFe.Endpoints
{
    public class SoapEndpoints : CarterModule
    {
        public SoapEndpoints() : base("/soap") { }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", (ISoapService soapService) =>
            {
                return soapService.execute();
            });
        }
    }
}