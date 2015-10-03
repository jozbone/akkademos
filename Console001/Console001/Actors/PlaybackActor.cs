using System.Threading.Tasks;
using Console001.Messages;

namespace Console001.Actors
{
    using System;
    using Akka.Actor;

    public class PlaybackActor : UntypedActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");
        }

        protected override void OnReceive(object message)
        {
            if (message is PlayMovieMessage)
            {
                var m = message as PlayMovieMessage;
                Console.WriteLine("Received movie title: {0}", m.MovieTitle);
                Console.WriteLine("Received user id: {0}", m.UserId);
            }
            else
            {
                Unhandled(message);
            }
        }
    }
}