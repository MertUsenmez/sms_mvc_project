using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sms_mvc_project.Models
{
    [BsonIgnoreExtraElements]
    public class Student
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId _id { get; set; }

        [BsonElement("StudentName")]
        public String StudentName { get; set; }

        [BsonElement("StudentSurname")]
        public String StudentSurname { get; set; }

        [BsonElement("StudentEmail")]
        public String StudentEmail { get; set; }

        [BsonElement("StudentPassword")]
        public String StudentPassword { get; set; }

        [BsonElement("DOB")]
        public String DOB { get; set; }

        [BsonElement("ConfirmCode")]
        public String ConfirmCode { get; set; }

        [BsonElement("SendConfirmCodeTime")]
        public String SendConfirmCodeTime { get; set; }

        [BsonElement("CreateOn")]
        public String CreateOn { get; set; }

        [BsonElement("ModifiedOn")]
        public String ModifiedOn { get; set; }

        //must be must be {"pre", "1", "2", "3", "4"} 
        [BsonElement("Class")]
        public String Class { get; set; }

        //must be equal or less than 3
        [BsonElement("CountOfMatch")]
        public int CountOfMatch { get; set; }

        [BsonElement("Department")]
        public String DepartmentName { get; set; }

        //University collectionundan alacak
        [BsonElement("University")]
        public String University { get; set; }

        //En başta registeration safhasında false olarak belirlenecek
        [BsonElement("MatchingStatus")]
        public bool MatchingStatus { get; set; }

        [BsonElement("InterestAreas")]
        public List<InterestAreas> InterestAreas { get; set; }

        //must be limit 3 matches
        [BsonElement("Mathces")]
        public List<Match> Mathces { get; set; }

        [BsonElement("Notifications")]
        public Notification[] Notifications { get; set; }

    }
}
