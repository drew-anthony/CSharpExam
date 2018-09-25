using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Exam.Models;

namespace Exam
{
  public class Like
  {
    [Key]
    public int LikeId {get; set;}

    public int UserId {get; set;}
    public User User {get; set;}

    public int IdeasId {get; set;}
    public Idea Idea {get; set;}
  }
}