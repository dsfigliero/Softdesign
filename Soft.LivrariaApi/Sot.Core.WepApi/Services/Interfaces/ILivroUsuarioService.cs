
using Soft.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sot.Core.WepApi.Services.Interfaces
{
    public interface ILivroUsuarioService
    {
        LivroUsuario Add(LivroUsuario entity);
        LivroUsuario Update(LivroUsuario entity);
        LivroUsuario Delete(int id);
        IEnumerable<LivroUsuario> GetAll();
        LivroUsuario GetById(int id);
    }
}
