using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    internal class TestStart
    {
        public static void Main(string[] args)
        {
            ScriptParser parser = new ScriptParser();
            StateMachine stateMachine = new StateMachine();
            parser.LoadFormFile("D:\\UHp\\test.ykr", stateMachine);
            stateMachine.LogAll();

            StateController controller = new StateController(stateMachine,new Effector());
            controller.Start();

        }
    }

    class Effector : IEffector
    {
        public string Effect(string name, Dictionary<string, object> parameters)
        {
            switch(name)
            {
                case "场景标题":
                    Console.WriteLine("当前场景:" + parameters["标题"]);
                    Console.ReadKey();
                    return null;
                case "对话":
                    Console.WriteLine($"【{parameters["名称"]}】：{parameters["内容"]}");
                    Console.ReadKey();
                    return null;
                case "选择":
                    Console.WriteLine($"【选项1】：{parameters["选项1"]}");
                    Console.WriteLine($"【选项2】：{parameters["选项2"]}");
                    return Console.ReadLine();
                case "声音":
                    Console.WriteLine($"*播放音效：【{parameters["音效"]}】");
                    Console.ReadKey();
                    return null;
                case "结局":
                    Console.WriteLine($"【结局】：{parameters["标题"]}");
                    Console.ReadKey();
                    return null;
                default:
                    Console.WriteLine("此函数不存在："+name );
                    Console.ReadKey();
                    return null;
            }
        }
    }
}

