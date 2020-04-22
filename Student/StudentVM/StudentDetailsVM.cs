using System;
using System.Collections.Generic;
using System.Text;

namespace StudentVM
{
    public class StudentDetailsVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Class { get; set; }
        public string Divistion { get; set; }
    }
    public class ResponseVM
    {
        public int ResCode { get; set; }
        public string ResMessage { get; set; }
    }
}
