using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Domain.Uow;
using Utility.Extensions;

namespace Shop.Comments
{
    public class CommentServiceImpl: ICommentService
    {
        private bool _isDisposed;
        private IUnitWork unitWork;

        public CommentServiceImpl(IUnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        public void Dispose()
        {
            _isDisposed = true;
        }

        void Check()
        {
            if (_isDisposed)
                throw new ObjectDisposedException("CommentServiceImpl disposed");
        }

        public int Add(Comment comment)
        {
            comment.Id = Guid.NewGuid().ToString("N");
            comment.CreationTime = DateTime.Now;
            unitWork.Insert(comment);
            return 1;
        }

        public int Update(Comment comment)
        {
            comment.LastModificationTime = DateTime.Now;
            unitWork.Update(comment);
            return 1;
        }

        public int Delete(string id)
        {
            unitWork.Delete<Comment>(id);
            return 1;
        }

        public int Delete(string[] ids)
        {
            Expression<Func<Comment, bool>> where = null;
            foreach (var item in ids)
            {
                where = LinqExpression.Or(where,it => it.Id == item);
            }
            unitWork.Update<Comment>(where,it=>new Comment() { 
                LastModificationTime=DateTime.Now,
                IsDeleted=true
            });
            return 1;
        }

        public ResultDto<Comment> Find(Comment comment, int page, int size)
        {
            var data= unitWork.FindByPage<Comment>(null, page, size).ToList();
            var count = unitWork.Count<Comment>(null);
            return new ResultDto<Comment>()
            {
                Data = data,
                Result = new PageResultDto(page, size, 
                count / size == 0 ? count / size : count / size + 1, count)
            };
        }

        public bool IsDisposed => _isDisposed;
    }
}
