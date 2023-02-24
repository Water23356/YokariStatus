using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    /// <summary>
    /// 状态机，状态系统
    /// </summary>
    public class StateMachine
    {
        #region 属性
        /// <summary>
        /// 系统中的单元
        /// </summary>
        private Dictionary<string, StatusCell> cells;

        #endregion

        #region 响应函数
        /// <summary>
        /// 判断指定状态是否存在
        /// </summary>
        /// <param name="key">状态名称</param>
        /// <returns></returns>
        public bool isExist(string key)
        {
            foreach(string k in cells.Keys)
            {
                if (k == key) { return true; }
            }
            return false;
        }

        #endregion

        
    }

    /// <summary>
    /// 脚本解释器
    /// </summary>
    public class ScriptPaser
    {

    }
}
