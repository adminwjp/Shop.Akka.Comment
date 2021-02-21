using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;
using Utility.Enums;
using Utility.Response;

namespace Shop.Comments
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CommentController : ControllerBase
    {
        private IPublicCommentService commentService;

        public CommentController(IPublicCommentService commentService)
        {
            this.commentService = commentService;
        }
        /// <summary>
        /// 添加  A task was canceled
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Insert([FromBody] Comment create)
        {
            create.Save = false;
            create.EnablePage = false;
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            int res = commentService.Add(create, cts.Token).Result.Result;
            return ResponseApi.Create(Language.Chinese, res > 0 ? Code.AddSuccess : Code.AddFail);
        }

        /// <summary>
        /// 修改 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Update([FromBody] Comment update)
        {
            update.Save = true;
            update.EnablePage = false;
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            int res = commentService.Update(update, cts.Token).Result.Result;
            return ResponseApi.Create(Language.Chinese, res > 0 ? Code.ModifySuccess : Code.ModifyFail);
        }

        [HttpGet("delete/{id}")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Delete(string id)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            int res = commentService.Delete(id, cts.Token).Result.Result;
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);
        }

        [HttpPost("delete_list")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi DeleteList([FromBody] DeleteEntity<string> ids)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            int res = commentService.Delete(ids.Ids, cts.Token).Result.Result;
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
        public virtual ResponseApi<ResultDto<Comment>> FindResultDtoByPage(Comment input, int page = 1, int size = 10)
        {
            input.Save = false;
            input.EnablePage = true;
            input.Page = page;
            input.Size = size;
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var res = commentService.Find(input, cts.Token).Result.Result;
            return ResponseApi<ResultDto<Comment>>.Create(Language.Chinese, Code.QuerySuccess).SetData(res);
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
