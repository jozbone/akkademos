using System;
using System.Collections.Generic;
using Akka.Actor;
using SupervisorHierarchy.Messages;

namespace SupervisorHierarchy.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users = new Dictionary<int, IActorRef>();

        public UserCoordinatorActor()
        {
            Receive<PlayMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);
                IActorRef childActorRef = _users[message.UserId];
                childActorRef.Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);
                IActorRef childActorRef = _users[message.UserId];
                childActorRef.Tell(message);
            });
        }

        private void CreateChildUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                IActorRef newChildActorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), $"User{userId}");
                _users.Add(userId, newChildActorRef);

                ColorConsole.WriteLineCyan($"UserCoordinatorActor create new child UserActor for {userId} (Total users: {_users.Count})");
            }
        }

        #region Lifecycle hooks

        protected override void PreStart()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineCyan($"UserCoordinatorActor PreRestart [reason:{reason}]");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineCyan($"UserCoordinatorActor PostRestart [reason:{reason}]");
            base.PostRestart(reason);
        }

        #endregion

    }
}