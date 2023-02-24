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
            foreach (string k in cells.Keys)
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
        public StatusCell Find(string name, int index = 0)
        {
            if (isExist(name)) { return cells[name].Copy(index); }
            if (isExist("End")) { return cells["End"].Copy(index); }
            return new StatusCell("End");
        }
        /// <summary>
        /// 向库中添加新的状态，同名状态则覆盖原先状态
        /// </summary>
        /// <param name="cell">新添状态单元对象</param>
        public void Add(StatusCell cell)
        {
            if (isExist(cell.name)) { cells[cell.name] = cell; }
            else { cells.Add(cell.name, cell); }
        }
        /// <summary>
        /// 清空库存
        /// </summary>
        public void Clear()
        {
            cells.Clear();
        }
        #endregion


    }

    /// <summary>
    /// 脚本解释器
    /// </summary>
    public class ScriptParser
    {
        private enum formCell { waitCname,Cname,Cnameok,step}
        private enum formStep { waitSname,Sname,Snameok,param,paramok,to,took,go}
        private enum formParam { waitKname,Kname,Knameok,waitVname,Vname,Vnameok}
        private enum formTo { waitTname,Tname,Tnameok,waitIname,Iname,Inameok,function,functionok}
        private enum formFunc { waitFname,Fname,Fnameok,param,paramok}


        #region 状态器
        private formCell cell = formCell.waitCname;
        private formStep step = formStep.waitSname;
        private formParam param = formParam.waitKname;
        private formTo to = formTo.waitTname;
        private formFunc func = formFunc.waitFname;
        #endregion

        #region 缓存器
        private string cache;//命名缓存
        private StatusCell cell_c;
        private StepInfo step_c;
        private SkipInfo skip_c;
        private FunctionInfo function_c;
        #endregion


        private bool inchange;//转义编辑
        private bool inquote;//引用编辑
        /**
         * 引号的作用是，表示一个整体
         * 在引号编辑内是，转义才起作用
         */
        /// <summary>
        /// 从指定脚本文件中解析指令到指定状态机对象
        /// </summary>
        /// <param name="fileName">脚本文件路径（包含后缀.ykr）</param>
        /// <param name="sm">实现指令的状态机对象</param>
        /// <return>若无法成功解析，或者解析出错，则终止返回false，正常解析完毕返回true</return>
        public bool LoadFormFile(string fileName, StateMachine sm)
        {
            if (!File.Exists(fileName)) { return false; }
            StreamReader reader = new StreamReader(fileName);

            char[] buffer = new char[1024];//缓存
            int length = reader.Read(buffer);//读取长度
            while (length > 0)
            {
                if(!Parse(buffer,length))//解析异常直接终止
                {
                    Console.WriteLine("解析异常");
                    sm.Clear();
                    return false;
                }
            }
            //所有文本解析完毕后
            if(cell!= formCell.waitCname)//存在编辑器状态则说明解析异常
            {
                Console.WriteLine("终止解析异常");
                sm.Clear();
                return false;
            }
            return true;
        }

        public bool Parse(char[] input,int length)
        {
            int i = 0;
            while(i<length)
            {
                char c = input[i];
                switch(cell)
                {
                    case formCell.waitCname://等待单元命名
                        switch(c)
                        {
                            case ' '://空白字符跳过
                            case '\t':
                            case '\n':
                                break;
                            case '"'://转为引用模式
                                inquote = true;
                                cell = formCell.Cname;
                                break;
                            default:
                                cache += c;
                                cell = formCell.Cname;
                                break;
                        }
                        break;
                    case formCell.Cname://单元命名
                        if(inquote)//引用模式
                        {
                            if(inchange)//转义模式
                            {
                                switch(c)
                                {
                                    case '"':
                                    case '\\':
                                        cache += c;
                                        break;
                                    case 'n':
                                        cache += '\n';
                                        break;
                                    case 't':
                                        cache+= '\t';
                                        break;
                                    default:
                                        break;
                                }
                                inchange = false;
                            }
                            else//非转义模式
                            {
                                switch (c)
                                {
                                    case '"'://结束命名
                                        cell_c = new StatusCell(cache);//创建单元对象
                                        cache = "";//清空缓存
                                        cell = formCell.Cnameok;
                                        break;
                                    case '\\':
                                        inchange = true;
                                        break;
                                    default:
                                        cache += c;
                                        break;
                                }
                            }
                        }
                        else//非引用模式
                        {
                            switch(c)
                            {
                                case ' ':
                                case '\t':
                                case '\n':
                                    break;
                                case '{'://命名结束
                                    cell_c = new StatusCell(cache);
                                    cache = "";
                                    cell = formCell.step;
                                    step = formStep.waitSname;
                                    break;
                                default:
                                    cache += c;
                                    break;
                            }
                        }
                        break;
                    case formCell.Cnameok://引用命名完毕
                        switch(c)
                        {
                            case '{':
                                cell = formCell.step;
                                step = formStep.waitSname;
                                break;
                            default:
                                Console.WriteLine("单元解析异常");
                                return false;
                        }
                        break;
                    case formCell.step://步骤编辑
                        if(!Parse_Step(c))//解析出错则抛出错误信息并终止
                        {
                            Console.WriteLine("步骤解析异常");
                            return false;
                        }
                        break;
                }
                ++i;
            }
            return true;
        }

        private bool Parse_Step(char c)
        {
            switch(step)
            {
                case formStep.waitSname://等待步骤命名
                    switch (c)
                    {
                        case ' '://空白字符跳过
                        case '\t':
                        case '\n':
                            break;
                        case '"'://转为引用模式
                            inquote = true;
                            step = formStep.Sname;
                            break;
                        default:
                            cache += c;
                            step = formStep.Sname;
                            break;
                    }
                    break;
                case formStep.Sname://步骤命名
                    if (inquote)//引用模式
                    {
                        if (inchange)//转义模式
                        {
                            switch (c)
                            {
                                case '"':
                                case '\\':
                                    cache += c;
                                    break;
                                case 'n':
                                    cache += '\n';
                                    break;
                                case 't':
                                    cache += '\t';
                                    break;
                                default:
                                    break;
                            }
                            inchange = false;
                        }
                        else//非转义模式
                        {
                            switch (c)
                            {
                                case '"'://结束命名
                                    step_c = new StepInfo();
                                    function_c = new FunctionInfo() { name = cache};
                                    cache = "";//清空缓存
                                    step = formStep.Snameok;
                                    break;
                                case '\\':
                                    inchange = true;
                                    break;
                                default:
                                    cache += c;
                                    break;
                            }
                        }
                    }
                    else//非引用模式
                    {
                        switch (c)
                        {
                            case ' ':
                            case '\t':
                            case '\n':
                                break;
                            case '('://命名结束
                                step_c = new StepInfo();
                                function_c = new FunctionInfo() { name = cache };
                                cache = "";//清空缓存
                                step = formStep.param;
                                break;
                            default:
                                cache += c;
                                break;
                        }
                    }
                    break;
                case formStep.Snameok://引用命名完毕
                    switch (c)
                    {
                        case '(':
                            step = formStep.param;
                            break;
                        default:
                            Console.WriteLine("步骤解析异常");
                            return false;
                    }
                    break;
                case formStep.param://参数
                    if (!Parse_Param(c))//解析出错则抛出错误信息并终止
                    {
                        Console.WriteLine("参数解析异常");
                        return false;
                    }
                    break;
                case formStep.paramok://参数处理完毕
                    switch(c)
                    {
                        case '{':
                            step = formStep.to;
                            break;
                        case ';':
                            step = formStep.waitSname;
                            break;
                        default:
                            Console.WriteLine("步骤解析异常");
                            break;
                    }
                    break;
                case formStep.to://出口
                    if(!Parse_To(c))//解析出错则抛出错误信息并终止
                    {
                        Console.WriteLine("出口解析异常");
                        return false;
                    }
                    break;
                case formStep.took://出口处理完毕(整个步骤解析完毕)
                    switch(c)
                    {
                        case '}'://步骤的完整结构
                            cell_c.AddStep(step_c);
                            step = formStep.go;
                            break;
                        default:
                            Console.WriteLine("步骤解析异常");
                            break;
                    }
                    break;
                case formStep.go:
                    switch(c)
                    {
                        case '}'://单元编辑结束
                            cell = formCell.waitCname;
                            break;
                        default://单元编辑未结束，作为等待步骤名处理
                            switch (c)
                            {
                                case ' '://空白字符跳过
                                case '\t':
                                case '\n':
                                    break;
                                case '"'://转为引用模式
                                    inquote = true;
                                    step = formStep.Sname;
                                    break;
                                default:
                                    cache += c;
                                    step = formStep.Sname;
                                    break;
                            }
                            break;
                    }
                    break;
            }
            return true;
        }

        private bool Parse_Param(char c)
        {
            return true;
        }

        private bool Parse_To(char c)
        {
            return true;
        }

        private bool Parse_Func(char c)
        {
            return true;
        }
    }
}
