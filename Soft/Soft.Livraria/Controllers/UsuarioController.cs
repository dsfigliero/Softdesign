using Soft.Models.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Soft.Livraria.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        [Authorize]
        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<Usuario> usuarios = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());
                var responseTask = client.GetAsync("Usuario");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    
                    var readTask = result.Content.ReadAsAsync<IList<Usuario>>();
                    readTask.Wait();

                    usuarios = readTask.Result;
                   
                   

                    return View(usuarios.ToList());
                }
                else
                {
                    usuarios = Enumerable.Empty<Usuario>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(usuarios.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

                var postTask = client.PostAsJsonAsync<Usuario>("Usuario", usuario);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(usuario);

        }
        //
        // GET: /Livro/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Usuario usuario = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

                var responseTask = client.GetAsync("Usuario?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Usuario>();
                    readTask.Wait();

                    usuario = readTask.Result;
                }
            }
            return View(usuario);
        }

        //
        // POST: /Livro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());
                var putTask = client.PostAsJsonAsync<Usuario>("Usuario", usuario);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(usuario);
        }

        //
        // GET: /Livro/Delete/5

        public ActionResult Delete(int id = 0)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

                var deleteTask = client.DeleteAsync("usuario/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

    }
}
