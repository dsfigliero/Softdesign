namespace Soft.WebApi.Migrations
{
    using Soft.Models.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Soft.WebApi.DAL.LivrariaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Soft.WebApi.DAL.LivrariaContext context)
        {
            var livros = new List<Livro>
            {
                new Livro{ Titulo = "Livro A", Autor = "Tal1", Situacao = true},
                new Livro{ Titulo = "Livro B", Autor = "Tal2", Situacao = true},
                new Livro{ Titulo = "Livro C", Autor = "Tal3", Situacao = true},
                new Livro{ Titulo = "Livro D", Autor = "Tal4", Situacao = true},
                new Livro{ Titulo = "Livro E", Autor = "Tal5", Situacao = true}
            };

            livros.ForEach(livro => context.Livros.AddOrUpdate(l => l.Titulo, livro));
            context.SaveChanges();

            var usuarios = new List<Usuario>
            {
                new Usuario{ NomeUsuario = "Usuario A", Senha = "123456", Login = "A"},
                new Usuario{ NomeUsuario = "Usuario B", Senha = "123456", Login = "B"}
            };

            usuarios.ForEach(usuario => context.Usuarios.AddOrUpdate(u => u.Login, usuario));
            context.SaveChanges();
        }
    }
}
