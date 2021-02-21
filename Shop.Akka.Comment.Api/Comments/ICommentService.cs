using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;

namespace Shop.Comments
{
    public interface ICommentService: IDisposable
    {
        bool IsDisposed { get; }

        int Add(Comment comment);

        int Update(Comment comment);

        int Delete(string id);

        int Delete(string[] ids);

        ResultDto<Comment> Find(Comment comment,int page,int size);
    }
}
