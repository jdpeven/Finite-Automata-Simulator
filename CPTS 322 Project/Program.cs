using CPTS_322_Project.GraphStructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Jackson waz here
//Peter was also here!
//Tony was Here! `0/3/10`6
//Commit test 10/6/2016 2:41
//Test after changing my email
// Brandon was here!!

namespace CPTS_322_Project
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Menu main_menu = new Menu();
            Directory.SetCurrentDirectory(@"..\..\");
            main_menu.DisplayMenu();


   
            //Automata a = new Automata();
            //a.readAutomataFromXML();
            //a.printAutomata();
            //a.writeAutomataToXML();
            //Console.WriteLine("Master branch");



            //Example of how to build the graph and to have an input string walk through the graph.
            //Jackson this will be helpful in GraphManager, I think.

            //create a new graph
            /*
            Graph g1 = new Graph();
            g1.InitialState = a.InitialState;//set initial state
            foreach(var fs in a.FinalStates)//set all final states
            {
                g1.AcceptingStates.Add(fs);
            }
            foreach(var trans in a.Transitions)//create all nodes in the graph based on transitions
            {
                g1.addNode(trans);
            }
            bool accepted = g1.walk("01");//pass in the string to walk. If accepted == true, there is a walk, otherwise accpted is false
            */
        }
     
        
       
    }
}
