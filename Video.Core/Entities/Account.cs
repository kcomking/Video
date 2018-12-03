using System;
using System.Collections.Generic;
using System.Text;
using Video.Core.Enum;

namespace Video.Core.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
   public class Account:Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImage { get; set; }

        public int Score { get; set; }

        /// <summary>
        /// 会员类型
        /// </summary>
        public VipType VipType { get; set; }
        /// <summary>
        /// 会员过期时间
        /// </summary>

        public DateTime VipExpirationDate { get; set; }
        /// <summary>
        /// 是否总代
        /// </summary>
        public bool IsGeneralAgent { get; set; }
        /// <summary>
        /// 总代ID
        /// </summary>
        public int? GeneralAgentId { get; set; }
        /// <summary>
        /// 免费观看数量
        /// </summary>
        public int FreeVideoCount { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 三级代理
        /// </summary>
        public int? Leader3Id { get; set; }
        /// <summary>
        /// 一级代理
        /// </summary>
        public int? Leader1Id { get; set; }
        /// <summary>
        /// 二级代理
        /// </summary>
        public int? Leader2Id { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 推广码
        /// </summary>
        public string PromoCode { get; set; }
        /// <summary>
        /// 是否分享
        /// </summary>
        public bool IsShared { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

        public Account Leader1 { get; set; }
        public Account Leader2 { get; set; }
        public Account Leader3 { get; set; }

        public Account GeneralAgent { get; set; }


        public ICollection<Account> Subordinates { get; set; }
        public ICollection<Account> Subordinates1 { get; set; }
        public ICollection<Account> Subordinates2 { get; set; }
        public ICollection<Account> Subordinates3 { get; set; }

    }
}
