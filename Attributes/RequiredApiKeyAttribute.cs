using ACCVeiculos.Core.Configuracoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using VersionamentoApi.Constantes;

namespace VersionamentoApi.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiredApiKeyAttribute : Attribute, IAsyncActionFilter
    {


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers
                 .TryGetValue(ApiConstantes.API_KEY_NAME, out var apiKey))
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = "API Requer autenticação"
                };
                return;
            }

            var clienteConfiguration = context.HttpContext.RequestServices.GetRequiredService<IClienteConfiguration>();

            //var key = appSettings.GetValue<string>(ApiConstantes.API_KEY_NAME);

            if (!clienteConfiguration.Clientes.Any(c => c.Key == apiKey))
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = "Acesso não autorizado"
                };
                return;
            }

            await next();
        }
    }
}
