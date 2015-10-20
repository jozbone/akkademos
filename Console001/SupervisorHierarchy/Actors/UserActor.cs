using System;
using Akka.Actor;
using SupervisorHierarchy.Messages;

namespace SupervisorHierarchy.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;
        public int UserId { get; private set; }

        public UserActor(int userId)
        {
            this.UserId = userId;
            Console.WriteLine("Creating a UserActor");
            ColorConsole.WriteLineYellow("Setting initial behavior to stopped.");
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message => ColorConsole.WriteLineRed("Error: cannot start playing a movie before stopping existing one."));
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
            ColorConsole.WriteLineYellow($"UserActor is now in playing state. [Playing: {_currentlyWatching}].");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing."));
            ColorConsole.WriteLineYellow($"UserActor '{UserId}' has now become Stopped.");
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;
            ColorConsole.WriteLineYellow($"User is currently watching '{_currentlyWatching}'.");
            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(title));
            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User has stopped watching '{_currentlyWatching}'.");
            _currentlyWatching = null;
            Become(Stopped);
        }

        #region lifecycle events
        protected override void PreStart()
        {
            ColorConsole.WriteLineYellow($"UserActor '{UserId}' PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow($"UserActor '{UserId}' PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow($"UserActor '{UserId}' PreRestart [reason:{reason}]");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow($"UserActor '{UserId}' PostRestart [reason:{reason}]");
            base.PostRestart(reason);
        }
        #endregion
    }
}