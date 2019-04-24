using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sms_mvc_project.Models
{
    public class Match
    {

        public Match(ObjectId StudentFriendId, DateTime CreateOn)
        {
            this.StudentFriendId = StudentFriendId;
            this.CreateOn = CreateOn;
        }

        [BsonElement("StudentFriendId")]
        public ObjectId StudentFriendId { get; set; }

        [BsonElement("CreateOn")]
        public DateTime CreateOn { get; set; }
    }
}