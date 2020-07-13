using System.Threading.Tasks;
using DesafioLike.Dominio.Entidades;
using DesafioLike.Dominio.IRepositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DesafioLike.Api.Dtos;

namespace DesafioLike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerguntaController : ControllerBase
    {
        private readonly IPerguntaRepositorio _perguntaRepositorio;
        public  readonly IMapper _mapper;
        public PerguntaController(IPerguntaRepositorio perguntaRepositorio, IMapper mapper)
        {
             _mapper = mapper;
            _perguntaRepositorio = perguntaRepositorio;
        }

        ///Busca de Todas as Perguntas
        [Route("v1/obtertodos")]
        [HttpGet]
        public async Task<IActionResult> Obtertodos(){
            try{
                var results = await _perguntaRepositorio.GetAllPerguntaAsync();
                return Ok(results);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");

            }
        }

        ///Busca das perguntas de uma Categoria
        [Route("v1/obterporcategoriaid/{CategoriaId}")]
        [HttpGet]
        public async Task<IActionResult> ObterPorCategoriaId(int CategoriaId){
            try{
                var results = await _perguntaRepositorio.ObterPorCategoriaId(CategoriaId);
                return Ok(results);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");

            }
        }

        ///Busca de pergunta por Id da pergunta
        [Route("v1/obterporid/{PerguntaId}")]
        [HttpGet]
        public async Task<IActionResult> ObterPorId(int PerguntaId){
            try{
                var results = await _perguntaRepositorio.ObterPorPerguntaId(PerguntaId);
                return Ok(results);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");

            }
        }
        /*
        ///Busca das perguntas de uma Categoria não Respondidas
        [Route("v1/obterporcategoriaid/{categoriaid}/")]
        [HttpGet]
        public IActionResult ObterPorCategoriaId(int CategoriaId){
            try{
                var results = _perguntaRepositorio.ObterPorCategoriaId(CategoriaId);
                return Ok(results);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");

            }
        }
*/
        
        ///Adicionar uma Pergunta
        [Route("v1/adicionar")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Adicionar(Pergunta perguntaModel){
            try{
                var pergunta = _mapper.Map<Pergunta>(perguntaModel);
                _perguntaRepositorio.Adicionar(pergunta);
                return Created($"v1/obterporid/{pergunta.Id}", _mapper.Map<PerguntaDto>(pergunta));
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");

            }
            
        }

        ///Atualizar uma Pergunta
        [Route("v1/atualizar")]
        [HttpPost]
        public IActionResult Atualizar(Pergunta pergunta){
            try{
                _perguntaRepositorio.Atualizar(pergunta);
                
                return Created($"api/pergunta/v1/obterporid/{pergunta.Id}", pergunta);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");

            }
            
        }
    }

}