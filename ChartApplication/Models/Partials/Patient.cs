using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChartApplication.Models
{
    [MetadataType(typeof(PatientMetaData))]
    public partial class Patient
    {
    }

    public class PatientMetaData
    {
        [Display(Name = "Patient Name")]
        public string PatientFirst { get; set; }
        public string PatientLast { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime BirthDate { get; set; }
    }
}