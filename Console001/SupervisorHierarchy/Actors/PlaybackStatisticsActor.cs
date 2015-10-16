using System;
using Akka.Actor;

namespace SupervisorHierarchy.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        #region Lifecycle hooks

        protected override void PreStart()
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineWhite($"PlaybackStatisticsActor PreRestart [reason:{reason}]");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineWhite($"PlaybackStatisticsActor PostRestart [reason:{reason}]");
            base.PostRestart(reason);
        }

        #endregion

    }
}