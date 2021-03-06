using System;
using System.Collections.Generic;

namespace Soft.Modelos
{
    public partial class Livro
    {
        public Livro()
        {
            LivroUsuarios = new HashSet<LivroUsuario>();
        }

        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public bool Situacao { get; set; }

        public virtual ICollection<LivroUsuario> LivroUsuarios { get; set; }
    }
}
