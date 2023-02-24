using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    /// <summary>
    /// 跳转信息
    /// </summary>
    public class SkipInfo
    {
        #region 属性
        /// <summary>
        /// 目标单元
        /// </summary>
        public string name;
        /// <summary>
        /// 条件
        /// </summary>
        public int condition;
        /// <summary>
        /// 过渡函数
        /// </summary>
        public Queue<FunctionInfo> functions;
        #endregion

        public SkipInforom Copy()
        {
            return new SkipInforom()
            {
                name = name,
                condition = condition,
                Functions = functions
            };
        }
    }


    public struct SkipInforom
    {
        public string name;
        public int condition;
        private Queue<FunctionInfo> functions;

        public Queue<FunctionInfo> Functions
        {
            set { functions = value; }
        }
        public FunctionInfo GetFunction()
        {
            return functions.Peek();
        }
    }

}
