using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soft.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sot.Core.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService service)
        {
            _usuarioService = service;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Usuario/GetUsuario")]
        public IEnumerable<Usuario> GetUsuario()
        {
            return _usuarioService.GetAll();
        }
        [HttpPost]
        [Route("[action]")]
        [Route("api/Usuario/AddUsuario")]
        public Usuario AddUsuario(Usuario Usuario)
        {
            return _usuarioService.Add(Usuario);
        }
        [HttpPut]
        [Route("[action]")]
        [Route("api/Usuario/EditUsuario")]
        public Usuario EditUsuario(Usuario Usuario)
        {
            return _usuarioService.Update(Usuario);
        }
        [HttpDelete]
        [Route("[action]")]
        [Route("api/Usuario/DeleteUsuario")]
        public Usuario DeleteUsuario(int id)
        {
            return _usuarioService.Delete(id);
        }
        [HttpGet]
        [Route("[action]")]
        [Route("api/Usuario/GetUsuarioId")]
        public Usuario GetUsuarioId(int id)
        {
            return _usuarioService.GetById(id);
        }

    }
}
