using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Soft.Models.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        public string NomeUsuario { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }
    }
}