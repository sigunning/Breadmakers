using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Breadmakers.Helpers;
using Breadmakers.Models;
using Breadmakers.ViewModels;

namespace Breadmakers.Controllers
{
    public class ScoreCardController : Controller
    {

        
        
        private DataAccess data;

        public ScoreCardController()
        {
            // connect to db and get data
            data = new DataAccess("DBNAME");

        }

        // GET: ScoreCard
        public ActionResult ScoreCard(PlayerRound id)
        {
            // param is playerscoreid

            // get course details for score calc
            Course course = data.GetCourse(id.CourseId);

            // calc scores
            id.P1 = Utils.GetPoints(id.S1, course.H1_Par, id.HCAP, course.H1_SI);
            id.P2 = Utils.GetPoints(id.S2, course.H2_Par, id.HCAP, course.H2_SI);
            id.P3 = Utils.GetPoints(id.S3, course.H3_Par, id.HCAP, course.H3_SI);
            id.P4 = Utils.GetPoints(id.S4, course.H4_Par, id.HCAP, course.H4_SI);
            id.P5 = Utils.GetPoints(id.S5, course.H5_Par, id.HCAP, course.H5_SI);
            id.P6 = Utils.GetPoints(id.S6, course.H6_Par, id.HCAP, course.H6_SI);
            id.P7 = Utils.GetPoints(id.S7, course.H7_Par, id.HCAP, course.H7_SI);
            id.P8 = Utils.GetPoints(id.S8, course.H8_Par, id.HCAP, course.H8_SI);
            id.P9 = Utils.GetPoints(id.S9, course.H9_Par, id.HCAP, course.H9_SI);
            id.P10 = Utils.GetPoints(id.S10, course.H10_Par, id.HCAP, course.H10_SI);
            id.P11 = Utils.GetPoints(id.S11, course.H11_Par, id.HCAP, course.H11_SI);
            id.P12 = Utils.GetPoints(id.S12, course.H12_Par, id.HCAP, course.H12_SI);
            id.P13 = Utils.GetPoints(id.S13, course.H13_Par, id.HCAP, course.H13_SI);
            id.P14 = Utils.GetPoints(id.S14, course.H14_Par, id.HCAP, course.H14_SI);
            id.P15 = Utils.GetPoints(id.S15, course.H15_Par, id.HCAP, course.H15_SI);
            id.P16 = Utils.GetPoints(id.S16, course.H16_Par, id.HCAP, course.H16_SI);
            id.P17 = Utils.GetPoints(id.S17, course.H17_Par, id.HCAP, course.H17_SI);
            id.P18 = Utils.GetPoints(id.S18, course.H18_Par, id.HCAP, course.H18_SI);

            // totals
            id.Out_Score = Utils.GetScoreOut(id);
            id.In_Score = Utils.GetScoreIn(id);
            id.Tot_Score = id.Out_Score + id.In_Score;

            id.Out_Pts = id.P1 + id.P2 + id.P3 + id.P4 + id.P5 + id.P6 + id.P7 + id.P8 + id.P9;
            id.In_Pts = id.P10 + id.P11 + id.P12 + id.P13 + id.P14 + id.P15 + id.P16 + id.P17 + id.P18;
            id.Tot_Pts = id.Out_Pts + id.In_Pts;

            course.Out_Par = course.H1_Par + course.H2_Par+ course.H3_Par+ course.H4_Par+ course.H5_Par+
                course.H6_Par+ course.H7_Par+ course.H8_Par+ course.H9_Par;
            course.In_Par =course.H10_Par + course.H11_Par + course.H12_Par + course.H13_Par + course.H14_Par + course.H15_Par +
                course.H16_Par + course.H17_Par + course.H18_Par ;
            course.Tot_Par = course.Out_Par + course.In_Par;

            var scoreCardViewModel = new ScoreCardViewModel
            {
                //
                ScoreCard = id,
                Course = course
            };


            return View(scoreCardViewModel);

        }
    }
}