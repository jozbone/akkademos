namespace MovieStreaming.Common.Actors
{
    using System;

    using Akka.Actor;

    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                exception =>
                    {
                        if (exception is SimulatedCorruptStateException)
                        {
                            return Directive.Restart;
                        }

                        if (exception is Exceptions.SimulatedTerribleMovieException)
                        {
                            return Directive.Resume;
                        }

                        return Directive.Restart;
                    });
        }

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