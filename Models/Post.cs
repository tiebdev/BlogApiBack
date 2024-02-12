using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace BlogApi.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("content")]
        public string? Content { get; set; }

        [BsonElement("authorFullName")]
        public string? AuthorFullName { get; set; }

        [BsonElement("publishDate")]
        public DateTime PublishDate { get; set; }

        [BsonElement("categories")]
        public List<string> Categories { get; set; } = new List<string>();

        [BsonElement("tags")]
        public List<string> Tags { get; set; } = new List<string>();

        [BsonElement("comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        [BsonElement("imageurls")]
        public List<string> ImageUrls { get; set; } = new List<string>();

        [BsonElement("videourls")]
        public List<string> VideoUrls { get; set; } = new List<string>();

        [BsonElement("fileurls")]
        public List<string> FileUrls { get; set; } = new List<string>();
    }
}

