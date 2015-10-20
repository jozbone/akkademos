namespace SupervisorHierarchy.Actors
{
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

            ColorConsole.WriteMagenta(
                $"MoviePlayCounterActor '{message.MovieTitle}' has been watched {this._moviePlayCounts[message.MovieTitle]}");
        }
    }
}