using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace UBookable.Models
{
    [TableName("UBBookers")]
    [PrimaryKey("BookerID", autoIncrement = true)]
    [ExplicitColumns]
    public class Booker
    {
        [Column("BookerID")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int BookerID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("MemberID")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string MemberID { get; set; }
    }
}