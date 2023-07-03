using System.ComponentModel.DataAnnotations;

namespace API_TESTE.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public List<Like>? Likes { get; set; }

        public List<Tweet>? Tweets { get; set; }

        public List <Comment>? Comments { get; set; }



       
    }
}


