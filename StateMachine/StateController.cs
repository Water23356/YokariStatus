using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    /// <summary>
    /// 状态控制器
    /// </summary>
    public class StateController
    {
        /// <summary>
        /// 效果器
        /// </summary>
        public IEffector effector;
        /// <summary>
        /// 状态机
        /// </summary>
        public StateMachine stateMachine;

    }
}
