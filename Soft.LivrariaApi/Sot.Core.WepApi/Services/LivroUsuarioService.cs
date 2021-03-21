using Microsoft.EntityFrameworkCore;
using Soft.Modelos;
using Soft.Models;
using Sot.Core.WepApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sot.Core.WepApi.Services
{
    public class LivroUsuarioService : ILivroUsuarioService
    {
        private LivrariaContext _dbContext;
        public LivroUsuarioService(LivrariaContext dbContext)
        {
            _dbContext = dbContext;
        }
        public LivroUsuario GetById(int id)
        {
            IQueryable<LivroUsuario> query = _dbContext.LivroUsuarios;
            query = query
            .AsNoTracking()
            .Where(c => c.LivroId == id);

            return query.FirstOrDefaultAsync().Result;
        }

        public LivroUsuario Add(LivroUsuario entity)
        {
            if (entity != null)
            {
                _dbContext.LivroUsuarios.Add(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            return null;
        }

        public LivroUsuario Delete(int id)
        {
            var livroUsuario = _dbContext.LivroUsuarios.FirstOrDefault(x => x.LivroUsuarioId == id);
            _dbContext.Entry(livroUsuario).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return livroUsuario;
        }
        public LivroUsuario Update(LivroUsuario entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<LivroUsuario> GetAll()
        {
            IQueryable<LivroUsuario> query = _dbContext.LivroUsuarios
                .Include(c => c.Livro)
                .Include(c => c.Usuario);

            query = query
            .AsNoTracking();

            return query;
        }
    }
}

