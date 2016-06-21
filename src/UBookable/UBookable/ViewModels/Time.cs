
using System.ComponentModel;
using UBookable.TypeConvertors;
using Umbraco.Core.Models.PublishedContent;

namespace UBookable.ViewModels
{

    public class Time
    {
        public int Hours { get; set;}
        public int Mins { get; set; }
        public override string ToString()
        {
            return Hours.ToString("D2") + ":" + Mins.ToString("D2"); 
        }
    }

}