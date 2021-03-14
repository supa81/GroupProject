using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PawMates.Models
{
    public class Dog
    {
        [Key]
        public int DogId { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int? Weight { get; set; }
        public string Gender { get; set; }
        public string? Breed { get; set; }
        public string? Temperment { get; set; }
        public string Bio { get; set; }
        public string PictureUrl { get; set; }
        public string ZipCode { get; set; }
        public double? OwnerLat { get; set; }
        public double? OwnerLng { get; set; }
        [Display(Name = "Potential Match #1")]
        public int? PotentialMatches { get; set; }
        [Display(Name = "Potential Match #2")]
        public int? PotentialMatches2 { get; set; }
        [Display(Name = "Potential Match #3")]
        public int? PotentialMatches3 { get; set; }
        [Display(Name = "Potential Match #4")]
        public int? PotentialMatches4 { get; set; }
        [Display(Name = "Potential Match #5")]
        public int? PotentialMatches5 { get; set; }

        [ForeignKey("OwnerId")]
        public int Id { get; set; }
        public Owner Owner { get; set; }
    }
}
