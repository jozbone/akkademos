namespace MovieStreaming.Common.Actors
{
    using System;

    using Akka.Actor;

    using MovieStreaming.Common.Messages;

    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor(int userId)
        {
            this.UserId = userId;
            Console.WriteLine("Creating a UserActor");
            ColorConsole.WriteLineYellow("Setting initial behavior to stopped.");
            this.Stopped();
        }

        public int UserId { get; }

        private void Playing()
        {
            this.Receive<PlayMovieMessage>(
                message =>
                ColorConsole.WriteLineRed("Error: cannot start playing a movie before stopping existing one."));
            this.Receive<StopMovieMessage>(message => this.StopPlayingCurrentMovie());
            ColorConsole.WriteLineYellow($"UserActor is now in playing state. [Playing: {this._currentlyWatching}].");
        }

        private void Stopped()
        {
            this.Receive<PlayMovieMessage>(message => this.StartPlayingMovie(message.MovieTitle));
            this.Receive<StopMovieMessage>(
                message => ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing."));
            ColorConsole.WriteLineYellow($"UserActor '{this.UserId}' has now become Stopped.");
        }

        private void StartPlayingMovie(string title)
        {
            this._currentlyWatching = title;
            ColorConsole.WriteLineYellow($"User is currently watching '{this._currentlyWatching}'.");
            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(title));
            this.Become(this.Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User has stopped watching '{this._currentlyWatching}'.");
            this._currentlyWatching = null;
            this.Become(this.Stopped);
        }

        #region lifecycle events

        protected override void PreStart()
        {
            ColorConsole.WriteLineYellow($"UserActor '{this.UserId}' PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow($"UserActor '{this.UserId}' PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow($"UserActor '{this.UserId}' PreRestart [reason:{reason}]");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow($"UserActor '{this.UserId}' PostRestart [reason:{reason}]");
            base.PostRestart(reason);
        }

        #endregion
    }
}