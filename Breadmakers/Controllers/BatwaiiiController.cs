using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Breadmakers.Models;
using Breadmakers.ViewModels;
using Breadmakers.Helpers;
using MoreLinq;


namespace Breadmakers.Controllers
{
    

    public class BatwaiiiController : Controller
    {
        

        private string _societyId = Helpers.Utils.C_societyId;
        
        private List<PlayerRound> _lstPlayerRounds;
        private DataAccess data;

        public BatwaiiiController()
        {
            // connect to db and get data
           

            
        }

        
        public ActionResult BatList()
        {
            List<Batwaiii> _lstBatwaiii;
            
            data = new DataAccess("DBNAME");

            _lstBatwaiii = data.GetListBatwaiii(_societyId);

            var batwaiiiViewModel = new BatwaiiiViewModel
            {
                //
                BatwaiiiList = _lstBatwaiii
            };


            return View(batwaiiiViewModel);
        }

        public ActionResult BatDetail(string id)
        {

            data = new DataAccess("DBNAME");

            Batwaiii bat = data.GetBatwaiii( id);

            // get scores
            _lstPlayerRounds = data.GetPlayerScores(bat.CompetitionId);

            // create rounds
            //List<Round> lstRounds = data.GetRounds(bat.CompetitionId);
            //bat.Rounds = lstRounds;

            
            List<Round> lstRounds = _lstPlayerRounds
               .Where(o => o.CompetitionId == bat.CompetitionId)
               .Select(o => new Round { RoundId = o.RoundId, CourseName = o.CourseName, 
                   RoundDate = o.RoundDate, CodeName= o.CodeName })
               .DistinctBy(o =>  o.RoundId)
               .OrderBy(o => o.RoundDate)
               .ToList();

            List<SummaryScore> lstTotalScores = this._lstPlayerRounds.Where<PlayerRound>((Func<PlayerRound, bool>)(o => o.CompetitionId == bat.CompetitionId)).GroupBy<PlayerRound, string>((Func<PlayerRound, string>)(o => o.PlayerName)).Select<IGrouping<string, PlayerRound>, SummaryScore>((Func<IGrouping<string, PlayerRound>, SummaryScore>)(s => new SummaryScore()
            {
                PlayerName = s.Select<PlayerRound, string>((Func<PlayerRound, string>)(x => x.PlayerName)).FirstOrDefault<string>(),
                Tot_Score = s.Sum<PlayerRound>((Func<PlayerRound, int>)(x => x.Tot_Score)),
                Dis_Pts = s.Sum<PlayerRound>((Func<PlayerRound, int>)(x => x.Dis_Pts)),
                Tot_Pts = s.Sum<PlayerRound>((Func<PlayerRound, int>)(x => x.Tot_Pts))
            })).OrderByDescending<SummaryScore, int>((Func<SummaryScore, int>)(s => s.Net_Pts)).ToList<SummaryScore>();


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

                // set image path
                round.ImgCourse = string.Concat("~/Content/BAT/", bat.CodeName, "/course", round.Seq.ToString(), ".jpg");

                round.SummaryScores = lstSummaryScores;

            }


            bat.Rounds = lstRounds;
            bat.TotalScores = lstTotalScores;

            //var lst3 = lstRounds.Select(o => new Round { RoundId = o.RoundId, CourseName = o.CourseName, RoundDate = o.RoundDate }).Distinct().ToList();





            // read PostScript text
            string filePath = string.Concat("~/Content/BAT/", bat.CodeName, "/postscript.htm");
            if (System.IO.File.Exists(Server.MapPath(filePath)))
            {
                using (StreamReader sr = new StreamReader(Server.MapPath(filePath)))
                {
                    bat.PostScript = sr.ReadToEnd();
                }
            }
            else
            {
                bat.PostScript = "Nothing to report yet";
            }

            // presentation photo?
            filePath = string.Concat("~/Content/BAT/", bat.CodeName, "/presentation.jpg");
            if (System.IO.File.Exists(Server.MapPath(filePath)))
            {
                bat.ImgPresentation = filePath;
            }
            else
            {
                bat.ImgPresentation = string.Concat("~/Content/Images/Mugs/unknown.jpg");
            }

            // champ photo?
            filePath = string.Concat("~/Content/BAT/", bat.CodeName, "/champ.jpg");
            if (System.IO.File.Exists(Server.MapPath(filePath)))
            {
                bat.ImgChamp = filePath;
            }
            else
            {
                bat.ImgChamp = string.Concat("~/Content/Images/Mugs/",
                    bat.Winner == null ? "Unknown" : bat.Winner.ToLower(), ".jpg");
            }



            var batwaiiiViewModel = new BatwaiiiViewModel
            {
                Batwaiii = bat
            };

            return View(batwaiiiViewModel);
        }


        
    }
}