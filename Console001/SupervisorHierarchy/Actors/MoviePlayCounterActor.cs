namespace SupervisorHierarchy.Actors
{
    using System;
    using System.Collections.Generic;

    using Akka.Actor;

    using SupervisorHierarchy.Messages;

    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
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
    }

    public class SimulatedTerribleMovieException : Exception
    {
    }

    public class SimulatedCorruptStateException : Exception
    {
    }
}