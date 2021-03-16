
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Soft.Models.Models;
using System.Net.Http;
using System.Web.Security;

namespace Soft.Livraria.Controllers
{
    public class LoginController : Controller
    {
        private const string BASE_URL = "http://localhost:57678/api/";

        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autenticar(Usuario u)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URL);

                var postTask = client.PostAsJsonAsync<Usuario>("Login/Autenticar", u);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Usuario>();
                    readTask.Wait();

                    if (readTask.Result != null)
                    {
                        FormsAuthentication.SetAuthCookie(u.Login, false);
                        Session["usuarioLogadoID"] =  readTask.Result.UsuarioId.ToString();
                        Session["nomeUsuarioLogado"] = readTask.Result.NomeUsuario.ToString();
                        return RedirectToAction("Index", "Livro");
                        
                    }
                }
            }


            return RedirectToAction("Index", "Login");
        }

    }
}