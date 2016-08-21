using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;


namespace DAL.Models
{
    [BsonIgnoreExtraElements]
    public class Restaurant
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("address")]
        public Address Address { get; set; }

        [BsonElement("borough")]
        public string Borough { get; set; }

        [BsonElement("cuisine")]
        public string Cuisine { get; set; }

        [BsonElement("grades")]
        public IEnumerable<Grades> Grades { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("restaurant_id")]
        [BsonRepresentation(BsonType.String)]
        public int restaurant_id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}