using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sms_mvc_project.Models
{
    public class University
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId _id { get; set; }

        [BsonElement("UniversityName")]
        public String UniversityName { get; set; }

        [BsonElement("UniversityCountry")]
        public String UniversityCountry { get; set; }
    }
}