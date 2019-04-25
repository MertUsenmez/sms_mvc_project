using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sms_mvc_project.Models
{
    public class Message
    {
        // if(Sender and Recipient == Conversation.Participants[0] and Conversation.Participants[1]){ conversationId = Conversation.Id; }
        // ya da şöylede olabilir
        // Student Conversation.Id ile Conversation'a bağlanır zaten sender ve recipient bellidir
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId), BsonRequired]
        public ObjectId conversationId { get; set; }

        [BsonElement("Sender")]
        public Student Sender { get; set; }

        [BsonElement("Recipient")]
        public Student Recipient { get; set; }

        [BsonElement("MessageBody")]
        public string MessageBody { get; set; }

        // Messages sorting by CreateOn
        [BsonElement("CreateOn")]
        public DateTime CreateOn { get; set; } = DateTime.Now;


    }
}
