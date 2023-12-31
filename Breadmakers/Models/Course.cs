﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Breadmakers.Models
{
    public class Course
    {
        public string Id { get; set; }

        public string CourseId { get; set; }

        public string CourseName { get; set; }
        public int PAR { get; set; }
        public int SSS { get; set; }
        public int Yards { get; set; }
        public int H1_Par { get; set; }
        public int H2_Par { get; set; }
        public int H3_Par { get; set; }
        public int H4_Par { get; set; }
        public int H5_Par { get; set; }
        public int H6_Par { get; set; }
        public int H7_Par { get; set; }
        public int H8_Par { get; set; }
        public int H9_Par { get; set; }
        public int H10_Par { get; set; }
        public int H11_Par { get; set; }
        public int H12_Par { get; set; }
        public int H13_Par { get; set; }
        public int H14_Par { get; set; }
        public int H15_Par { get; set; }
        public int H16_Par { get; set; }
        public int H17_Par { get; set; }
        public int H18_Par { get; set; }


        public int H1_SI { get; set; }
        public int H2_SI { get; set; }
        public int H3_SI { get; set; }
        public int H4_SI { get; set; }
        public int H5_SI { get; set; }
        public int H6_SI { get; set; }
        public int H7_SI { get; set; }
        public int H8_SI { get; set; }
        public int H9_SI { get; set; }
        public int H10_SI { get; set; }
        public int H11_SI { get; set; }
        public int H12_SI { get; set; }
        public int H13_SI { get; set; }
        public int H14_SI { get; set; }
        public int H15_SI { get; set; }
        public int H16_SI { get; set; }
        public int H17_SI { get; set; }
        public int H18_SI { get; set; }

        // totals
        public int Out_Par { get; set; }
        public int In_Par { get; set; }
        public int Tot_Par { get; set; }
    }
}