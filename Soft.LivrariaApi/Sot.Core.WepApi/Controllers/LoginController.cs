using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soft.Modelos;
using Sot.Core.WepApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soft.Core.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService service)
        {
            _loginService = service;
        }

        [HttpPost]
        [Route("[action]")]
        [Route("api/Login/Autenticar")]
        public Usuario Autenticar(Usuario usuario)
        {
            return _loginService.Autenticar(usuario);
        }
    }
}
