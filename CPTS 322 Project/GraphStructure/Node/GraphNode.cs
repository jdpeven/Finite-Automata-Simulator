using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_322_Project.GraphStructure.Node
{
    //referenced for help: https://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx

    public class GraphNode : Node
    {
        //this will contain all transitions for the starting node we are at. I.E. if this node q1, we hold...
        //q1->a->q2; q1->b->q3; etc.

        private Dictionary<char, GraphNode> transitions;

        //JP private List<Transition> transitions;

        private List<GraphNode> neighbors;
        
        //JP private NodeList neighbors;

        public string name { get; set; } //JP added this for dictionary lookup
                                
        
        public GraphNode()
        {
            neighbors = new List<GraphNode>();
            //JP  neighbors = new NodeList();
            transitions = new Dictionary<char, GraphNode>();
        }//JP added this

        public GraphNode(string state)//JP changed this : base(state) 
        {
            neighbors = new List<GraphNode>();
            //neighbors = new NodeList();
            this.name = state;
            transitions = new Dictionary<char, GraphNode>();
        }

        public GraphNode(string state, List<GraphNode> neighbors) : base(state) { this.neighbors = neighbors; }


        //JP public GraphNode(string state, NodeList neighbors) : base(state) { this.neighbors = neighbors; }

        public List<GraphNode> get_neighbors()
        {
            return neighbors;
        }

        /*public NodeList Neighbors
        {
            get
            {
                if (neighbors == null)
                    neighbors = new NodeList();

                return neighbors;
            }
        }*/

        public void addTransition(char input, GraphNode next)
        {
            transitions[input] = next;
            neighbors.Add(next); //JP not sure how useful this is
        }


        public GraphNode makeTransition(char input)
        {
            GraphNode result;
            if(transitions.TryGetValue(input, out result))
            {
                return transitions[input];
            }
            return null;
        }

        /* JP public List<Transition> Transitions
        {
            get
            {
                if (transitions == null)
                    transitions = new List<Transition>();
                return transitions;
            }
        }*/
    }
}
