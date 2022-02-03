using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using VersionamentoApi.Attributes;

namespace VersionamentoApi.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(ResponseError))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseError))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseError))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequest))]

 
    [RequiredApiKey]
    public abstract class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CustomResponse(object data)
            => data == null ? NotFound(new ResponseError(404, "Dados Não Encontrado")) : Ok(data);

    }

    public class BadRequest
    {
        public BadRequest(int statusCode, string[] erros)
        {
            Erros = erros;
            StatusCode = statusCode;
        }
        public int StatusCode { get; private set; }
        public string[] Erros { get; private set; }

    }

    public class ResponseError
    {
        public ResponseError(int statusCode, string messsage, string details = null)
        {
            StatusCode = statusCode;
            Messsage = messsage;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Messsage { get; set; }
        public string Details { get; set; }
    }
}
