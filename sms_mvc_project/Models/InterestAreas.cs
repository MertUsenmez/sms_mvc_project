using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sms_mvc_project.Models
{
    public class InterestAreas
    {
        
        public InterestAreas(string InterestAreaName)
        {
            this.InterestAreaName = InterestAreaName;
        }
        

        [BsonElement("InterestAreaName")]
        public string InterestAreaName { get; set; }

    }
}
