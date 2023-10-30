using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Breadmakers.Models;
using Breadmakers.ViewModels;
using Breadmakers.Helpers;
using MoreLinq;
using System.IO;

namespace Breadmakers.Controllers
{
    public class BreadBoardController : Controller
    {

        private string _societyId = Helpers.Utils.C_societyId;

        private List<PlayerRound> _lstPlayerRounds;
        private DataAccess data;



        // GET: BreadBoard
        public ActionResult BrdList()
        {
            List<BreadBoard> _lstBreadboard;

            data = new DataAccess("DBNAME");

            _lstBreadboard = data.GetListBreadBoard(_societyId);

            var breadBoardViewModel = new BreadBoardViewModel
            {
                //
                BreadBoardList = _lstBreadboard
            };


            return View(breadBoardViewModel);
        }


        public ActionResult BrdDetail(string id)
        {

            data = new DataAccess("DBNAME");

            string competitionId = id;
            BreadBoard bread = data.GetBreadboard(competitionId);


            // get scores
            _lstPlayerRounds = data.GetPlayerScores(competitionId);

            List<Round> lstRounds = _lstPlayerRounds
               .Where(o => o.CompetitionId == competitionId)
               .Select(o => new Round
               {
                   RoundId = o.RoundId,
                   CourseName = o.CourseName,
                   RoundDate = o.RoundDate,
                   CodeName = o.CodeName
               })
               .DistinctBy(o => o.RoundId)
               .OrderBy(o => o.RoundDate)
               .ToList();

            // assign sequence
            int i = 0;
            foreach (Round round in lstRounds)
            {
                i++;
                round.Seq = i;

                // get scores
                List<PlayerRound> lstSummaryScores = _lstPlayerRounds
               .Where(o => o.RoundId == round.RoundId)
               .OrderBy(o => (o.Tot_Pts - o.Dis_Pts), OrderByDirection.Descending)
               .ToList();

                /*
                List<Score> lstScores = _lstPlayerScores
               .Where(o => o.RoundId == round.RoundId)
               .Select(o => new Score
               {
                   PlayerScoreId = o.PlayerScoreId,
                   PlayerName = o.PlayerName,
                   HCAP = o.HCAP,
                   Dis_Pts = o.Dis_Pts,
                   Tot_Pts = o.Tot_Pts
               })
               .DistinctBy(o => o.PlayerName)
               .OrderBy(o => (o.Tot_Pts-o.Dis_Pts), OrderByDirection.Descending)
               .ToList();
                */

               
                round.SummaryScores = lstSummaryScores;

            }
            bread.Rounds = lstRounds;

            // read Bake-off text
            string filePath = string.Concat("~/Content/BRD/", bread.CodeName, "_bakeoff.htm");
            try
            {
                using (StreamReader sr = new StreamReader(Server.MapPath(filePath)))
                {
                    bread.BakeOff = sr.ReadToEnd();
                }
            }
            catch
            {
                bread.BakeOff = "No Bake-Off Today";
            }

            // read postscript text
            filePath = string.Concat("~/Content/BRD/", bread.CodeName, "_postscript.htm");
            try
            {
                using (StreamReader sr = new StreamReader(Server.MapPath(filePath)))
                {
                    bread.PostScript = sr.ReadToEnd();
                }
            }
            catch
            {
                bread.BakeOff = "No Bake-Off Today";
            }



            var breadboardViewModel = new BreadBoardViewModel
            {
                BreadBoard = bread
            };

            return View(breadboardViewModel);
        }

    }
}