using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    [BsonIgnoreExtraElements]
    public class Address
    {
        [BsonElement("building")]
        public string BuildingNr { get; set; }

        [BsonElement("coord")]
        public double[] Coordinates { get; set; }

        [BsonElement("street")]
        public string Street { get; set; }

        [BsonElement("zipcode")]
        public object ZipCode { get; set; }
    }
}