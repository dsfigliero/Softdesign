
using System;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Security;
using System.Configuration;
using Soft.Modelos;


namespace Soft.Livraria.Controllers
{
    public class LoginController : Controller
    {
        
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
                                
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("UrlApi").ToString());

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