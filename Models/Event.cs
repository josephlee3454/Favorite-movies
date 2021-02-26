using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace favoriteMovies.Models
{
    public class RestrictedDate : ValidationAttribute
    {
        public override bool IsValid(object date) 
        {
            return (DateTime)date >= DateTime.Now;
        }
    }
    public class Event
    {
        [Key]

        public int EventId {get; set;}



        [Required(ErrorMessage="Please enter a title")]
        [MinLength(3,ErrorMessage="please make a title 3 characters or more.")]
        public string Title {get;set;}

        
        [Required(ErrorMessage="Please enter some sort of a description")]
        public string Description {get;set;}


        [Required(ErrorMessage="Please enter duration time")]
        public int Duration {get;set;}


        [Required(ErrorMessage="Please enter time you want to save ")]
        public TimeSpan Time{ get; set; } 


        [Required(ErrorMessage="Please enter a date")]
        [RestrictedDate]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date{get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;



        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public int UserId{get;set;}
        public User PostedBy {get;set;}

        public List<Like> Likes {get;set;}

        


    }

}