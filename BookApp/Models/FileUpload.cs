using System.Reflection.Metadata;
namespace BookApp.Models
{
    public class FileUpload
    {
         public int Id { get; set; }
         public  string  FileName  { get; set; }
           public byte[] Content { get; set; }
         public DateTime CreatedOn {get;set;}

    }
}