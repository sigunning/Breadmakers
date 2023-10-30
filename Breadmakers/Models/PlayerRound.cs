using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Breadmakers.Models
{
    public class PlayerRound
    {
		
		public string PlayerScoreId { get; set; }
		public string SocietyId { get; set; }
		public string SocietyName { get; set; }
		public string CompetitionId { get; set; }
		public string CompetitionName { get; set; }
		public string CodeName { get; set; }
		public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string RoundId { get; set; }
		public DateTime RoundDate { get; set; }
		public string PlayerId { get; set; }
		public string PlayerName { get; set; }
		public int HCAP { get; set; }
		public int Discard9 { get; set; }
		public int Tot_Pts { get; set; }
		public int Dis_Pts { get; set; }
		public int Net_Pts { get => Tot_Pts - Dis_Pts; }

        // Scores
        public int S1 { get; set; }
        public int S2 { get; set; }
        public int S3 { get; set; }
        public int S4 { get; set; }
        public int S5 { get; set; }
        public int S6 { get; set; }
        public int S7 { get; set; }
        public int S8 { get; set; }
        public int S9 { get; set; }
        public int S10 { get; set; }
        public int S11 { get; set; }
        public int S12 { get; set; }
        public int S13 { get; set; }
        public int S14 { get; set; }
        public int S15 { get; set; }
        public int S16 { get; set; }
        public int S17 { get; set; }
        public int S18 { get; set; }
        public int Out_Score { get; set; }
        public int In_Score { get; set; }
        public int Tot_Score { get; set; }

        // Points
        public int P1 { get; set; }
        public int P2 { get; set; }
        public int P3 { get; set; }
        public int P4 { get; set; }
        public int P5 { get; set; }
        public int P6 { get; set; }
        public int P7 { get; set; }
        public int P8 { get; set; }
        public int P9 { get; set; }

        public int P10 { get; set; }
        public int P11 { get; set; }
        public int P12 { get; set; }
        public int P13 { get; set; }
        public int P14 { get; set; }
        public int P15 { get; set; }
        public int P16 { get; set; }
        public int P17 { get; set; }
        public int P18 { get; set; }
        public int Out_Pts { get; set; }
        public int In_Pts { get; set; }
    }


    public class Round
    {
        public int id { get; set; }
        public int Seq { get; set; }
        public string CodeName { get; set; }
        public string RoundId { get; set; }
        public string CourseName { get; set; }
        public DateTime RoundDate { get; set; }
        public string ImgCourse { get; set; }
        

        public List<PlayerRound> SummaryScores { get; set; }

    }

    public class SummaryScore
    {
        public string PlayerScoreId { get; set; }
        public string RoundId { get; set; }
        public string PlayerName { get; set; }
        public int HCAP { get; set; }

        public int Tot_Score { get; set; }
        public int Dis_Pts { get; set; }

        public int Tot_Pts { get; set; }

        public int Net_Pts { get => Tot_Pts - Dis_Pts; }

    }

}