using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sms_mvc_project.Models
{
    public class Conversation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId), BsonRequired]
        public ObjectId Id { get; set; }

        [BsonElement("Participants"), BsonRequired]
        public Student[] Participants { get; set; }    /* ['student1', 'student2'] */


        //public Message[] Messages { get; set; }
    }
}