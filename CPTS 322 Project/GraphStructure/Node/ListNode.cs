using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_322_Project.GraphStructure.Node
{
    //referenced for help: https://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx
    public class NodeList : Collection<Node>
    {
        public NodeList() : base() { }

        public NodeList(int initialSize)
        {
            // Add the specified number of items
            for (int i = 0; i < initialSize; i++)
                base.Items.Add(default(Node));
        }

        public Node FindByValue(string state)
        {
            // search the list for the value
            foreach (Node node in Items)
                if (node.State.Equals(state))
                    return node;

            // if we reached here, we didn't find a matching node
            return null;
        }

    }

    
}
