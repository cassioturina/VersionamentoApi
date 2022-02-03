using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using VersionamentoApi.Controllers;
using VersionamentoApi.Entidades;

namespace VersionamentoApi.V1.Controllers
{

    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/clientes")]
    public class ClientesController : BaseController
    {
        private readonly IList<Cliente> _clientes;
        public ClientesController()
        {
            _clientes = new List<Cliente>
            {
                new Cliente(1,"Cliente 1"),
                new Cliente(2,"Cliente 2"),
                new Cliente(3,"Cliente 3"),
             };
        }


        [Obsolete(message: "Método Obsoleto")]
        [HttpGet("id/{id:int}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cliente))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult Get(int id)
        {
            var cliente = _clientes.FirstOrDefault(x => x.Id == id);
            return CustomResponse(cliente);
        }


        [HttpPost("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequest))]

        public async Task<IActionResult> Cadastrar([FromBody] CadastrarClienteDois model)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                return BadRequest(new BadRequest(StatusCodes.Status400BadRequest, erros));
            }


            var cliente = new Cliente(_clientes.Count, $"{model.Nome} {_clientes.Count}");

            return CustomResponse(cliente);
        }


    }
}
