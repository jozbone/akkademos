using System;
using Akka.Actor;
using SwtichableBehaviors_Before.Messages;

namespace SwtichableBehaviors_Before.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");
            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
            Receive<StopMovieMessage>(message => HandleStopMovieMessage());
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            if (_currentlyWatching != null)
            {
                ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping existing one");
            }
            else
            {
                StartPlayingMovie(message.MovieTitle);
            }
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;
            ColorConsole.WriteLineYellow($"User is currently watching '{_currentlyWatching}'");
        }

        private void HandleStopMovieMessage()
        {
            if (_currentlyWatching == null)
            {
                ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing");
            }
            else
            {
                StopPlayingCurrentMovie();
            }
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User has stopped watching '{_currentlyWatching}'");
            _currentlyWatching = null;
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen($"PlaybackActor PreRestart [reason:{reason}]");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen($"PlaybackActor PostRestart [reason:{reason}]");
            base.PostRestart(reason);
        }
    }
}
