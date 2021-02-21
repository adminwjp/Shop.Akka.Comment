using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Comments
{
    /// <summary>
    /// akka.net 支持 一个参数
    /// </summary>
    public interface IPublicCommentService
    {
        Task<CommentReply> Add(Comment comment, CancellationToken token);

        Task<CommentReply> Update(Comment comment, CancellationToken token);

        Task<CommentReply> Delete(string id, CancellationToken token);

        Task<CommentReply> Delete(string[] ids, CancellationToken token);

        Task<CommentOutputReply> Find(Comment comment, CancellationToken token);

        
    }
}
