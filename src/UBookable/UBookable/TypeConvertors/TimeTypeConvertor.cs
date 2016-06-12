using Our.Umbraco.Ditto;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using UBookable.ViewModels;
using Umbraco.Core;

namespace UBookable.TypeConvertors
{
    public class TimeTypeConvertor : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            JavaScriptSerializer oJS = new JavaScriptSerializer();
            string time = "\"time\" : [" + value.ToString() + "]}";
            return oJS.Deserialize<Time>(time);
        }
    }
}