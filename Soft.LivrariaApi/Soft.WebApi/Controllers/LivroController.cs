using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Soft.Modelos;
using Soft.WebApi.DAL;

namespace Soft.WebApi.Controllers
{
    public class LivroController : ApiController
    {
        private LivrariaContext db = new LivrariaContext();

        public IEnumerable<Livro> GetLivros()
        {
            return db.Livros.AsEnumerable();
        }

        public Livro GetLivro(int id)
        {
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return livro;
        }
        
        public HttpResponseMessage PutLivro(int id, Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != livro.LivroId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(livro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        public HttpResponseMessage PostLivro(Livro livro)
        {
            if (ModelState.IsValid)
            {
                if (db.Livros.Any<Livro>(l => l.LivroId == livro.LivroId))
                {
                    db.Entry(livro).State = EntityState.Modified;

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
                    db.Livros.Add(livro);
                    db.SaveChanges();
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, livro);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = livro.LivroId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        
        public HttpResponseMessage DeleteLivro(int id)
        {
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            db.Livros.Remove(livro);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}