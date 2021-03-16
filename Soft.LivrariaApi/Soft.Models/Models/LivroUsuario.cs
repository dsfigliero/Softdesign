using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soft.Models.Models
{
    public class LivroUsuario
    {
        public int LivroUsuarioId{ get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
    }
}