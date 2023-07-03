using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace API_TESTE.Models
{
    public class Tweet
    {
        [Key]
        public int TweetId { get; set; }

        public string TweetText { get; set; }

        public string CreateTime { get; set; }



        public int? Like { get; set; }
        // Relacionamento com   Comment

        public ICollection<Comment>? Comments { get; set; }

        // Relacionamento com Likes
        public ICollection<Like>? Likes { get; set; }

        
        // Propriedade de navegação para o User associado
        public string UserId { get; set; }
        public User? User { get; set; }
    }


}
