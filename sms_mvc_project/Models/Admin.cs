using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sms_mvc_project.Models
{
    public class Admin
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId _id { get; set; }

        [BsonElement("AdminName")]
        public string AdminName { get; set; }

        [BsonElement("AdminSurname")]
        public string AdminSurname { get; set; }

        [BsonElement("AdminEmail")]
        public string AdminEmail { get; set; }

        [BsonElement("AdminPassword")]
        public string AdminPassword { get; set; }
    }
}