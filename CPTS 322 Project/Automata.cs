using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CPTS_322_Project
{
    [XmlType("Automata")]
    public class Automata
    {
        public Automata()
        {
            Alphabet = new List<char>();
            States = new List<string>();
            Transitions = new List<Transition>();
            FinalStates = new List<string>();
        }

        //methods
        public void readAutomataFromXML(string path)
        {
            var doc = new XmlDocument();

            //string path = Path.Combine(Environment.CurrentDirectory, @"testXML\", "color.xml");

            //string path = Directory.GetCurrentDirectory() + @"\Automata.xml";

            doc.Load(path);


            //TODO check these to make sure i need them
            string data = "<Automata>";
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                data = data + node.OuterXml;
            }
            //TODO Check to see if I need
            data = data + @"</Automata>";

            var result = new Automata();

            var serializer = new XmlSerializer(typeof(Automata));
            using (var stream = new StringReader(data))
            using (var reader = XmlReader.Create(stream))
            {
                result = (Automata)serializer.Deserialize(reader);
            }
            //this is bad. find another way
            this.Alphabet = result.Alphabet;
            this.States = result.States;
            this.FinalStates = result.FinalStates;
            this.InitialState = result.InitialState;
            this.Transitions = result.Transitions;
            this.Password = result.Password;


        }

        private bool inAlphabet(char input) //JP string->char
        {
            foreach(char ch in Alphabet)
            {
                if(ch == input)
                {
                    return true;
                }
            }

            return false;
        }

        private bool inStates(string input)
        {
            foreach (string str in States)
            {
                if (str == input)
                {
                    return true;
                }
            }
            return false;
        }

        private bool inTransitions(Transition trans)
        {
            foreach (Transition trs in Transitions)
            {
                if(trans == trs)
                {
                    return true;
                }
            }
            return false;
        }

        private bool inFinal(string acc)
        {
            foreach (string accept in FinalStates)
            {
                if(acc == accept)
                {
                    return true;
                }
            }
            return false;
        }

        public void ManuallyInputAutomata()
        {
            Console.WriteLine("First please input the alphabet to be accepted by the FA, type 'X' to stop entering values");
            char test = ' '; //JP char->string
            test = Convert.ToChar(System.Console.ReadLine()); //JP tochar
            while(test != 'X') //JP changed from Exit
            {
                if(!inAlphabet(test)) //If the current state entered is 
                {
                    Alphabet.Add(test);
                }
                else
                {
                    Console.WriteLine("You already entered that one");
                }
                test = Convert.ToChar(System.Console.ReadLine());
            }
            //test = "";
            string stringtest = "";//JP added this
            Console.WriteLine("Next please input the names of the states, type 'EXIT' to stop entering values");
            stringtest = System.Console.ReadLine();
            while (stringtest != "EXIT")
            {
                if (!inStates(stringtest))
                {
                    States.Add(stringtest);
                }
                else
                {
                    Console.WriteLine("You already entered that one");
                }
                stringtest = System.Console.ReadLine();
            }
            string state1 = "";
            string state2 = "";
            char trans_element =  ' '; //JP string->char
            Console.WriteLine("Now for transitions. Enter\n state1, *enter*, state2, *enter*, transition element *enter*\n type 'X' to stop entering values");
            state1 = System.Console.ReadLine();
            state2 = System.Console.ReadLine();
            trans_element = Convert.ToChar(System.Console.ReadLine());
            while (state1 != "EXIT" && state2 != "EXIT" && trans_element != 'X')
            {

                if (inStates(state1) && inStates(state2) && inAlphabet(trans_element))
                {
                    Transition new_trans = new Transition(state1, state2, trans_element);
                    if (!inTransitions(new_trans))
                    {
                        Transitions.Add(new_trans);
                        state1 = "";
                        state2 = "";
                        trans_element = ' ';
                    }
                    else
                    {
                        Console.WriteLine("You've already entered this transition");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again");
                }
                Console.WriteLine("Enter Next transition or EXIT");
                state1 = System.Console.ReadLine();

                if(state1 == "EXIT")
                {
                    break;
                }

                state2 = System.Console.ReadLine();
                trans_element = Convert.ToChar(System.Console.ReadLine());
            }
            Console.WriteLine("Next please input the name of the intial state");
            stringtest = "";
            stringtest = System.Console.ReadLine();
            while (true) //JP This should only read in one time, the while loop is just to see if they entered an invalid input
                //I"m not sure if my "break" statement is correct syntactially 
            {
                if (!inStates(stringtest))
                {
                    Console.WriteLine("This state is not in your states");
                }
                else
                {
                    InitialState = stringtest;
                    break;
                }
                stringtest = System.Console.ReadLine();
            }

            Console.WriteLine("Next please input the names of the accepting states, type 'EXIT' to stop entering values");
            stringtest = System.Console.ReadLine();
            while (stringtest != "EXIT")
            {
                if (!inStates(stringtest))
                {
                    Console.WriteLine("This state is not in your states");
                }
                else
                {
                    if(!inFinal(stringtest))
                    {
                        FinalStates.Add(stringtest);
                    }
                    else
                    {
                        Console.WriteLine("You already entered that one");
                    }
                }
                stringtest = System.Console.ReadLine();
            }

            Console.WriteLine("Finally please input the name of the password");
            stringtest = "";
            stringtest = System.Console.ReadLine();
            Password = stringtest;

            return;
        }


        public void printAutomata() //will print the values of the automata
        {
            Console.WriteLine("Alphabet");
            foreach (char i in Alphabet)
            {
                Console.WriteLine(i);
            }
            //Console.ReadKey();
            Console.WriteLine("States");
            foreach (string i in States)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Final States");
            foreach (string i in FinalStates)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Initial State");
            Console.WriteLine(InitialState);
            Console.WriteLine("Transitions");
            foreach (Transition i in Transitions)
            {
                Console.WriteLine("Start: " + i.Start + ". Transition Val: " + i.Input + ". Final: " + i.End);
            }
            Console.WriteLine("Password");
            Console.WriteLine(Password);
            
        }

        public void writeAutomataToXML(string path)
        {

            //string path = Directory.GetCurrentDirectory() + @"\Automata.xml";
            var writer = new XmlSerializer(typeof(Automata));
            FileStream file = System.IO.File.Create(path);
            writer.Serialize(file, this);
            file.Close();

        }

        public void addState(string n)
        {
            States.Add(n);
        }



        //props
        [XmlArray("Alphabet")]
        [XmlArrayItem("Letter")]
        public List<char> Alphabet { get; set; } //JP string->char
        [XmlArray("States")]
        [XmlArrayItem("State")]
        public List<string> States { get; set; }
        [XmlElement("InitialState")]
        public string InitialState { get; set; }
        [XmlArray("FinalStates")]
        [XmlArrayItem("FinalState")]
        public List<string> FinalStates { get; set; }
        [XmlArray("Transitions")]
        [XmlArrayItem("Transition")]
        public List<Transition> Transitions { get; set; }
        [XmlElement("Password")]
        public string Password { get; set; }

        //should have private members for all of these properties 




    }
}
