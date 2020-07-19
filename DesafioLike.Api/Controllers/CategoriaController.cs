using System.Threading.Tasks;
using AutoMapper;
using DesafioLike.Api.Dtos;
using DesafioLike.Dominio.Entidades;
using DesafioLike.Dominio.IRepositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioLike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        public  readonly IMapper _mapper;
        public CategoriaController(ICategoriaRepositorio categoriaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _categoriaRepositorio = categoriaRepositorio;
        }

        ///<summary>
        ///Busca de todas as categorias
        ///</summary>
        [Authorize(Roles = "Admin, Operador")]
        [Route("v1/obtertodos")]
        [HttpGet]
        public async Task<IActionResult> Obtertodos()
        {
            try
            {
                var categorias = await _categoriaRepositorio.ObterTodosCategoria();
                var results = _mapper.Map<CategoriaDto[]>(categorias);
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falou {ex.Message}");

            }
        }

        ///<summary>
        ///Busca de categoria por id da categoria
        ///</summary>
        [Authorize(Roles = "Admin, Operador")]
        [Route("v1/obterPorId/{CategoriaId}")]
        [HttpGet]        
        public async Task<IActionResult> ObterPorId(int CategoriaId)
        {
            try
            {
                var categoria = await _categoriaRepositorio.ObterPorIdCategoria(CategoriaId);
                var result = _mapper.Map<CategoriaDto>(categoria);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falou {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin, Operador")]
        [Route("v1/cadastrar")]
        [HttpPost]
        public IActionResult Cadastrar(CategoriaDto categoriaModel)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaModel);
                _categoriaRepositorio.Adicionar(categoria);
                return Created($"api/categoria/{categoria.Id}", _mapper.Map<CategoriaDto>(categoria));
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falou{ ex.Message }" );
            }
        }

        [Authorize(Roles = "Admin, Operador")]
        [Route("v1/atualizar/{CategoriaId}")]
        [HttpPut]
        public async Task<IActionResult> put(int CategoriaId, CategoriaDto categoriaModel)
        {
            try
            {
                var categoria = await _categoriaRepositorio.ObterPorIdCategoria(CategoriaId);
                if (categoria == null) return NotFound();

                _mapper.Map(categoriaModel, categoria);
                _categoriaRepositorio.Atualizar(categoria);
                return Created($"api/categoria/{categoria.Id}", _mapper.Map<CategoriaDto>(categoria));
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");
            }
        }

        [Authorize(Roles = "Admin, Operador")]
        [Route("v1/excluir/{CategoriaId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int CategoriaId)
        {
            try
            {
                var _categoria = await _categoriaRepositorio.ObterPorId(CategoriaId);
                if (_categoria == null) return NotFound();

                _categoriaRepositorio.Remover(_categoria);
                return Ok();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");
            }
        }
    }
}