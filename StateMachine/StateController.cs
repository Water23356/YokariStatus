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
        #region 属性
        /// <summary>
        /// 效果器
        /// </summary>
        public IEffector effector;
        /// <summary>
        /// 状态机
        /// </summary>
        public StateMachine stateMachine;
        /// <summary>
        /// 消息委托
        /// </summary>
        public Action<string> printAction;
        /// <summary>
        /// 当前状态信息
        /// </summary>
        public StatusCell statusInfo;
        /// <summary>
        /// 控制器状态
        /// </summary>
        public bool active = false;
        #endregion

        #region 内部函数
        private void Log(string txt)
        {
            if (printAction != null) { printAction(txt); }
            else { Console.Write(txt); }
        }
        #endregion
        /// <summary>
        /// 从头开始
        /// </summary>
        public void Start()
        {
            if (effector == null || stateMachine == null) { Log("缺少状态库 或 效果器\n"); return; }
            statusInfo = stateMachine.Find("Start");
            if (statusInfo == null) { Log("状态库缺少启动状态Start\n"); return; }
            active = true;
            Go();
        }

        private void Go()
        {
            while(active)//不用递归，防止深层递归导致爆栈
            {
                active = Run();
            }
        }

        /// <summary>
        /// 执行状态机
        /// </summary>
        /// <returns>是否正在执行，状态结束则返回false</returns>
        private bool Run()
        {
            string nextName = "End";//下一个状态名称
            int nextIndex = 0;//下一个状态的步骤索引
            if (statusInfo != null)
            {
                StepInfo next = statusInfo.Next();
                if (next != null)
                {
                    StepInfo step = (StepInfo)next;
                    #region 函数部分
                    FunctionInfo function = step.function;//函数信息
                    string backvalue = "";//函数返回值
                    if (function.name != "null")//为非空函数
                    {
                        backvalue = effector.Effect(function.name, function.parameters);
                    }
                    #endregion
                    #region 出口部分
                    if (statusInfo.name == "End")//End状态
                    {
                        return false;//End状态直接结束状态机
                    }
                    else//非End状态则处理出口状态信息
                    {
                        int i = 0;
                        while (i < step.skips.Count)
                        {
                            SkipInfo skip = step.skips[i];//获取出口
                            if (skip.condition == backvalue)//满足出口条件
                            {
                                //逐步实现伴随函数
                                if(skip.functions != null)
                                {
                                    int k = 0;
                                    while (k < skip.functions.Count)
                                    {
                                        FunctionInfo functionInfo = skip.functions[k];
                                        effector.Effect(functionInfo.name, functionInfo.parameters);
                                        ++k;
                                    }
                                }
                                //修改出口信息
                                nextName = skip.name;
                                nextIndex = skip.index;
                                break;//跳出对出口的循环
                            }
                            ++i;
                        }
                    }
                }
            }
            #endregion
            statusInfo= stateMachine.Find(nextName,nextIndex);
            return true;
        }
    }
}
