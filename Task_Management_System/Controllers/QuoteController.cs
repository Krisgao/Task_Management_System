using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task_Management_System.Models;

namespace Task_Management_System.Controllers
{
    public class QuoteController : ApiController
    {
        [Route("api/Quote/GetAll")]
        public IEnumerable<Quote> Get()
        {
            using (QuoteWebAPIEntities entities = new QuoteWebAPIEntities())
            {
                return entities.Quotes.ToList();
            }

        }
        [Route("api/Quote/TaskById")]
        public HttpResponseMessage GetTaskById(int id)
        {
            using (QuoteWebAPIEntities entities = new QuoteWebAPIEntities())
            {
                var info = entities.Quotes.FirstOrDefault(e => e.QuoteID == id);
                if (info != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, info);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "QuoteID = " + id.ToString() + " is not found");
                }
            }
        }
        [HttpPost]
        [Route("api/Quote/Add")]
        public HttpResponseMessage Create([FromBody] Quote Quote)
        {
            try {
                using (QuoteWebAPIEntities entities = new QuoteWebAPIEntities())
                {
                    entities.Quotes.Add(Quote);
                    entities.SaveChanges();
                    var info = Request.CreateResponse(HttpStatusCode.Created, Quote);
                    info.Headers.Location = new Uri(Request.RequestUri + "/" + Quote.QuoteID.ToString());
                    return info;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }
        [HttpPut]
        [Route("api/Quote/Update")]
        public HttpResponseMessage Update(int id, [FromBody] Quote Quote)
        {
            try
            {
                using (QuoteWebAPIEntities entities = new QuoteWebAPIEntities())
                {
                    var DBData = entities.Quotes.FirstOrDefault(e => e.QuoteID == id);


                    //if you want to change the Quote's value
                    if (DBData != null)
                    {
                        DBData.QuoteType = Quote.QuoteType;
                        DBData.Contact = Quote.Contact;
                        DBData.Task = Quote.Task;
                        DBData.DueDate = Quote.DueDate;
                        DBData.TaskType = Quote.TaskType;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, DBData);
                    }
                    //if the Quote values even does not exsit in Quote class
                    //you need to create it
                    else
                    {
                        entities.Quotes.Add(Quote);
                        entities.SaveChanges();
                        var info1 = Request.CreateResponse(HttpStatusCode.Created, Quote);
                        info1.Headers.Location = new Uri(Request.RequestUri + "/" + Quote.QuoteID.ToString());
                        return info1;
                    }

                }

                
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpDelete]
        [Route("api/Quote/Delete")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (QuoteWebAPIEntities entities = new QuoteWebAPIEntities())
                {
                    var context = entities.Quotes.Find(id);
                    if (context == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Quote with Id = " + id.ToString() + " is not found");
                    }
                    else
                    {
                        entities.Quotes.Remove(context);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
