using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    /// <summary>
    /// 函数信息
    /// </summary>
    public class FunctionInfo
    {
        #region 属性
        /// <summary>
        /// 函数名称
        /// </summary>
        public string name;
        /// <summary>
        /// 参数信息
        /// </summary>
        public Dictionary<string, object> parameters;
        #endregion
        /// <summary>
        /// 获取一个结构体副本
        /// </summary>
        /// <returns></returns>
        public FunctionInforom Copy()
        {
            return new FunctionInforom()
            {
                name = name,
                Parameters = parameters
            };
        }
    }

    public struct FunctionInforom
    {
        public string name;
        private Dictionary<string, object> parameters;
        public Dictionary<string, object> Parameters { set { parameters = value; } }
        public object GetValue(string key)
        {
            return parameters[key];
        }
    }
}
