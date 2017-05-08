using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CPTS_322_Project
{

    [XmlType("Transition")]
    public class Transition
    {
        public Transition()
        {

        }
        public Transition(string start, string end, char input) //JP changed to char
        {
            Start = start;
            End = end;
            Input = input;
        }
        [XmlElement("Start")]
        public string Start { get; set; }
        [XmlElement("Input")]
        public char Input { get; set; }//JP changed to char
        [XmlElement("End")]
        public string End { get; set; }

        //now == works by compairing strings rather than compairing memory
        public override bool Equals(object obj)
        {
            Transition lhs = (Transition) obj;
            if (Start == lhs.Start && Input == lhs.Input && End == lhs.End)
                return true;
            return false;
        }
    }
}


