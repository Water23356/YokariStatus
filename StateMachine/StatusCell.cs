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
        /// <summary>
        /// 状态名称
        /// </summary>
        public string name;
        /// <summary>
        /// 状态步骤
        /// </summary>
        private List<StepInfo> steps;
        /// <summary>
        /// 当前步骤索引
        /// </summary>
        public int Index { get; set; }
        #endregion

        #region 构造函数
        public StatusCell() { steps = new List<StepInfo>(); }
        public StatusCell(string name_) { name = name_; steps = new List<StepInfo>(); }
        #endregion

        #region 响应函数
        /// <summary>
        /// 获取该状态指定步骤的信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public StepInfo? this[int index]
        {
            get 
            {
                if (index > -1 && index < steps.Count) { return steps[index]; }
                return null;
            }
        }
        /// <summary>
        /// 获取当前步骤的信息，并将索引向后移动一位
        /// </summary>
        /// <returns></returns>
        public StepInfo? Next()
        {
            if (Index > -1 && Index < steps.Count) { return steps[Index++]; }
            return null;
        }
        public StatusCell Copy(int index)
        {
            return new StatusCell()
            {
                name = name,
                steps = steps,
                Index = index
            };
        }
        #endregion
    }
}