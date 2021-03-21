using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soft.Modelos;

namespace Sot.Core.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService service)
        {
            _livroService = service;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Livro/GetLivro")]
        public IEnumerable<Livro> GetLivro()
        {
            return _livroService.GetAll();
        }
        [HttpPost]
        [Route("[action]")]
        [Route("api/Livro/AddLivro")]
        public Livro AddLivro(Livro livro)
        {
            return _livroService.Add(livro);
        }
        [HttpPost]
        [Route("[action]")]
        [Route("api/Livro/EditLivro")]
        public Livro EditLivro(Livro Livro)
        {
            return _livroService.Update(Livro);
        }
        [HttpDelete]
        [Route("[action]")]
        [Route("api/Livro/DeleteLivro")]
        public Livro DeleteLivro(int id)
        {
            return _livroService.Delete(id);
        }
        [HttpGet]
        [Route("[action]")]
        [Route("api/Livro/GetLivroId")]
        public Livro GetLivroId(int id)
        {
            return _livroService.GetById(id);
        }

    }
}
