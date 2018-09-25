using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Exam
{
  public class User
  {
    [Key]
    public int UserId {get; set;}
    [Required]
    [MinLength(1)]
    public string UserName {get; set;}
    [Required]
    [MinLength(1)]
    public string Alias {get; set;}
    [Required]
    [MinLength(1)]
    [EmailAddress]
    public string Email {get; set;}    
    [Required]
    [MinLength(8)]
    public string Password {get; set;} 
    [Required]
    [NotMapped]
    [Compare("Password")]
    public string Confirm { get; set; }
    public DateTime Created_At {get; set;}
    public DateTime Updated_At {get; set;}
    public List<Like> UserLikes {get; set;}
    public List<Idea> UsersIdeas {get; set;}
    public User()
    {
      Created_At = DateTime.Now;
      Updated_At = DateTime.Now;
      UserLikes = new List<Like>();
      UsersIdeas = new List<Idea>();
    }
  }
}