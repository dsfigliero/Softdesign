using Soft.Models.Models;
using Soft.WebApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Soft.WebApi.Controllers
{
    public class LoginController : ApiController
    {
        private LivrariaContext db = new LivrariaContext();

        [ValidateAntiForgeryToken]
        public Usuario Autenticar(Usuario u)
        {
            if (ModelState.IsValid) 
            {
                var v = db.Usuarios.Where(a => a.Login.Equals(u.Login) && a.Senha.Equals(u.Senha)).FirstOrDefault();
                if (v != null)
                {
                    return v;
                }
            }
            return null;
        }
    }
}
