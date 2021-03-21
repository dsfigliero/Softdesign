using System;
using System.Collections.Generic;

namespace Soft.Modelos
{
    public partial class Usuario
    {
        public Usuario()
        {
            LivroUsuarios = new HashSet<LivroUsuario>();
        }

        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public virtual ICollection<LivroUsuario> LivroUsuarios { get; set; }
    }
}
