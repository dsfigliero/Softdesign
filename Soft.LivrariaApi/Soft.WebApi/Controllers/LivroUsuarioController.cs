using Soft.Models.Models;
using Soft.WebApi.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soft.WebApi.Controllers
{
    public class LivroUsuarioController : ApiController
    {
        private LivrariaContext db = new LivrariaContext();

        public IEnumerable<LivroUsuario> GetLivrosUsuarios()
        {
            return db.LivrosUsuarios.AsEnumerable();
        }
        
        public LivroUsuario GetLivrouUsuario(int id)
        {
            LivroUsuario livro = db.LivrosUsuarios.Where(l => l.LivroId == id).FirstOrDefault();
            return livro;
        }
        
        public HttpResponseMessage Post(LivroUsuario livroUsuario)
        {
            if (ModelState.IsValid)
            {
                if (db.LivrosUsuarios.Any<LivroUsuario>(l => l.LivroId == livroUsuario.LivroId))
                {
                    db.Entry(livroUsuario).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                    }
                }
                else
                {
                    db.LivrosUsuarios.Add(livroUsuario);
                    db.SaveChanges();
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, livroUsuario);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = livroUsuario.LivroId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        
        public HttpResponseMessage Delete(int id)
        {
            LivroUsuario livro = db.LivrosUsuarios.Where(l => l.LivroUsuarioId == id).FirstOrDefault();
            if (livro == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.LivrosUsuarios.Remove(livro);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, livro);
        }
    }
}
