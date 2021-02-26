using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace favoriteMovies.Models
{

    public class Movie
    {
        [Key]

        public int MovieId {get; set;}



        [Required(ErrorMessage="Please enter a title")]
        [MinLength(3,ErrorMessage="please make a title 3 characters or more.")]
        public string Title {get;set;}

        [Required(ErrorMessage="Please enter a star")]
        [MinLength(3,ErrorMessage="please make a title 3 characters or more.")]
        public string Starring {get;set;}
        
        [Required(ErrorMessage="Please enter a image url")]
        public string ImageUrl {get;set;}

        // [DataType(DataType.Date)]
        public DateTime ReleaseDate {get;set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;



        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public int UserId{get;set;}
        public User PostedBy {get;set;}

        public List<Like> Likes {get;set;}

        


    }

}