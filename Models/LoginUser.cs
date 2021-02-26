using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace favoriteMovies.Models
{
    public class LoginUser
    {
  

    
        [Required(ErrorMessage="please provide your Email")]
        [EmailAddress(ErrorMessage = "please make sure its a real email")]
        [Display(Name="Email")]
        public string LoginEmail {get; set;}


        [Required(ErrorMessage="please provide your password")]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string LoginPassword{get;set;}

      
        

        


    }

}