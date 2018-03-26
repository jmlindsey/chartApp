using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChartApplication.Models
{
    [MetadataType(typeof(VisitMetaData))]
    public partial class Visit
    {

    }

    public class VisitMetaData
    {
        public int Room { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime VisitDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DischargeDate { get; set; }
        public Nullable<int> PatientId { get; set; }

        public virtual Patient Patient { get; set; }

    }
}