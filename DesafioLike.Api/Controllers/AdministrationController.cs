using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DesafioLike.Api.Dtos;
using DesafioLike.Dominio.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DesafioLike.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        public readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public AdministrationController(IMapper mapper, RoleManager<Role> roleManager
        , UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Route("v1/obtertodosregra")]
        [HttpGet]
        public IActionResult obtertodosregra()
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

        ///Busca de regra por id 
        [Route("v1/obterRegraPorId/{RoleId}")]
        [HttpGet]        
        public async Task<IActionResult> ObterRegraPorId(int RoleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                var result = _mapper.Map<RoleDto>(role);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falou {ex.Message}");

            }
        }

        ///Busca de regra por id 
        [Route("v1/obterUsuarioRegraPorId/{RoleId}")]
        [HttpGet]        
        public async Task<IActionResult> ObterUsuarioRegraPorId(int RoleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                var users = await _userManager.GetUsersInRoleAsync(role.Name);
                //var users = _userManager.Users;
                if (users.ToList().Count() == 0){
                    List<User> usuarios =null;
                    return Ok(usuarios);
                } 
                
                var result = _mapper.Map<UserDto[]>(users);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falou {ex.Message}");

            }
        }
        ///Busca de regra por id 
        [Route("v1/obterUsuarios/{RoleId}")]
        [HttpGet]        
        public async Task<IActionResult> ObterUsuarios(int RoleId)
        {
            try
            {
                List<User> usuarios = new List<User>();
                var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                var usuarioPermissoes = await _userManager.GetUsersInRoleAsync(role.Name);
                if (usuarioPermissoes.Count() == 0){
                     usuarios = _userManager.Users.ToList();
                }else{
                    var idsUsuario = usuarioPermissoes.Select(x=>x.Id).ToArray();
                    usuarios = _userManager.Users.Where(x => !idsUsuario.Contains(x.Id)).ToList();    
                }

                var result = _mapper.Map<UserDto[]>(usuarios);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falou {ex.Message}");

            }
        }
        
        [Route("v1/cadastrarregra")]
        [HttpPost]
        public async Task<IActionResult> cadastrarregra(RoleDto roleDto){
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

        [Route("v1/atualizarregra/{RoleId}")]
        [HttpPut]
        public async Task<IActionResult> atualizarregra(int RoleId, RoleDto roleModel)
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
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou - {ex.Message}");
            }
        }

        [Route("v1/excluirregra/{RoleId}")]
        [HttpDelete]
        public async Task<IActionResult> excluirregra(int RoleId)
        {
            try
            {
                var regra = await _roleManager.FindByIdAsync(RoleId.ToString());
                if (regra == null) return NotFound();

                 var result = await _roleManager.DeleteAsync(regra);

                if(result.Succeeded){
                     return Ok();
                 }else{
                     return BadRequest(result.Errors);
                 }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou - {ex.Message}");
            }
        }

        [Route("v1/excluirpermissao/{RoleId}/{UserId}")]
        [HttpDelete]
        public async Task<IActionResult> excluirPermissao(int RoleId, int UserId)
        {
            try
            {
                var regra = await _roleManager.FindByIdAsync(RoleId.ToString());
                var usuario = await _userManager.FindByIdAsync(UserId.ToString());
                var result = await _userManager.RemoveFromRoleAsync(usuario,regra.Name);
                
                if(result.Succeeded){
                     return Ok();
                 }else{
                     return BadRequest(result.Errors);
                 }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou - {ex.Message}");
            }
        }
        
        [Route("v1/adicionarPermissao/{name}")]
        [HttpPut]
        public async Task<IActionResult> adicionarPermissao(string name, UserDto userModel)
        {
            try
            {  
                 var Mapuser = _mapper.Map<User>(userModel);
                 var user = await _userManager.FindByIdAsync(Mapuser.Id.ToString());
                 var result = await _userManager.AddToRoleAsync(user,name);
                 if(result.Succeeded){
                  return Ok();
                 }else{ 
                     return BadRequest(result.Errors);
                 }
                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou - {ex.Message}");
            }
        }
    }
}