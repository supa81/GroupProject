using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PawMates.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [Display(Name = "Zip Code")]
        [StringLength(10, MinimumLength = 5)]
        public string ZipCode { get; set; }
        [Display(Name = "Picture URL")]
        public string PictureURL { get; set; }
        [Display(Name = "Filter Age")]
        public int? FilterAge { get; set; }
        [Display(Name = "Filter Weight")]
        public int? FilterWeight { get; set; }
        [Display(Name = "Filter Gender")]
        public string FilterGender { get; set; }
        [Display(Name = "Filter Breed")]
        public string? FilterBreed { get; set; }
        [Display(Name = "Filter Temperment")]
        public string? FilterTemperment { get; set; }
        [Display(Name = "Filter Distance")]
        public int? FilterDistance { get; set; }
        public double? OwnerLatitude { get; set; }
        public double? OwnerLongitude { get; set; }

        //[ForeignKey("SlackUserID")]
        //public string SlackUserId { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
