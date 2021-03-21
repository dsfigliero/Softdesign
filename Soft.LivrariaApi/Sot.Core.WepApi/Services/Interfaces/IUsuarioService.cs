using Soft.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sot.Core.WepApi
{
    public interface IUsuarioService
    {
        Usuario Add(Usuario entity);
        Usuario Update(Usuario entity);
        Usuario Delete(int id);
        IEnumerable<Usuario> GetAll();
        Usuario GetById(int id);
    }

}
