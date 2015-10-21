namespace MovieStreaming.Common.Messages
{
    public class IncrementPlayCountMessage
    {
        public IncrementPlayCountMessage(string movieTitle)
        {
            this.MovieTitle = movieTitle;
        }

        public string MovieTitle { get; private set; }
    }
}