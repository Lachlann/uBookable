
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

    [Umbraco.Core.PropertyEditors.PropertyValueType(typeof(Time))]
    public class MyTimePropertyValueConverter : Umbraco.Core.PropertyEditors.PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias == "Timepicker";
        }

        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
                return null;

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Time>(source.ToString());
        }
    }
}