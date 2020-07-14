using System.Threading.Tasks;
using AutoMapper;
using DesafioLike.Api.Dtos;
using DesafioLike.Dominio.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DesafioLike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        public readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        public AdministrationController(IMapper mapper, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [Route("v1/obtertodos")]
        [HttpGet]
        public IActionResult Obtertodos()
        {
           try
           {
               var result = _roleManager.Roles;
               var rolesToReturn = _mapper.Map<RoleDto[]>(result);
               return Ok(rolesToReturn);
           }
           catch (System.Exception)
           {
               
               throw;
           }
        }
        
        [Route("v1/cadastrar")]
        [HttpPost]
        public async Task<IActionResult> cadastrar(RoleDto roleDto){
            try
            {
                var _role =_mapper.Map<Role>(roleDto);
                var result = await _roleManager.CreateAsync(_role);
                var roleToReturn = _mapper.Map<RoleDto>(_role);

                 if(result.Succeeded){
                     return Created("GetRole", roleToReturn);
                 }else{
                     return BadRequest(result.Errors);
                 }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou - {ex.Message}");
            }
        }

        [Route("v1/atualizar/{RoleId}")]
        [HttpPut]
        public async Task<IActionResult> put(int RoleId, RoleDto roleModel)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                if (role == null) return NotFound();
                
                _mapper.Map(roleModel, role);
                var result = await _roleManager.UpdateAsync(role);

                if(result.Succeeded){
                     return Created("GetRole", _mapper.Map<RoleDto>(role));
                 }else{
                     return BadRequest(result.Errors);
                 }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falou");
            }
        }
    }
}