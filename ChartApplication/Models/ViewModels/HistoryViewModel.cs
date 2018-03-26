using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChartApplication.Models.ViewModels
{
    public class HistoryViewModel
    {
        public IQueryable<ChartViewModelInstance> History { get; set; }



    }
}