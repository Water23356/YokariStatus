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
        private Dictionary<string, StatusCell> cells = new Dictionary<string, StatusCell>();

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
        /// <summary>
        /// 获取指定状态信息
        /// </summary>
        /// <param name="name">状态名称</param>
        /// <returns>若不存在则返回null</returns>
        public StatusCell Find(string name,int index=0)
        {
            if (isExist(name)) { return cells[name].Copy(index); }
            if (isExist("End")) { return cells["End"].Copy(index); }
            return new StatusCell("End");
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
