﻿namespace MovieStreaming.Common.Messages
{
    public class PlayMovieMessage
    {
        public PlayMovieMessage(string movieTitle, int userId)
        {
            this.MovieTitle = movieTitle;
            this.UserId = userId;
        }

        public string MovieTitle { get; private set; }

        public int UserId { get; private set; }
    }
}