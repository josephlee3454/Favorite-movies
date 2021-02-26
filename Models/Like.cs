using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace favoriteMovies.Models
{

     public class Like
    {
        [Key]
        public int LikeId {get; set;}


        public int UserId{get;set;}// connects us to one user 

        public User UserWhoLikes {get;set;} //reference the actual object

        public int EventId{get;set;}// conects us to one movie 

         public Event LikedEvent {get;set;} //reference the actual object

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        


    }

}