using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_322_Project.GraphStructure.Node
{
    //referenced for help: https://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx
    public class Node
    {
        // Private member-variables
        private string state;

        public Node() { }
        public Node(string newState) { state = newState; }
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
    }

}
