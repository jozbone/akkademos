using System;
using Akka.Actor;
using SwitchableBehaviors.Messages;

namespace SwitchableBehaviors.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");
            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            ColorConsole.WriteLineYellow($"PlayMovieMessage '{message.MovieTitle}' for user {message.UserId}");
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
