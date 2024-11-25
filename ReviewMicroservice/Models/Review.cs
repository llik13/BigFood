using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ReviewMicroservice.Models
{
    public class Review
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string UserId { get; set; }

        [BsonRequired]
        public string Title { get; set; }

        [BsonRequired]
        public string Content { get; set; }

        [BsonRequired]
        public int Rating { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}

