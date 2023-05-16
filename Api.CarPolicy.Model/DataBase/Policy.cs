using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.CarPolicy.Model.DataBase
{
    public class Policy
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [Required]
        public string? PolicyNumber { get; set; }

        [Required]
        public DateTime? DateInit { get; set; }

        [Required]
        public IEnumerable<string>? Coverages { get; set; }

        [Required]
        public int MaxValueCoverage { get; set; }

        [Required]
        public string? CoveragePlanName { get; set; }


        [Required]
        public Validity? Validity { get; set; }

        [Required]
        public Client? Client { get; set; }

        [Required]
        public Car? Car { get; set; }

    }

    public class Client
    {

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? TypeId { get; set; }

        [Required]
        public int? NumberId { get; set; }

        [Required]
        public DateTime? Birthdate { get; set; }

        [Required]
        public string? CityHome { get; set; }

        [Required]
        public string? AddressHome { get; set; }

    }

    public class Car
    {
        [Required]
        public string? Plate { get; set; }

        [Required]
        public short Model { get; set; }

        [Required]
        public bool HasInspection { get; set; }
    }

    public class Validity
    {
        public Validity()
        {
            this.Init = DateTime.UtcNow.AddDays(-5);
            this.End = Init.AddYears(1);
        }
        public DateTime Init { get; set; }
        public DateTime End { get; set; }

        

    }
}
