using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    /// <summary>
    /// 步骤信息
    /// </summary>
    public struct StepInfo
    {
        #region 属性
        /// <summary>
        /// 步骤函数
        /// </summary>
        public FunctionInfo function;
        /// <summary>
        /// 跳转状态
        /// </summary>
        public List<SkipInfo> skips;
        #endregion
    }
}
