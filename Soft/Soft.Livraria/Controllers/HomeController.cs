using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Soft.Livraria.ViewModels;
using Soft.Models.Models;
using System.Net.Http;

namespace Soft.Livraria.Controllers
{
    public class HomeController : Controller
    {
        private const string BASE_URL = "http://localhost:57678/api/";

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Informacoes()
        {

            IEnumerable<Livro> livros = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URL);
                var responseTask = client.GetAsync("Livro");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<IList<Livro>>();
                    readTask.Wait();

                    livros = readTask.Result;

                    var data = from livro in livros
                               group livro by livro.Autor into GrupoAutores
                               select new AutorLivros()
                               {
                                   Autor = GrupoAutores.Key,
                                   LivrosCount = GrupoAutores.Count()
                               };

                    return View(data);
                }
                else
                {
                    livros = Enumerable.Empty<Livro>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(new AutorLivros());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
