namespace MovieStreaming
{
    using System;
    using System.Threading;

    using Akka.Actor;

    using MovieStreaming.Common;
    using MovieStreaming.Common.Actors;
    using MovieStreaming.Common.Messages;

    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem");
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created.");

            ColorConsole.WriteLineGray("Creating actor supervisory hierarchy");
            MovieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            do
            {
                ShortPause();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ColorConsole.WriteLineGray("Enter a command and hit enter");

                var command = Console.ReadLine();
                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("exit"))
                {
                    MovieStreamingActorSystem.Shutdown();
                    MovieStreamingActorSystem.AwaitTermination();
                    ColorConsole.WriteLineGray("Actor system shutdown");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

            } while (true);

        }

        private static void ShortPause()
        {
            Thread.Sleep(2000);
        }
    }
}