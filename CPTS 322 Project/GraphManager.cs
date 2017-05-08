using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPTS_322_Project.GraphStructure;

namespace CPTS_322_Project
{
    public class GraphManager
    {
        public GraphManager()
        {
            m_automata = new Automata();
            m_graph = new Graph();
        }

        public void TranslateAutomataToGraph()//function builds graph
        {
            //JP idea
            foreach (string name in m_automata.States)
            {
                m_graph.addNode(name);
            }
            foreach (string name2 in m_automata.FinalStates)
            {
                m_graph.addFinal(name2);
            }
            m_graph.addInitial(m_automata.InitialState);
            foreach (Transition trans in m_automata.Transitions)
            {
                m_graph.addTransition(trans);
            }
            m_graph.addAlphabet(m_automata.Alphabet);

            /* JP
            m_graph.InitialState = m_automata.InitialState;
            foreach (var fState in m_automata.FinalStates)
            {
                m_graph.AcceptingStates.Add(fState);
            }
            foreach (Transition trans in m_automata.Transitions)
            {
                m_graph.addNode(trans);
            }*/
        }
        /*JP public void nTranslate()
        {
            Graph t = new Graph();//In the case of edditing we are changing the graph everytime a change is made. Not adding to it. 
            t.InitialState = m_automata.InitialState;//Probablt a better way to do this. CONSULT
            foreach (var fState in m_automata.FinalStates)
            {
                t.AcceptingStates.Add(fState);
            }
            foreach (Transition trans in m_automata.Transitions)
            {
                t.addNode(trans);
            }
            m_graph = t;
        }*/

        public void CreateAutomataFromXML(string path)
        {
            m_automata.readAutomataFromXML(path);
        }

        public void RemoveFromAutomataAndGraph()
        {

        }
        public void CreateUnion()
        {
            //This function will create the union between two DFAs
        }
        public bool IsStringAccepted(string test)
        {
            return m_graph.walk(test);
            //return true;
        }
        public void AddToAutomataAndGraph()
        {

        }
        public void CheckInputString()
        {

        }
        private bool DestroyGraph()
        {
            //remove
            return true;
        }

        //Below take the spot of edit graph. This will be more useful for GUI transitions. 
        public void removeState()
        {
            //ermove state and remove all connecting transitions
        }
        public void addState(string stateName)
        {
            //add state, will have no transitions to it.
            //cgraph
            //automata
            CAutomata.addState(stateName);
            CGraph.addNode(stateName); //JP added this
            //JP nTranslate();

        }

        public void removeTransistion(string firstState, string alphabetChar, string lastState)
        {
            //remove the transition between to states that are given. 
        }
        public void addTransition(string firstState, string alphabetChar, string lastState)
        {
            //add a transition between 2 states if it doesnt already exist
        }

        public void addAlphabet(string n_alpha)
        {
            //add a new string to alphabet
        }
        public void rmAlphabet(string n_alpha)
        {
            //remove alphabet string specified, And all of its connecting strings.
        }

        

        public Automata CAutomata { get { return m_automata;  } set { m_automata = value; } }
        public Graph CGraph { get { return m_graph; } set { m_graph = value; } }

        private Automata m_automata;
        private Graph m_graph;
    }
}
