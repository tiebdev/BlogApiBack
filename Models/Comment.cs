using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BlogApi.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("authorId")]
        public string? AuthorId { get; set; }

        [BsonElement("content")]
        public string? Content { get; set; }

        [BsonElement("publishDate")]
        public DateTime PublishDate { get; set; }

        [BsonElement("postId")]
        public string? PostId { get; set; }
    }
}
