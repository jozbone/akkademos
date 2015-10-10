using System;
using Akka.Actor;
using LifecycleEventsConsole.Actors;
using LifecycleEventsConsole.Messages;

namespace LifecycleEventsConsole
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem2");
            Console.WriteLine("Actor system created.");

            Props props = Props.Create<PlaybackActor>();
            IActorRef actorRef = MovieStreamingActorSystem.ActorOf(props, "PlaybackActor");

            actorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 42));
            actorRef.Tell(new PlayMovieMessage("Partial Recall", 99));
            actorRef.Tell(new PlayMovieMessage("Boolean Lies", 77));
            actorRef.Tell(new PlayMovieMessage("Codenan the Destroyer", 1));

            actorRef.Tell(PoisonPill.Instance);

            //Console.WriteLine("Press any key to start the shutdown of the system");
            Console.ReadKey();

            // Tell actor system (and all child actors) to shutdown
            MovieStreamingActorSystem.Shutdown();
            // Wait for actor system to finish shutting down
            MovieStreamingActorSystem.AwaitTermination();
            Console.WriteLine("Actor system has shutdown");

            Console.ReadKey();
        }
    }
}
