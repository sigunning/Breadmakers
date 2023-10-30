using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Breadmakers.Models
{
    public class BreadBoard
    {

        public int id { get; set; }
        public string CompetitionId { get; set; }
        public string CodeName { get; set; }
        public string CompetitionName { get; set; }
        public string CompetitionDescription { get; set; }
        public string Venue { get; set; }
        public string Accommodation { get; set; }
        public string MonthYear { get => StartDate.ToString("MMM") + " " + StartDate.ToString("yyyy"); }
        public string ImgChamp1
        {
            get
            {
                if (this.Winner.Split('&').Length != 2)
                    return "~/Content/Images/Mugs/unknown.jpg";
                return "~/Content/Images/Mugs/" + this.Winner.Split('&')[0].Trim() + ".jpg";
            }
        }
        public string ImgChamp2
        {
            get
            {
                if (this.Winner.Split('&').Length != 2)
                    return "~/Content/Images/Mugs/unknown.jpg";
                return "~/Content/Images/Mugs/" + this.Winner.Split('&')[1].Trim() + ".jpg";
            }
        }
        public string SmugShot
        { get => string.Concat("~/Content/BRD/", CodeName, "_smugshot", ".jpg"); }

        public DateTime StartDate { get; set; }
        public string Winner { get; set; }
        public string BakeOff { get; set; }
        public string PostScript { get; set; }

        public List<Round> Rounds { get; set; }
    }


}