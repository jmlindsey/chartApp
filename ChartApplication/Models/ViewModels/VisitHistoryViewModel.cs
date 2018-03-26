using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartApplication.Models.ViewModels
{
    public class VisitHistoryViewModel
    {
        public IQueryable<Visit> VisitHistory { get; set; }
        public Nullable<int> PatientId { get; set; }
        public string PatientName { get; set; }
        
    }
}