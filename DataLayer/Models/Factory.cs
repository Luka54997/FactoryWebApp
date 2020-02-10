using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace DataLayer.Models
{
    public class Factory
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("city")]
        [Required]
        public string City { get; set; }
        

        [BsonElement("CEO")]
        [Required]
        public string CEO { get; set; }

        [BsonElement("areaOfExpertise")]
        [Required]
        public string AreaOfExpertise { get; set; }
    }
}
