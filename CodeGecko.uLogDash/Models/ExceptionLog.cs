using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace CodeGecko.Packages.Umbraco.uLogDash.Models
{
    [TableName("cgExceptionLog"), PrimaryKey("ID", autoIncrement=true)]
    public class ExceptionLog {

        [PrimaryKeyColumn(AutoIncrement=true)]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Server { get; set; }

        public string Thread { get; set; }

        public string Level { get; set; }

        public string Logger { get; set; }

        public string Message { get; set; }

    }
}