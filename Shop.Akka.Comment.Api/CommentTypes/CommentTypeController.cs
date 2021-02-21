using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;
using Utility.Domain.Uow;
using Utility.Enums;
using Utility.Extensions;
using Utility.ObjectMapping;
using Utility.Response;

namespace Shop.CommentTypes
{
    [Route("api/v{version:apiVersion}/admin/comment_type")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CommentTypeController : ControllerBase
    {
        private IUnitWork unitWork;
        //private IObjectMapper objectMapper;

        public CommentTypeController(IUnitWork unitWork/*, IObjectMapper objectMapper*/)
        {
            this.unitWork = unitWork;
            //this.objectMapper = objectMapper;
        }
        /// <summary>
        /// 添加 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Insert([FromBody] CommentType create)
        {
            create.Id = Guid.NewGuid().ToString("N");
            create.CreationTime = DateTime.Now;
            unitWork.Insert(create); 
            int res = 1;
            return ResponseApi.Create(Language.Chinese, res > 0 ? Code.AddSuccess : Code.AddFail);
        }

        /// <summary>
        /// 修改 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Update([FromBody] CommentType update)
        {
            update.LastModificationTime = DateTime.Now;
            unitWork.Update(update);
            int res = 1;
            return ResponseApi.Create(Language.Chinese, res > 0 ? Code.ModifySuccess : Code.ModifyFail);
        }

        [HttpGet("delete/{id}")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Delete(string id)
        {
            unitWork.Delete<CommentType>(id);
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);
        }

        [HttpPost("delete_list")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi DeleteList([FromBody] DeleteEntity<string> ids)
        {
            Expression<Func<CommentType, bool>> where=null;
            foreach (var item in ids.Ids)
            {
                where = LinqExpression.Or(where, it => it.Id == item);
            }
            unitWork.Update<CommentType>(where,it=>new CommentType() {
                LastModificationTime=DateTime.Now,
                IsDeleted=true});
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);
        }

        //[HttpPost("find")]

        //public virtual ResponseApi<IList<GetOutput>> Find([FromBody] GetInput input)
        //{
        //    var res = service.Find(input);
        //    return ResponseApi<IList<GetOutput>>.Create(Language.Chinese, Code.QuerySuccess).SetData(res);
        //}

        //[HttpPost("find/{page}/{size}")]
        //public virtual ResponseApi<IList<GetOutput>> FindByPage(GetInput input, int page = 1, int size = 10)
        //{
        //    var res = service.FindByPage(input, page, size);
        //    return ResponseApi<IList<GetOutput>>.Create(Language.Chinese, Code.QuerySuccess).SetData(res);
        //}

        [HttpPost("find/{page}/{size}")]
        public virtual ResponseApi<ResultDto<CommentType>> FindResultDtoByPage(CommentType input, int page = 1, int size = 10)
        {
            var data = unitWork.FindByPage<CommentType>(null, page, size).ToList();
            var count = unitWork.Count<CommentType>(null);
            var res= new ResultDto<CommentType>()
            {
                Data = data,
                Result = new PageResultDto(page, size,
                count / size == 0 ? count / size : count / size + 1, count)
            };
            return ResponseApi<ResultDto<CommentType>>.Create(Language.Chinese, Code.QuerySuccess).SetData(res);
        }



        //[HttpPost("count")]
        //[ProducesResponseType(typeof(int), 200)]
        //public virtual int Count(GetInput input)
        //{
        //    int res = service.Count(input);
        //    return res;
        //}
    }
}
