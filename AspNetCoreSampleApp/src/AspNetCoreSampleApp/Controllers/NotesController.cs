using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreSampleApp.Model;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetCoreSampleApp.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        [HttpGet]
        public List<Note> Get()
        {
            List<Note> result = new List<Note>();

            using (AppDbContext context = new AppDbContext())
            {
                var notes = context.Notes.ToList();
                if (notes != null && notes.Any())
                    result = notes;
            }

            return result;
        }

        [HttpGet("{id}")]
        public Note Get(Guid id)
        {
            if (id == null || Guid.Empty == id)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Id is Null or Empty" };
                throw new HttpResponseException(msg);
            }

            Note result = new Note();

            using (AppDbContext context = new AppDbContext())
            {
                var notes = context.Notes.Where(a => a.Id == id).FirstOrDefault();
                if (notes != null)
                    result = notes;
            }

            return result;
        }

        [HttpPost]
        public JsonResult Post([FromBody]string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Value is Null or Empty" };
                throw new HttpResponseException(msg);
            }

            Note rec = new Note();
            rec.Id = new Guid();
            rec.Content = value;

            using (AppDbContext context = new AppDbContext())
            {
                context.Notes.Add(rec);
                context.Commit();
            }

            return new JsonResult(new { Id = rec.Id });
        }

        [HttpPut("{id}")]
        public JsonResult Put(Guid id, [FromBody]string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Value is Null or Empty" };
                throw new HttpResponseException(msg);
            }

            Note rec = new Note();

            using (AppDbContext context = new AppDbContext())
            {
                rec = context.Notes.Where(a => a.Id == id).FirstOrDefault();
                if (rec == null)
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = "" };
                    throw new HttpResponseException(msg);
                }

                rec.Content = value;
                context.Attach<Note>(rec);
                context.Commit();
            }

            return new JsonResult(new { Id = rec.Id });
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            if (id == null || Guid.Empty == id)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Id is Null or Empty" };
                throw new HttpResponseException(msg);
            }

            using (AppDbContext context = new AppDbContext())
            {
                var rec = context.Notes.Where(a => a.Id == id).FirstOrDefault();
                if (rec == null)
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = "" };
                    throw new HttpResponseException(msg);
                }

                context.Remove<Note>(rec);
                context.Commit();
            }
        }
    }
}
