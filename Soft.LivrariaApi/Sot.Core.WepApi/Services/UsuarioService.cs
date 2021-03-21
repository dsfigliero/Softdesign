using Microsoft.EntityFrameworkCore;
using Soft.Modelos;
using Soft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sot.Core.WepApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private LivrariaContext _dbContext;
        public UsuarioService(LivrariaContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Usuario GetById(int id)
        {
            IQueryable<Usuario> query = _dbContext.Usuarios;
            query = query
            .AsNoTracking()
            .OrderByDescending(c => c.NomeUsuario)
            .Where(c => c.UsuarioId == id);

            return query.FirstOrDefaultAsync().Result;
        }

        public Usuario Add(Usuario entity)
        {
            if (entity != null)
            {
                _dbContext.Usuarios.Add(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            return null;
        }

        public Usuario Delete(int id)
        {
            var usuario = _dbContext.Usuarios.FirstOrDefault(x => x.UsuarioId == id);
            _dbContext.Entry(usuario).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return usuario;
        }
        public Usuario Update(Usuario entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<Usuario> GetAll()
        {
            IQueryable<Usuario> query = _dbContext.Usuarios;

            query = query
            .AsNoTracking()
            .OrderByDescending(c => c.NomeUsuario);

            return query;
        }
    }
}

