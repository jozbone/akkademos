namespace MovieStreaming.Remote
{
    using Akka.Actor;

    using MovieStreaming.Common;

    internal class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        private static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem in remote process");
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            MovieStreamingActorSystem.AwaitTermination();
        }
    }
}