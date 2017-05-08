using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CPTS_322_Project
{
    public class Menu
    {
        
        public Menu() //JP I also made this public.
        {
            m_graphMan = new List<GraphManager>();
            current = "";
        }

        public void DisplayMenu() //JP. Changed the access to public so it could be called from "Program.cs" idk if that was right
        {
            int test = 0;
            
            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("CPTS 322 Project");
                if (m_graphMan.Count != 0) { System.Console.WriteLine("Current Automata: {0}", current); }//means it contains a graph
                System.Console.WriteLine("1. Open New Automata /*Deletes current*/");//Open from file. Deletes current.
                System.Console.WriteLine("2. Input New automata /*Deletes current*/"); // Create new Automata. Deletes current
                if (m_graphMan.Count != 0)
                {
                    System.Console.WriteLine("3. Save Automata");//Save Automata. *if you updated/Created*
                    System.Console.WriteLine("4. Test String");  //Test on current automata
                    System.Console.WriteLine("5. Print Automata");
                    System.Console.WriteLine("6. Test to see if it is solvable");
                    //System.Console.WriteLine("4. Edit Current Automata");//Edit . Bring up menu to state what we have , and how you can edit
                }
                System.Console.WriteLine("9. Exit");

                test = System.Console.ReadLine().ToCharArray()[0];//was reading ascii version of integer.
                test = test - 48;
                switch (test)
                {
                    case 1:
                        // OpenAutomata();
                        InputAutomata();
                        break;
                    case 2:
                        ManuallyInputAutomata();
                        break;
                    case 3:
                        if (m_graphMan.Count == 0) { break;  }
                        SaveAutomata();
                        break;
                    case 4:
                        if (m_graphMan.Count == 0) { break; }
                        InputString();
                        break;
                    case 5:
                        if (m_graphMan.Count == 0) { break; }
                        System.Console.Clear();
                        m_graphMan[0].CAutomata.printAutomata();
                        System.Console.ReadKey();
                        break;
                    case 6: //test to see if it is solvable
                        bool solve = m_graphMan[0].CGraph.isSolvable();//I don't understand the whole m_graphman thing
                        if (solve) { System.Console.WriteLine("This FA is solvable"); }
                        else { System.Console.WriteLine("This FA is not solvable"); }
                        System.Console.ReadKey();
                        break;
                    case 9:
                        return;
                }//Do I need another 
                test = 0;
            }
        }
        void EditAutomata()
        {
            GraphManager m_Cur = m_graphMan[0];//Milestone 4, one case
            //check how this works
            System.Console.Clear();
            int input = 0;
            string ct = "";
            //1. Clear Console
            //2. Display all Current graph connections


            //3. Ask user add/remove connections
            //m_Cur.CAutomata.printAutomata(); 
            while (true)
            {
                System.Console.Clear();

                System.Console.WriteLine("**Edit Options**");
                System.Console.WriteLine("1. Add State");
                System.Console.WriteLine("2. Add Transitions");
                System.Console.WriteLine("3. Remove State");
                System.Console.WriteLine("4. Remove Transition");
                System.Console.WriteLine("5. Add to Alphabet");
                System.Console.WriteLine("6. Remove from Alphabet");
                System.Console.WriteLine("7. Print Automata (Updated)");
                System.Console.WriteLine("8. Return to Main Menu");
                input = System.Console.ReadLine().ToCharArray()[0]; ;
                input = input - 48;
                switch (input)
                {
                    case 1://add state no connections
                        System.Console.Write("Enter new state name: ");
                        ct = System.Console.ReadLine();
                        m_Cur.addState(ct);
                        break;
                    case 2://add transition between to existing states
                        break;
                    case 3: // remove a state and all transitions to it
                        break;
                    case 4: // remove justa  single transition
                        break;
                    case 5: // add a new alphabet character
                        break;
                    case 6: // remove an alphabet character and all of its dependent transitions
                        break;
                    case 7: //
                        System.Console.Clear();
                        m_Cur.CAutomata.printAutomata();
                        System.Console.ReadKey();
                        break;
                    case 8://exit
                        current = "*UNUPDATED SAVE AGAIN*" + current;
                        return;
                }
            }
            
        }

        void InputString()
        {
            //return true;//or false if not accepted
            GraphManager m_Cur = m_graphMan[0];// Milestone 4, one case
            string tested = "";
            System.Console.Clear();
            System.Console.WriteLine("****Test String****");
            System.Console.Write("Enter a string to test : ");
            tested = System.Console.ReadLine();
            //Tested string on.
            if (m_Cur.IsStringAccepted(tested))
            {
                //The string is accepted
                System.Console.WriteLine("The string '{0}' is accepted!", tested);
                System.Console.WriteLine("Press any key to continue");
                System.Console.ReadKey();
                return;
            }
            System.Console.WriteLine("This key is not accepted");
            System.Console.ReadKey();
            return;
        }

        void InputAutomata(/*Automata insert*/)//get automata
        {
            //string path = "";
            if(m_graphMan.Count > 0)
            {
                System.Console.WriteLine("Already a graph open\nPress any key to continue");
                System.Console.ReadKey();
                return;
            }
            GraphManager n_graph = new GraphManager();
            OpenFileDialog n = new OpenFileDialog();
            n.FileName = "*.xml";
            n.Filter = "Automata (*.xml) | *.xml;";
            n.ShowDialog();
            n_graph.CAutomata.readAutomataFromXML(n.FileName);
            n_graph.TranslateAutomataToGraph();
            m_graphMan.Add(n_graph);
            current = n.FileName;
            //int triesremaining = 3;
            if (n_graph.CAutomata.Password != "")// && triesremaining > 0)
            {
                System.Console.Clear();
                System.Console.WriteLine("Enter password below: ");
                if (n_graph.CAutomata.Password != System.Console.ReadLine())
                {
                    //triesremaining--;
                    System.Console.WriteLine("Incorrect password. "); //+ triesremaining + " tries remaining");
                    System.Console.ReadKey();
                    m_graphMan.Clear();
                    return;
                }
            }

        }
        
        void DeleteAutomata()//not sure if we need to implement
        {
            //Not sure yet if there is a 'current' Automata or we specify
            GraphManager m_Cur = m_graphMan[0];//
        }

        void ManuallyInputAutomata()
        {
            Console.Clear();
            GraphManager n_graph = new GraphManager();
            n_graph.CAutomata.ManuallyInputAutomata();

            n_graph.TranslateAutomataToGraph();
            m_graphMan.Clear();
            m_graphMan.Add(n_graph);
            current = "Unsaved / Untitled";
        }

        void SaveAutomata()
        {
            //Save
            GraphManager m_Cur = m_graphMan[0];//We are only using m_graphman[0] for milestone 4
            SaveFileDialog sv = new SaveFileDialog();
            sv.FileName = "Untitled.xml";
            sv.Filter = "Automata (*.xml) | *.xml;";
            sv.ShowDialog();
            m_Cur.CAutomata.writeAutomataToXML(sv.FileName);
            current = sv.FileName;
        }

        void Exit()
        {
            //Save everything and exit.
            SaveAutomata();            
        }


        private List<GraphManager> m_graphMan;
        private string current;

    }
}


