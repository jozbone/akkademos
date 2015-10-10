using System;
using Akka.Actor;
using SwitchableBehaviors.Actors;
using SwitchableBehaviors.Messages;

namespace SwitchableBehaviors
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem2");
            Console.WriteLine("Actor system created.");

            Props userProps = Props.Create<UserActor>();
            IActorRef actorRef = MovieStreamingActorSystem.ActorOf(userProps, "UserActor");

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Codenan the Destroyer)");
            actorRef.Tell(new PlayMovieMessage("Codenan the Destroyer", 42));
            
            Console.ReadKey();
            Console.WriteLine("Sending another PlayMovieMessage (Boolean Lies)");
            actorRef.Tell(new PlayMovieMessage("Boolean Lies", 42));

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            actorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending another PlayMovieMessage (Boolean Lies)");
            actorRef.Tell(new PlayMovieMessage("Boolean Lies", 42));

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            actorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            actorRef.Tell(new StopMovieMessage());

            // Tell actor system (and all child actors) to shutdown
            MovieStreamingActorSystem.Shutdown();
            // Wait for actor system to finish shutting down
            MovieStreamingActorSystem.AwaitTermination();
            Console.WriteLine("Actor system has shutdown");

            Console.ReadKey();
        }
    }
}
