using System;
using System.Threading;
using Akka.Actor;
using Console001.Actors;
using Console001.Messages;

namespace Console001
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created.");
//            Props playbackActorProps = Props.Create<PlaybackActor>();
//            IActorRef playActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            Props playbackReceiveActorProps = Props.Create<PlaybackReceiveActor>();
            IActorRef playbackReceiveActorRef = MovieStreamingActorSystem.ActorOf(playbackReceiveActorProps, "PlaybackReceiveActor");

            playbackReceiveActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 41));

            Console.ReadLine();
            MovieStreamingActorSystem.Shutdown();
            MovieStreamingActorSystem.AwaitTermination();
        }
    }
}
