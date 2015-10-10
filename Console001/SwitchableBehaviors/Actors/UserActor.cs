using System;
using System.Threading.Tasks;
using Akka.Actor;
using SwitchableBehaviors.Messages;

namespace SwitchableBehaviors.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");
            ColorConsole.WriteLineCyan("Setting initial behavior to stopped.");
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message => ColorConsole.WriteLineRed("Error: cannot start playing a movie before stopping existing one."));
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
            ColorConsole.WriteLineCyan("UserActor is now in playing state.");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing"));
            ColorConsole.WriteLineCyan("UserActor has now become Stopped");
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;
            ColorConsole.WriteLineYellow($"User is currently watching '{_currentlyWatching}'");
            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User has stopped watching '{_currentlyWatching}'");
            _currentlyWatching = null;
            Become(Stopped);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen($"UserActor PreRestart [reason:{reason}]");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen($"UserActor PostRestart [reason:{reason}]");
            base.PostRestart(reason);
        }

    }
}