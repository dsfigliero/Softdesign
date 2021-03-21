using Soft.Modelos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sot.Core.WepApi.Services.Interfaces
{
    public interface ILoginService
    {
        Usuario Autenticar(Usuario usuario);
    }
}
