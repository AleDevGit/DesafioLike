using System;
using System.Threading.Tasks;
using AutoMapper;
using DesafioLike.Api.Dtos;
using DesafioLike.Dominio.Entidades;
using DesafioLike.Dominio.IRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioLike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespostaController  : ControllerBase
    {
         private readonly IRespostaRepositorio _respostaRepositorio;
         public  readonly IMapper _mapper;
        public RespostaController(IRespostaRepositorio respostaRepositorio, IMapper mapper)
        { 
            _mapper = mapper;
            _respostaRepositorio = respostaRepositorio;
        }
        
        ///Busca das perguntas de uma Categoria
        [Route("v1/obterporperguntaid/{perguntaid}")]
        [HttpGet]
        public IActionResult ObterPorPerguntaId(int PerguntaId){
            try{
                var results = _respostaRepositorio.ObterPorPerguntaId(PerguntaId);
                return Ok(results);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");

            }
        }

        ///Adicionar uma Resposta
        [Route("v1/adicionar")]
        [HttpPost]
        public IActionResult Adicionar(RespostaDto respostaModel){
            try{
                var resposta = _mapper.Map<Resposta>(respostaModel);
                _respostaRepositorio.Adicionar(resposta);
                respostaModel = _mapper.Map<RespostaDto>(resposta);
                return Created($"v1/obterporperguntaid/{resposta.PerguntaId}", respostaModel);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");
            }
        }

        ///Atualizar uma Resposta
        [Route("v1/atualizar")]
        [HttpPost]
        public IActionResult Atualizar(Resposta resposta){
            try{
                _respostaRepositorio.Atualizar(resposta);
                
                return Created($"api/resposta/v1/obterporid/{resposta.Id}", resposta);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");

            }
            
        }
    
    }
}