using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    [BsonIgnoreExtraElements]
    public class Grades
    {
        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("grade")]
        public string Grade { get; set; }

        [BsonElement("score")]
        public object Score { get; set; } // because sometimes score can be declared as null. we must not declare specific type
    }
}