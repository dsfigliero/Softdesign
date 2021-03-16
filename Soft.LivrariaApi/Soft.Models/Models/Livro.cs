using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Soft.Models.Models
{
    public class Livro
    {
        public int LivroId { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public bool Situacao { get; set; }
    }
}