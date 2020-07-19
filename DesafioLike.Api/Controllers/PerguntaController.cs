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

        ///<summary>
        ///Busca de Todas as Perguntas
        ///</summary>
        [Authorize(Roles = "Admin, Operador")]
        [Route("v1/obtertodos")]
        [HttpGet]
        public async Task<IActionResult> Obtertodos(){
            try{
                var results = await _perguntaRepositorio.GetAllPerguntaAsync();
                var resultsPergunta = _mapper.Map<PerguntaDto[]>(results);
                return Ok(resultsPergunta);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");
            }
        }

        ///<summary>
        ///Busca das perguntas de uma Categoria
        ///</summary>
        [Authorize(Roles = "Admin, Operador")]
        [Route("v1/obterporcategoriaid/{CategoriaId}")]
        [HttpGet]
        public async Task<IActionResult> ObterPorCategoriaId(int CategoriaId){
            try{
                var results = await _perguntaRepositorio.ObterPorCategoriaId(CategoriaId);
                var resultsPergunta = _mapper.Map<PerguntaDto[]>(results);
                return Ok(resultsPergunta);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");
            }
        }

        ///<summary>
        ///Busca de pergunta por Id da pergunta
        ///</summary>
        [Authorize(Roles = "Admin, Operador")]
        [Route("v1/obterporid/{PerguntaId}")]
        [HttpGet]
        public async Task<IActionResult> ObterPorId(int PerguntaId){
            try{
                var results = await _perguntaRepositorio.ObterPorPerguntaId(PerguntaId);
                var resultsPergunta = _mapper.Map<PerguntaDto[]>(results);
                return Ok(results);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");
            }
        }
        
        ///Adicionar uma Pergunta
        [Authorize(Roles = "Admin, Operador")]
        [Route("v1/adicionar")]
        [HttpPost]
        public IActionResult Adicionar(PerguntaDto perguntaModel){
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
        [Authorize(Roles = "Admin, Operador")]
        [Route("v1/atualizar/{PerguntaId}")]
        [HttpPost]
        public IActionResult Atualizar(int PerguntaId, PerguntaDto perguntaModel){
            try{
                var pergunta =  _perguntaRepositorio.ObterPorId(PerguntaId).Result;
                if (pergunta == null) return NotFound();

                _perguntaRepositorio.Atualizar(pergunta);
                var result = _mapper.Map<PerguntaDto>(pergunta);
                return Created($"api/pergunta/v1/obterporid/{result.Id}", result);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");
            }
        }

        [Route("v1/obterpergunta/{Id}")]
        [HttpGet]
        public async Task<IActionResult> obterpergunta(int Id){
            try{
                var results = await _perguntaRepositorio.ObterProximaPergunta(Id);
                var result = _mapper.Map<PerguntaDto>(results);
                return Ok(result);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");
            }
        }
    }
}