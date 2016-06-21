using Our.Umbraco.Ditto;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using UBookable.ViewModels;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;

namespace UBookable.TypeConvertors
{
    public class TimeTypeConvertor : Umbraco.Core.PropertyEditors.PropertyValueConverterBase
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

