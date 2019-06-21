namespace Apka2.Model
{
    //public enum VoteType
    //{
    //    Accept ,
    //    Against,
    //    Paused 

    //}

    public class VoteType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Law Law { get; set; }

    }

}
