using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnerAPIController : ApiController
    {
        private NerdDinnerContext db = new NerdDinnerContext();

        public JsonDinner GetDinner(int id)
        {
            return SearchController.JsonDinnerFromDinner(db.Dinners.Find(id));
        }

        public IEnumerable<JsonDinner> GetDinners()
        {
            return db.Dinners.ToList().Select(d => SearchController.JsonDinnerFromDinner(d));
        }

        public int Create(JsonDinner dinner)
        {

        }
    }
}
