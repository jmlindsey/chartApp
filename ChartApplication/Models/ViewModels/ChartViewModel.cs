using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChartApplication.Models.ViewModels
{
    public class ChartViewModel
    {
        public HistoryViewModel historyVM;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDone { get; set; } = DateTime.Now;
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime TimeDone { get; set; } = DateTime.Now;


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DischargeDate { get; set; }

        public Nullable<int> PatientId { get; set; }
        public string PatientName { get; set; }

        //vitals
        public string UpperLeftSound { get; set; }
        public string UpperRightSound { get; set; }
        public string LowerLeftSound { get; set; }
        public string LowerRightSound { get; set; }
        public string MiddleSound { get; set; }
        public Nullable<int> RR { get; set; }
        public Nullable<int> HR { get; set; }
        public string BP { get; set; }
        public Nullable<int> Saturation { get; set; }
        public string Cough { get; set; }
        public string O2Device { get; set; }
        public Nullable<int> Flow { get; set; }

        // intervention
        public string Activity1 { get; set; }
        public string Activity2 { get; set; }
        public string Activity3 { get; set; }
        public string Activity4 { get; set; }
        public string RouteGiven { get; set; }
        public string ResponseToTreatment { get; set; }
        public string HowTolerated { get; set; }



    }
}
