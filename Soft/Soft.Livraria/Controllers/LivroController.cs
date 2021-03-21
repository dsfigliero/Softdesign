using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Configuration;
using Soft.Modelos;


namespace Soft.Livraria.Controllers
{
    public class LivroController : Controller
    {

        [NoDirectAccess]
        public ActionResult Index(string sortOrder, string searchString)
        {
            IEnumerable<Livro> livros = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());
                var responseTask = client.GetAsync("Livro/GetLivro");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Titulo" : "";
                    var readTask = result.Content.ReadAsAsync<IList<Livro>>();
                    readTask.Wait();

                    livros = readTask.Result;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        livros = livros.Where(l => l.Titulo.ToUpper().Contains(searchString.ToUpper())
                                               || l.Autor.ToUpper().Contains(searchString.ToUpper()));
                    }
                    switch (sortOrder)
                    {
                        case "Titulo":
                            livros = livros.OrderBy(l => l.Titulo);
                            break;
                        case "Autor":
                            livros = livros.OrderBy(l => l.Autor);
                            break;
                    }

                    return View(livros.ToList());
                }
                else
                {
                    livros = Enumerable.Empty<Livro>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(livros.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Livro livro = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());
                var responseTask = client.GetAsync("Livro/GetLivro/?id="+id);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Livro>();
                    readTask.Wait();
                    livro = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }
            return View(livro);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Livro livro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

                var postTask = client.PostAsJsonAsync<Livro>("Livro/AddLivro", livro);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(livro);

        }

        public ActionResult Edit(int id = 0)
        {
            Livro livro = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

                var responseTask = client.GetAsync("Livro/GetLivroId/?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Livro>();
                    readTask.Wait();

                    livro = readTask.Result;
                }
            }
            return View(livro);
        }

        //
        // POST: /Livro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Livro livro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());
                var putTask = client.PostAsJsonAsync<Livro>("Livro/EditLivro", livro);
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
        // GET: /Livro/Delete/5

        public ActionResult Delete(int id = 0)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

                var deleteTask = client.DeleteAsync("Livro/DeleteLivro/?id=" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

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

        public ActionResult Alugar(int id = 0)
        {
            Livro livro = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

                var responseTask = client.GetAsync("LivroUsuario/GetLivroUsuarioId/?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Livro>();
                    readTask.Wait();
                    livro = readTask.Result;
                }

                if (livro == null)
                {
                    var livroUsuario = new LivroUsuario();
                    livroUsuario.LivroId = id;
                    livroUsuario.UsuarioId = Convert.ToInt32(Session["usuarioLogadoID"].ToString());
                    AlteraSituacao(id);
                    var postTask = client.PostAsJsonAsync<LivroUsuario>("LivroUsuario/AddLivroUsuario", livroUsuario);
                    postTask.Wait();
                    return RedirectToAction("Index", "LivroUsuario");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(livro);
        }

        //
        // GET: /LivroUsuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alugar(Livro livro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

                var livroUsuario = new LivroUsuario();
                livroUsuario.LivroId = livro.LivroId;
                livroUsuario.UsuarioId = Convert.ToInt32(Session["usuarioLogadoID"].ToString());
                var putTask = client.PostAsJsonAsync<LivroUsuario>("LivroUsuario/AddLivroUsuario", livroUsuario);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(livro);


        }


    }
}