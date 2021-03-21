using System;
using System.Collections.Generic;

#nullable disable

namespace Soft.Models
{
    public partial class LivroUsuario
    {
        public int LivroUsuarioId { get; set; }
        public int UsuarioId { get; set; }
        public int LivroId { get; set; }

        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
