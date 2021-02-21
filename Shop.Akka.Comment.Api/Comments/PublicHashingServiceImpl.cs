using Akka.Actor;
using Akka.Configuration;
using Akka.DependencyInjection;
using Akka.Routing;
using Microsoft.Extensions.Hosting;
using Shop.Akka.Comment.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Comments
{
    public class PublicHashingServiceImpl : IPublicCommentService//, IHostedService
    {
        private ActorSystem _actorSystem;
        public IActorRef RouterActor { get; private set; }
        private readonly IServiceProvider _sp;

        //public PublicHashingServiceImpl(IServiceProvider sp)
        //{
        //    _sp = sp;
        //}

        public PublicHashingServiceImpl()
        {
            _actorSystem = Startup.ActorSystem;
            RouterActor = Startup.RouterActor;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var hocon = ConfigurationFactory.ParseString(await File.ReadAllTextAsync("app.conf", cancellationToken));
            var bootstrap = BootstrapSetup.Create().WithConfig(hocon);
            var di = ServiceProviderSetup.Create(_sp);
            var actorSystemSetup = bootstrap.And(di);
            _actorSystem = ActorSystem.Create("AspNetDemo", actorSystemSetup);
            // </AkkaServiceSetup>

            // <ServiceProviderFor>
            // props created via IServiceProvider dependency injection
            var hasherProps = ServiceProvider.For(_actorSystem).Props<CommentActor>();
            RouterActor = _actorSystem.ActorOf(hasherProps.WithRouter(FromConfig.Instance), "hasher");
            // </ServiceProviderFor>

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {

            // theoretically, shouldn't even need this - will be invoked automatically via CLR exit hook
            // but it's good practice to actually terminate IHostedServices when ASP.NET asks you to
            await CoordinatedShutdown.Get(_actorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance);
        }

        

        public async Task<CommentReply> Add(Comment comment, CancellationToken token)
        {
            return await RouterActor.Ask<CommentReply>(comment, token);
        }

        public async Task<CommentReply> Update(Comment comment, CancellationToken token)
        {
            return await RouterActor.Ask<CommentReply>(comment, token);
        }

        public async Task<CommentReply> Delete(string id, CancellationToken token)
        {
            return await RouterActor.Ask<CommentReply>(id, token);
        }

        public async Task<CommentReply> Delete(string[] ids, CancellationToken token)
        {
            return await RouterActor.Ask<CommentReply>(ids, token); ;
        }

        public async Task<CommentOutputReply> Find(Comment comment, CancellationToken token)
        {
            return await RouterActor.Ask<CommentOutputReply>(comment, token); ;
        }
    }
}
