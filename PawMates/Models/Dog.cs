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

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }

    }
}
