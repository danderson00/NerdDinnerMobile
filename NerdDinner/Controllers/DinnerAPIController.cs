using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnerAPIController : TableController<MobileDinner>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            NerdDinnerContext context = new NerdDinnerContext();
            DomainManager = new DinnerDomainManager(context, Request);
        }

        public SingleResult<MobileDinner> GetDinner(string id)
        {
            return Lookup(id);
        }

        public IQueryable<MobileDinner> GetDinners()
        {
            return Query();
        }

        public Task DeleteDinner(string id)
        {
            return DeleteAsync(id);
        }

        public async Task<IHttpActionResult> PostDinner(MobileDinner dinner)
        {
            MobileDinner d = await InsertAsync(dinner);
            return CreatedAtRoute("DefaultApi", new { id = d.Id }, d);
        }
    }

    public class DinnerDomainManager : MappedEntityDomainManager<MobileDinner, Dinner>
    {
        private NerdDinnerContext dbcontext;

        public DinnerDomainManager(NerdDinnerContext context, HttpRequestMessage request)
            : base(context, request)
        {
            dbcontext = context;
        }

        public override Task<bool> DeleteAsync(string id)
        {
            return this.DeleteItemAsync(int.Parse(id));
        }

        public override SingleResult<MobileDinner> Lookup(string id)
        {
            return this.LookupEntity(d => string.Compare(d.DinnerID.ToString(), id) == 0);
        }

        public override Task<MobileDinner> UpdateAsync(string id, Delta<MobileDinner> patch)
        {
            return this.UpdateEntityAsync(patch, int.Parse(id));
        }
    }
}
