using API_TESTE.Models;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace API_TESTE.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public string CreateTime { get; set; }

        // Propriedade de navegação para o User associado

        public User? User { get; set; }
        public string UserId { get; set; }

        // Propriedade de navegação para o Tweet associado
        public int TweetId { get; set; }
        public Tweet? Tweet { get; set; }

        // Relacionamento com Replies
        public ICollection<Comment>? Replies { get; set; }
    }

}