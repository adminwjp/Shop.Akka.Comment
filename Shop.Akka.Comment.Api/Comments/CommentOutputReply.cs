using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;

namespace Shop.Comments
{
    public class CommentOutputReply
    {
        public CommentOutputReply(ResultDto<Comment> rsult, IActorRef hasher)
        {
            Result = rsult;
            Comment = hasher;
        }

        public ResultDto<Comment> Result { get; }
        public IActorRef Comment { get; }
    }
}
