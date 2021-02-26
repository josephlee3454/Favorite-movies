using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace favoriteMovies.Models
{

    public class User
    {
        [Key]
        public int UserId {get; set;}

        //first name

        [Required(ErrorMessage="please provide your first name ")]
        [MinLength(2,ErrorMessage = "please ,ake sure its more than 2 characters")]
        [Display(Name="First Name")]
        public string FirstName {get;set;}
        ///Last name 

        [Required(ErrorMessage="please provide your Last name ")]
        [MinLength(2,ErrorMessage = "please ,ake sure its more than 2 characters")]
        [Display(Name="Last Name")]
        public string LastName {get;set;}
        // email

        [Required(ErrorMessage="please provide your Email")]
        [EmailAddress(ErrorMessage = "please make sure its a real email")]
        public string Email {get;set;}
        // pass

        [Required(ErrorMessage="please provide a password ")]
        [MinLength(8,ErrorMessage = "please make sure its more than 8 characters")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        //confirm
        [NotMapped]
        [Required(ErrorMessage="you need to provide a password confirmation")]
        [Compare("Password",ErrorMessage = "please make sure it matches the previous password")]
        [DataType(DataType.Password)]
        public string confirm {get;set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public List<Movie> PostedMovies {get;set;}


        public List<Like> Likes {get;set;}

        

        


    }

}