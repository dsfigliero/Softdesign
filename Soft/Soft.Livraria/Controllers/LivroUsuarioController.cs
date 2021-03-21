
using Soft.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Soft.Livraria.Controllers
{
    public class LivroUsuarioController : Controller
    {
        //
        // GET: /LivroUsuario/

        public ActionResult Index()
        {
            IEnumerable<LivroUsuario> livros = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());
                var responseTask = client.GetAsync("LivroUsuario/GetLivroUsuario");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<LivroUsuario>>();
                    readTask.Wait();

                    livros = readTask.Result;
                    
                    return View(livros.ToList());
                }
                else
                {
                    livros = Enumerable.Empty<LivroUsuario>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(livros.ToList());
        }

        //
        // GET: /LivroUsuario/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LivroUsuario/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LivroUsuario/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id = 0)
        {
            Livro livro = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());
                
                var livroUsuario = new LivroUsuario();
                livroUsuario.LivroId = id;
                livroUsuario.UsuarioId = Convert.ToInt32(Session["usuarioLogadoID"].ToString());

                var putTask = client.PostAsJsonAsync<LivroUsuario>("LivroUsuario/EditLivroUsuario", livroUsuario);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(livro);
        }

        //
        // GET: /LivroUsuario/Edit/5

        public ActionResult Edit(Livro livro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

                var livroUsuario = new LivroUsuario();
                livroUsuario.LivroId = livro.LivroId;
                livroUsuario.UsuarioId = Convert.ToInt32( Session["usuarioLogadoID"].ToString());
                var putTask = client.PostAsJsonAsync<LivroUsuario>("LivroUsuario/EditLivroUsuario", livroUsuario);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(livro);

            
        }

        //
        // POST: /LivroUsuario/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void AlteraSituacao(int id)
        {
            Livro livro = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());
                var responselivro = client.GetAsync("Livro/GetLivroId/?id=" + id);
                responselivro.Wait();

                var res = responselivro.Result;

                if (res.IsSuccessStatusCode)
                {
                    var readTask = res.Content.ReadAsAsync<Livro>();
                    readTask.Wait();
                    livro = readTask.Result;
                    livro.Situacao = !livro.Situacao;
                    var postLivro = client.PostAsJsonAsync<Livro>("Livro/EditLivro", livro);
                    postLivro.Wait();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
        }

        public ActionResult Delete(int id = 0)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

                var deleteTask = client.DeleteAsync("livrousuario/DeleteLivroUsuario/?id=" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                var readTask = result.Content.ReadAsAsync<LivroUsuario>();
                readTask.Wait();
                var livroUsuario = readTask.Result;
                AlteraSituacao(livroUsuario.LivroId);
                
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }
    }
}
