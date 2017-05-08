using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPTS_322_Project.GraphStructure.Node;

namespace CPTS_322_Project.GraphStructure
{

    //referenced for help: https://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx

    public class Graph
    {
        private Dictionary<string,GraphNode> nodes;
        private GraphNode initialState;
        private Dictionary<string, GraphNode> acceptingStates;
        private List<char> alphabet;

        public Graph()
        {
            acceptingStates = new Dictionary<string, GraphNode>();
            nodes = new Dictionary<string, GraphNode>();
            initialState = null;
            alphabet = new List<char>();
        }
        public GraphNode InitialState
        {
            get { return initialState;  }
            set { initialState = value; }
        }
        public Dictionary<string, GraphNode> AcceptingStates { get { return acceptingStates; } }
        /// <summary>
        /// Adds a node to our graph, taking in a transition
        /// </summary>
        /// <param name="transition"></param>
        // TODO add a return type, for error or success!!!!!!!!!!!!!!!!!!!
        
        public void addNode(string name)
        {
            nodes[name] = new GraphNode(name);
        }

        public void addFinal(string name)
        {
            if(nodes.ContainsKey(name))
            {
                acceptingStates[name] = nodes[name];
            }
            else
            {
                Console.WriteLine("A final state entered has not been entered into the allowed states");
                Console.ReadKey();
            }
        }

        public void addInitial(string name)
        {
            if (nodes.ContainsKey(name))
            {
                initialState = nodes[name];
            }
            else
            {
                Console.WriteLine("An initial state entered has not been entered into the allowed states");
                //Console.ReadKey();
            }
        }

        public void addAlphabet(List<char> inputAlphabet)
        {
            alphabet = inputAlphabet; //JP this may be an invalid assignment
        }

        public void addTransition(Transition trans)
        {
            /*
             * JP error checking that doesn't work
             * if (!nodes.ContainsKey(trans.Start)) //the start is not found
            {
                Console.WriteLine("The start value of this transition is not present in the graph");
                //Console.ReadKey();
            }
            else if (!nodes.ContainsKey(trans.End))
            {
                Console.WriteLine("The end value of this transition is not present in the graph");
                //Console.ReadKey();
            }
            else if (!inAlphabet(trans.Input)) //inefficent function
            {
                Console.WriteLine("The transition element of this transition is not present in the graph");
                //Console.ReadKey();
            }
            else
            {
                nodes[trans.Start].addTransition(trans.Input, nodes[trans.End]);
            }*/
            nodes[trans.Start].addTransition(trans.Input, nodes[trans.End]);
        }

        public bool inAlphabet(char ch)
        {
            foreach (char element in alphabet)
            {
                if(ch == element)
                {
                    return true;
                }
            }
            return false;
        }


        /* JP public void addNode(Transition transition)
        {
            //see if start node is already in the graph.
            foreach (GraphNode node in nodes)
            {
                if (node.State == transition.Start)
                {
                    //see if it already has the transition
                    foreach (var trans in node.Transitions)
                    {
                        if (trans == transition)
                        {
                            //transition are already in the graph, break out of loop
                            goto SkipTransition;
                        //}
                    }
                    //otherwise add transition
                    node.Transitions.Add(transition);
                SkipTransition:
                    foreach (var neighbor in node.Neighbors)
                    {
                        if (neighbor.State == transition.End)
                        {
                            //already have this state as a neighbor, break out of loop
                            return;
                        }
                    }
                    //otherwise add neighbor
                    node.Neighbors.Add(new GraphNode(transition.End));
                    //all cases taken care of, return
                    return;
                }
            }
            //if the node is not already in the list, create a new one and add it to Nodes list.
            var newNode = new GraphNode(transition.Start);
            newNode.Neighbors.Add(new GraphNode(transition.End));
            newNode.Transitions.Add(transition);

            nodes.Add(newNode);
        }*/

        //search for a state given the state name

        public bool isSolvable()
        {
            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            //The dictionary will be used to store values that have been visited
            Stack<GraphNode> stack = new Stack<GraphNode>();
            GraphNode temp = new GraphNode();

            stack.Push(initialState);
            while(stack.Count != 0) //idk if that's the right syntax
            {
                temp = stack.Pop();
                //GraphNode temp = new GraphNode();
                if (acceptingStates.ContainsKey(temp.name))//temp is an accepting state
                {
                    return true;
                }

                if (!visited.ContainsKey(temp.name))//if it has not already been visited
                {
                    visited[temp.name] = true;
                    List<GraphNode> neighbors = temp.get_neighbors();
                    foreach(GraphNode neighbor in neighbors)
                    {
                        if(!visited.ContainsKey(neighbor.name))//This neighbor has not been visited
                        {
                            stack.Push(neighbor);
                        }
                    }
                }
            }


            return false;//all nodes have been visited, none are accepting
        }

        public string shortest_word()
        {
            //Will implement Dijkstra's Algorithm
            return "000";
        }


       /* private GraphNode search(string state)
        {
            foreach (var node in nodes)
            {
                if (node.State == state)
                {
                    return node;
                 
                }
            }
            return null;
        }*/


        public bool walk(string input)
        {
            GraphNode currNode = null;
            currNode = initialState;
            if(currNode != null)
            {
                foreach (char c in input)
                {
                    currNode = currNode.makeTransition(c);
                    if(currNode == null)
                    {
                        return false;
                    }
                }
                return (acceptingStates.TryGetValue(currNode.name, out currNode)); //JP Single most badass line of code I've ever written
            }
            else
            {
                return true; //JP or false, IDK
            }
        }
        /*public bool walk(string inputString)
        {
            GraphNode currNode = null, nextNode = null;

            currNode = search(initialState);
            if (currNode != null)
            {
                //for every input in the inputstring
                foreach (char c in inputString)
                {
                    //look for a transition
                    foreach (var trans in currNode.Transitions)
                    {
                        //if there is a transition with that input, take it
                        if (trans.Input == c.ToString())
                        {
                            nextNode = search(trans.End);
                            break;//break out of loop hopefully
                        }
                    }
                    if(nextNode != null)//found the next state
                    {
                        currNode = nextNode;
                    }
                    else//no transition on given input
                    {
                        return false;
                    }
                }
            }
            else//no initial state
            {
                return false;
            }

            //check if we are in an accepting state or not
            foreach(string state in acceptingStates)
            {
                if(currNode.State == state)
                {
                    return true;
                }
            }
            return false;
        }*/


    }
}
