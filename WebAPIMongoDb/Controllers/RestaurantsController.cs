using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using DAL.Models;
using DAL.UnitOfWork;
using MongoDB.Bson;

namespace WebAPIMongoDb.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [EnableQuery]
    public class RestaurantsController : ApiController
    {
        private readonly UnitOfWork _rUnitOfwork;

        public RestaurantsController()
        {
            _rUnitOfwork = new UnitOfWork();
        }

        // GET: api/Restaurants
        public HttpResponseMessage Get()
        { 
            //var restaurants = _rUnitOfwork.Restaurant.GetAll().AsQueryable();
            var restaurants = _rUnitOfwork.Restaurant.GetAll().Result.AsQueryable();
            if (restaurants != null)
                return Request.CreateResponse(HttpStatusCode.OK, restaurants);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No restaurant found.");
        }

        /*public IQueryable<Restaurant> Get()
        {
            return _rUnitOfwork.Restaurant.GetAll().Result.AsQueryable();
        }*/

        // GET: api/Restaurants/5
        public HttpResponseMessage Get(string id)
        {
            var restaurant = _rUnitOfwork.Restaurant.Get(r => r.Id, new ObjectId(id));

            if (restaurant != null)
                return Request.CreateResponse(HttpStatusCode.OK, restaurant);
            
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Restaurant with {id} not found.");
        }

        // POST: api/Restaurants
        public HttpResponseMessage Post([FromBody]Restaurant value)
        {
            if (value == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Restaurant value is null");
            }

            _rUnitOfwork.Restaurant.Add(value);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT: api/Restaurants/5
        public HttpResponseMessage Put(string id, [FromBody]Restaurant value)
        {
            if (id == null)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "id value is empty");

            var resId = new ObjectId(id);
            var restaurant = _rUnitOfwork.Restaurant.Get(r => r.Id, new ObjectId(id));

            if (restaurant == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Restaurant not found.");
            }
            else
            {
                var model = new Restaurant
                {
                    restaurant_id = value.restaurant_id,
                    Id = resId,
                    Address = value.Address,
                    Borough = value.Borough,
                    Cuisine = value.Cuisine,
                    Grades = value.Grades,
                    Name = value.Name
                };

                _rUnitOfwork.Restaurant.Update(r => r.Id, resId, value);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        // DELETE: api/Restaurants/5
        public HttpResponseMessage Delete(string id)
        {
            if (id == null)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "id value is empty");

            _rUnitOfwork.Restaurant.Delete(r => r.Id, new ObjectId(id));
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}