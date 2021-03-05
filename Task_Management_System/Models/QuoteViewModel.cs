using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Management_System.Models
{
    public class QuoteViewModel
    {
        public int QuoteID { get; set; }
        public string QuoteType { get; set; }
        public string Contact { get; set; }
        public string Task { get; set; }
        public string DueDate { get; set; }
        public string TaskType { get; set; }
    }
}