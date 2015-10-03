using System;
using Akka.Actor;
using Console001.Messages;

namespace Console001.Actors
{
    public class PlaybackReceiveActor : ReceiveActor
    {
        public PlaybackReceiveActor()
        {
            Console.WriteLine("Creating a PlaybackReceiveActor");
            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message), message => message.UserId == 41);
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            Console.WriteLine("Received movie title: {0}", message.MovieTitle);
            Console.WriteLine("Received user id: {0}", message.UserId);
        }
    }
}