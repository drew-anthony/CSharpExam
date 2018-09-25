using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exam.Models;

namespace Exam
{
  public class Idea
  {
    [Key]
    public int IdeasId {get; set;}
    public string UserIdea {get; set;}
    [ForeignKey("UserId")]
    public int UserId {get; set;}
    public User Owner {get; set;}
    public List<Like> LikedBy {get; set;}
    public Idea()
    {
      LikedBy = new List<Like>();
    }
  }
}