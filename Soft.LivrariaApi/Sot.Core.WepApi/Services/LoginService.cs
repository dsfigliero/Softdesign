using Soft.Modelos;
using Soft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sot.Core.WepApi.Services.Interfaces
{
    public class LoginService : ILoginService
    {
        private LivrariaContext _dbContext;
        public LoginService(LivrariaContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Usuario Autenticar(Usuario usuario)
        {
        var v = _dbContext.Usuarios.Where(a => a.Login.Equals(usuario.Login) && a.Senha.Equals(usuario.Senha)).FirstOrDefault();
            if (v != null)
            {
                return v;
            }

            return null;
        }
    }
}
