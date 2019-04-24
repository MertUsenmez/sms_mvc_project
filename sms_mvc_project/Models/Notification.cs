using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sms_mvc_project.Models
{
    public class Notification
    {
        //must be identify notification types(message, match, ....)
        [BsonElement("NotificationType")]
        public String NotificationType { get; set; }

        [BsonElement("NotificationText")]
        public String NotificationText { get; set; }
    }
}