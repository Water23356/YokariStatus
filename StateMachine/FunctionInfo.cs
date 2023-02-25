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

        public void PrintInfo(string tabto)
        {
            Console.WriteLine(tabto+$"【函数】:{name}"+"{");
            foreach(string k in parameters.Keys)
            {
                Console.WriteLine(tabto+$"\t{parameters.Keys} : {parameters.Values },");
            }
            Console.WriteLine(tabto+"}");
        }
    }
}
