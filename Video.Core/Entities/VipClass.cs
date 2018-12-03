using System;
using System.Collections.Generic;
using System.Text;
using Video.Core.Enum;

namespace Video.Core.Entities
{
    /// <summary>
    /// 会员等级信息
    /// </summary>
  public  class VipClass:Entity
    {
        /// <summary>
        /// 会员类型
        /// </summary>
        public VipType VipType { get; set; }
        /// <summary>
        /// 收费标准
        /// </summary>
        public Decimal Fee { get; set; }
        /// <summary>
        /// 试看时长
        /// </summary>
        public int PreRead { get; set; }
    }
}
