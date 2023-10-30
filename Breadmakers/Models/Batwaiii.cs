using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Breadmakers.Models
{
    public class Batwaiii
    {
        public int id { get; set; }
        public string CompetitionId { get; set; }
        public string CodeName { get; set; }
        public string CompetitionName { get; set; }
        public string CompetitionDescription{ get; set; }
        public string Venue { get; set; }
        public string Accommodation { get; set; }
        public string MonthYear { get => StartDate.ToString("MMM") +" "+  StartDate.ToString("yyyy");  }
        public string ImgWinner 
        { get => string.Concat("~/Content/Images/Mugs/", Winner==null ? "Unknown" : Winner.ToLower(), ".jpg"); }
        public string ImgChamp { get; set; }
        //{ get => string.Concat("~/Content/Images/Mugs/", Winner==null ? "Unknown" : Winner.ToLower(), ".jpg"); }

        public string GalleryPath
            // bar will be replaced with / in gallery view
        { get => string.Concat("~|Content|BAT|", CodeName, "|Gallery"); }
     
        public DateTime StartDate { get; set; }
        public string Winner { get; set; }
        public string PostScript { get; set; }
        public string ImgPresentation { get; set; }

        public List<Round> Rounds { get; set; }

        public List<SummaryScore> TotalScores { get; set; }
    }

    //public class BatRound
    //{
    //    public int id { get; set; }
    //    public int Seq { get; set; }
    //    public string CodeName { get; set; }
    //    public string RoundId { get; set; }
    //    public string CourseName { get; set; }
    //    public DateTime RoundDate { get; set; }
    //    public string ImgCourse
    //    { get => string.Concat("~/Content/BAT/", CodeName, "/course", Seq.ToString(), ".jpg"); }

    //    public List<PlayerRound> SummaryScores { get; set; }

    //}

}