using System.ComponentModel.DataAnnotations;

namespace API_TESTE.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        // Relacionamento com o User
        public string? UserId { get; set; }
        public User? User { get; set; }

        // Relacionamento com o Tweet
        public int? TweetId { get; set; }
        public Tweet? Tweet { get; set; }
    }
}
