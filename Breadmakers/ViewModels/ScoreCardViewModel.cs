using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Breadmakers.Models;

namespace Breadmakers.ViewModels
{
    public class ScoreCardViewModel
    {
        public PlayerRound ScoreCard { get; set; }

        public Course Course { get; set; }
    }
}