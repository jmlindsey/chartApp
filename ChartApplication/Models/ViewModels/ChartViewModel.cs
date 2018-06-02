using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChartApplication.Models.ViewModels
{
    public class ChartViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeInitials { get; set; }
        public string Position { get; set; }
        public string Credentials { get; set; }

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
        [Display(Name = "Upper Left")]
        public string UpperLeftSound { get; set; }
        [Display(Name = "Upper Right")]
        public string UpperRightSound { get; set; }
        [Display(Name = "Lower Left")]
        public string LowerLeftSound { get; set; }
        [Display(Name = "Lower Right")]
        public string LowerRightSound { get; set; }
        [Display(Name = "Middle")]
        public string MiddleSound { get; set; }
        public Nullable<int> RR { get; set; }
        public Nullable<int> HR { get; set; }
        public string BP { get; set; }
        public Nullable<int> Saturation { get; set; }
        public string Cough { get; set; }

        [Display(Name ="O2 Device")]
        public string O2Device { get; set; }
        public Nullable<int> Flow { get; set; }

        // intervention
        public string Activity1 { get; set; }
        public string Activity2 { get; set; }
        public string Activity3 { get; set; }
        public string Activity4 { get; set; }

        [Display(Name = "Route Given")]
        public string RouteGiven { get; set; }
        public string ResponseToTreatment { get; set; }

        [Display(Name = "Patient Toleration")]
        public string HowTolerated { get; set; }



    }
}
