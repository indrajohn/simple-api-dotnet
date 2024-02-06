using System.ComponentModel.DataAnnotations;

public class Message
{
   // [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
    public int Id { get; set; }
    
  //  [Required(ErrorMessage = "Text is required.")]
    public string Text { get; set; }
}