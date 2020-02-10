using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLayer.Models
{
    public class Worker
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("workerId")]
        [Required]
        public string WorkerId { get; set; }

        [BsonElement("name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("factory")]
        [Required]
        public string Factory { get; set; }

        [BsonElement("age")]
        [Required]
        [Range(15,65,ErrorMessage="Age must be between 15 and 65")]
        public int? Age { get; set; }

        [BsonElement("salary")]
        [Required]
        [Range(30270,400000, ErrorMessage = "Salary must be between 30720 and 400000")]
        public int? Salary { get; set; }
    }
}
