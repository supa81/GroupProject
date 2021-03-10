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
        public string Username { get; set; }
        public string ZipCode { get; set; }
        public string PictureURL { get; set; }
        public int? FilterAge { get; set; }
        public int? FilterWeight { get; set; }
        public string FilterGender { get; set; }
        public string? FilterBreed { get; set; }
        public string? FilterTemperment { get; set; }

        [ForeignKey("SlackUserID")]
        public string SlackUserId { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
