using Akka.Actor;
using Akka.Event;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Comments
{
    public class CommentActor: ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private readonly IServiceScope _scope;
        private readonly ICommentService _commentService;
        private ICancelable _cancel;

        public CommentActor(IServiceProvider sp)
        {
            _scope = sp.CreateScope();
            _commentService = _scope.ServiceProvider.GetRequiredService<ICommentService>();

            Receive<Comment>(it =>
            {
                if (it.EnablePage)
                {
                    var res = _commentService.Find(it, it.Page, it.Size);
                    Sender.Tell(new CommentOutputReply(res, Self));
                    _cancel?.Cancel();
                    _cancel = null;
                }
                else if (it.Save)
                {
                    var res=_commentService.Update(it);
                    Sender.Tell(new CommentReply(res, Self));
                    _cancel?.Cancel();
                    _cancel = null;
                }
                else
                {
                    var res = _commentService.Add(it);
                    Sender.Tell(new CommentReply(res, Self));
                    _cancel?.Cancel();
                    _cancel = null;
                }
                
            });


            Receive<string>(it =>
            {
                var res = _commentService.Delete(it);
                Sender.Tell(new CommentReply(res, Self));
                _cancel?.Cancel();
                _cancel = null;
            });

            Receive<string[]>(it =>
            {
                var res = _commentService.Delete(it);
                Sender.Tell(new CommentReply(res, Self));
                _cancel?.Cancel();
                _cancel = null;
            });

            if (_cancel == null)
            {
                _cancel = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.Zero, TimeSpan.FromSeconds(10), Self, new GetToken(), Self);
            }
        }

      

        public class GetToken
        {
            public string Token { get; set; }
        }

        protected override void PostStop()
        {
            _scope.Dispose();

            // _hashService should be disposed once the IServiceScope is disposed too
            _log.Info("Terminating. Is ScopedService disposed? {0}", _commentService.IsDisposed);
        }
    }
}
