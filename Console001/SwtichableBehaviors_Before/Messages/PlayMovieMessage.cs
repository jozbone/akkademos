namespace SwtichableBehaviors_Before.Messages
{
    public class PlayMovieMessage
    {
        public PlayMovieMessage(string movieTitle, int userId)
        {
            MovieTitle = movieTitle;
            UserId = userId;
        }

        public string MovieTitle { get; private set; }
        public int UserId { get; private set; }
    }

    public class StopMovieMessage
    {

    }
}
