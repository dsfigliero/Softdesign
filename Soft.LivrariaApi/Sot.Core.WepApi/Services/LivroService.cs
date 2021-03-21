using Microsoft.EntityFrameworkCore;
using Soft.Modelos;
using Soft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sot.Core.WepApi
{
    public class LivroService : ILivroService
    {
        private LivrariaContext _dbContext;
        public LivroService(LivrariaContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Livro GetById(int id)
        {
            var livro = _dbContext.Livros.FirstOrDefault(c => c.LivroId == id);
            return livro;
        }

        public Livro Add(Livro entity)
        {
            if (entity != null)
            {
                _dbContext.Livros.Add(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            return null;
        }

        public Livro Delete(int id)
        {
            var livro = _dbContext.Livros.FirstOrDefault(x => x.LivroId == id);
            _dbContext.Entry(livro).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return livro;
        }
        public Livro Update(Livro entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<Livro> GetAll()
        {
            IQueryable<Livro> query = _dbContext.Livros;

            query = query
            .AsNoTracking()
            .OrderByDescending(c => c.Titulo);

            return query;
        }
    }
}
