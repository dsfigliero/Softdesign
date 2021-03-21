using Soft.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Sot.Core.WepApi
{
    public interface ILivroService
    {
        Livro Add(Livro entity);
        Livro Update(Livro entity);
        Livro Delete(int id);
        IEnumerable<Livro> GetAll();
        Livro GetById(int id);
    }
}
