using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    /// <summary>
    /// 效果器
    /// </summary>
    public interface IEffector
    {
        /// <summary>
        /// 实现效果，调用具体的函数
        /// </summary>
        /// <param name="name">函数标记符</param>
        /// <param name="parameters">函数参数</param>
        public void Effect(string name,Dictionary<string,object> parameters);
    }
}
