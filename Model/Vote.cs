
namespace Apka2.Model
{
    public class Vote
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Law Law { get; set; }
        public VoteType VoteType { get; set; }
    }
}
