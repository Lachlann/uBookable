
using System.ComponentModel;
using UBookable.TypeConvertors;

namespace UBookable.ViewModels
{

    public class Time
    {
        public string Hours { get; set;}
        public string Mins { get; set; }
        public override string ToString()
        {
            return Hours + ":" + Mins;
        }
    }
}