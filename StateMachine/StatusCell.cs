using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    /// <summary>
    /// 状态单元
    /// </summary>
    public class StatusCell
    {
        #region 属性
        public string name;
        /// <summary>
        /// 过程块
        /// </summary>
        public Queue<FunctionInfo> pass;
        /// <summary>
        /// 跳转块
        /// </summary>
        public Queue<SkipInfo> to;
        #endregion
        /// <summary>
        /// 获取一个结构体副本
        /// </summary>
        /// <returns></returns>
        public StatusCellrom Copy()
        {
            return new StatusCellrom()
            {
                name = name,
                Pass = pass,
                To = to
            };
        }

    }

    /// <summary>
    /// 状态单元对应的结构体
    /// </summary>
    public struct StatusCellrom
    {
        public string name;
        private Queue<FunctionInfo> pass;
        private Queue<SkipInfo> to;
        public Queue<FunctionInfo> Pass { set { pass = value; } }
        public Queue<SkipInfo> To { set { to = value; } }

        public FunctionInfo GetFunction() 
        { 
            if(pass.Count > 0) { return pass.Dequeue(); }
            return null;
        }
        public SkipInfo GetSkip() 
        {
            if (to.Count > 0) { return to.Dequeue(); }
            return null;
        }
    }
}
