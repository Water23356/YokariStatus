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
        }
    }


}

