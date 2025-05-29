using Microsoft.AspNetCore.Mvc;
using aplicacao_web.Models;
using aplicacao_web.Services;
using System.ComponentModel.DataAnnotations;

namespace aplicacao_web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmbalagemController : ControllerBase
    {
        private readonly IEmbalagemService _embalagemService;

        public EmbalagemController(IEmbalagemService embalagemService)
        {
            _embalagemService = embalagemService;
        }

        /// <summary>
        /// Processa pedidos e retorna a melhor forma de embalagem
        /// </summary>
        /// <param name="request">Lista de pedidos com produtos e suas dimensões</param>
        /// <returns>Lista de pedidos com as caixas e produtos organizados</returns>
        /// <response code="200">Sucesso - Retorna os pedidos processados</response>
        /// <response code="400">Requisição inválida - Dados de entrada incorretos</response>
        [HttpPost]
        [ProducesResponseType(typeof(PedidosResponse), 200)]
        [ProducesResponseType(400)]
        public ActionResult<PedidosResponse> ProcessarPedidos([FromBody, Required] PedidosRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (request?.Pedidos == null || !request.Pedidos.Any())
                {
                    return BadRequest("Lista de pedidos não pode estar vazia.");
                }

                var response = _embalagemService.ProcessarPedidos(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao processar pedidos: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna informações sobre as caixas disponíveis
        /// </summary>
        /// <returns>Lista das caixas disponíveis</returns>
        [HttpGet("caixas-disponiveis")]
        [ProducesResponseType(typeof(List<CaixaDisponivel>), 200)]
        public ActionResult<List<CaixaDisponivel>> GetCaixasDisponiveis()
        {
            var caixas = new List<CaixaDisponivel>
            {
                new() { Id = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 },
                new() { Id = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 },
                new() { Id = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 }
            };

            return Ok(caixas);
        }
    }
} 