
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.CommentTypes
{
	/// <summary>
	/// 评论类型 实体
	/// </summary>
	[Table("t_comment_type")]
	public class CommentType:BaseEntity
    {
		/// <summary>
		/// 编码
		/// </summary>
		[Column("code")]
		[System.ComponentModel.DataAnnotations.StringLength(10)]
		public virtual string Code { get; set; }
	
		/// <summary>
		/// 名称 
		/// </summary>
		[Column("name")]
		[System.ComponentModel.DataAnnotations.StringLength(20)]
		public virtual string Name { get; set; }

		/// <summary>
		/// 状态 
		/// </summary>
		[Column("status")]
		[System.ComponentModel.DataAnnotations.StringLength(1)]
		public virtual string Status { get; set; }

	}
}
