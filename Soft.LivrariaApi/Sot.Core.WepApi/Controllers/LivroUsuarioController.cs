using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soft.Modelos;
using Sot.Core.WepApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sot.Core.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroUsuarioController : ControllerBase
    {
        private readonly ILivroUsuarioService _livroUsuarioService;

        public LivroUsuarioController(ILivroUsuarioService service)
        {
            _livroUsuarioService = service;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/LivroUsuario/GetLivroUsuario")]
        public IEnumerable<LivroUsuario> GetLivroUsuario()
        {
            return _livroUsuarioService.GetAll();
        }
        [HttpPost]
        [Route("[action]")]
        [Route("api/LivroUsuario/AddLivroUsuario")]
        public LivroUsuario AddLivroUsuario(LivroUsuario livroUsuario)
        {
            return _livroUsuarioService.Add(livroUsuario);
        }
        [HttpPut]
        [Route("[action]")]
        [Route("api/LivroUsuario/EditLivroUsuario")]
        public LivroUsuario EditLivroUsuario(LivroUsuario livroUsuario)
        {
            return _livroUsuarioService.Update(livroUsuario);
        }
        [HttpDelete]
        [Route("[action]")]
        [Route("api/LivroUsuario/DeleteLivroUsuario")]
        public LivroUsuario DeleteLivroUsuario(int id)
        {
            return _livroUsuarioService.Delete(id);
        }
        [HttpGet]
        [Route("[action]")]
        [Route("api/LivroUsuario/GetLivroUsuarioId")]
        public LivroUsuario GetLivroUsuarioId(int id)
        {
            return _livroUsuarioService.GetById(id);
        }
    }
}
