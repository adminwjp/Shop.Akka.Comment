using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Comments
{
	/// <summary>
	/// 评论 实体
	/// </summary>
   [Table("t_comment")]
	public class Comment:BaseEntity
    {
		/// <summary>
		/// 回复消息
		/// </summary>
		[Column("reply")]
		[System.ComponentModel.DataAnnotations.StringLength(500)]
		public virtual string Reply { get; set; }

		/// <summary>
		/// 订单详情id
		/// </summary>
		[Column("order_detail_id")]
		[System.ComponentModel.DataAnnotations.StringLength(36)]
		public virtual string OrderDetailID { get; set; }

		/// <summary>
		/// 昵称
		/// </summary>
		[Column("nickname")]
		[System.ComponentModel.DataAnnotations.StringLength(20)]
		public virtual string Nickname { get; set; }
		/// <summary>
		/// 点赞数
		/// </summary>
		[Column("star")]
		public virtual int Star { get; set; }

		/// <summary>
		/// 商品id
		/// </summary>
		[Column("product_id")]
		[System.ComponentModel.DataAnnotations.StringLength(36)]
		public virtual string ProductID { get; set; }

		/// <summary>
		/// 订单id
		/// </summary>
		[Column("order_id")]
		[System.ComponentModel.DataAnnotations.StringLength(36)]
		public virtual string OrderID { get; set; }

		/// <summary>
		/// 评论内容
		/// </summary>
		[Column("content")]
		[System.ComponentModel.DataAnnotations.StringLength(500)] 
		public virtual string Content { get; set; }

		/// <summary>
		/// 状态 
		/// </summary>
		[Column("status")]
		[System.ComponentModel.DataAnnotations.StringLength(1)]
		public virtual string Status { get; set; }

		/// <summary>
		/// 账户
		/// </summary>
		[Column("account")]
		[System.ComponentModel.DataAnnotations.StringLength(20)]
		public virtual string Account { get; set; }

		/// <summary>
		/// akka.net 需要 不然要定义不同实体
		/// </summary>
		[NotMapped]
		[Newtonsoft.Json.JsonIgnore]
		public bool Save { get; set; }

		[NotMapped]
		[Newtonsoft.Json.JsonIgnore]
		public bool EnablePage { get; set; }

		[NotMapped]
		[Newtonsoft.Json.JsonIgnore]
		public int Page { get; set; }

		[NotMapped]
		[Newtonsoft.Json.JsonIgnore]
		public int Size { get; set; }

	}
}
