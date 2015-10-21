namespace MovieStreaming.Common.Actors
{
    using System;
    using System.Collections.Generic;

    using Akka.Actor;

    using MovieStreaming.Common.Messages;

    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            ColorConsole.WriteMagenta("MoviePlayCounterActor constructor executing");
            this._moviePlayCounts = new Dictionary<string, int>();
            this.Receive<IncrementPlayCountMessage>(message => this.HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (this._moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                this._moviePlayCounts[message.MovieTitle]++;
            }
            else
            {
                this._moviePlayCounts.Add(message.MovieTitle, 1);
            }

            if (this._moviePlayCounts[message.MovieTitle] > 3)
            {
                throw new SimulatedCorruptStateException();
            }

            if (message.MovieTitle.Equals("Partial Recoil", StringComparison.OrdinalIgnoreCase))
            {
                throw new SimulatedTerribleMovieException();
            }

            ColorConsole.WriteMagenta(
                $"MoviePlayCounterActor '{message.MovieTitle}' has been watched {this._moviePlayCounts[message.MovieTitle]}");
        }

        #region Lifecycle hooks

        protected override void PreStart()
        {
            ColorConsole.WriteMagenta("MoviePlayCounterActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteMagenta("MoviePlayCounterActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteMagenta($"MoviePlayCounterActor PreRestart because: {reason.Message}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteMagenta($"MoviePlayCounterActor PostRestart because: {reason.Message}");

            base.PostRestart(reason);
        }
        #endregion
    }
}