using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Breadmakers.Models;

namespace Breadmakers.Helpers
{
    public static class Utils
    {

        public const string C_societyId = "25823207-0041-4C15-90C3-5422AF3F5249";


        //public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        //{
        //    return items.GroupBy(property).Select(x => x.First());
        //}

        public static int GetShots(int hCap, int strokeIndex, int HCapPct = 1)
        {
            // 1 = Full; 2 = 7 / 8; 3 = 3 / 4
            switch (HCapPct)
            {
                case 1: return (int)(Math.Floor(1 + (hCap - strokeIndex) / 18.0));
                case 2: return (int)(Math.Floor(1 + (Math.Round(hCap * 7.0 / 8.0, 0) - strokeIndex) / 18.0));
                case 3: return (int)(Math.Floor(1 + (Math.Round(hCap * 3.0 / 4.0, 0) - strokeIndex) / 18.0));
                default: return (int)(Math.Floor(1 + (hCap - strokeIndex) / 18.0));
            }

        }

        public static int GetPoints(int gross, int holePar, int hCap, int strokeIndex, int HCapPct = 1)
        {
            // get strokes, then work out points
            int strokes = GetShots(hCap, strokeIndex, HCapPct);
            return (gross == 0 ? 0 : Math.Max(0, (holePar + strokes - gross + 2)));
        }

        public static int GetScoreOut(PlayerRound ps)
        {
            return (ps.S1 + ps.S2 + ps.S3 + ps.S4 + ps.S5 + ps.S6 + ps.S7 + ps.S8 + ps.S9);

        }

        public static int GetScoreIn(PlayerRound ps)
        {
            return (ps.S10 + ps.S11 + ps.S12 + ps.S13 + ps.S14 + ps.S15 + ps.S16 + ps.S17 + ps.S18);

        }

        public static Dictionary<string, string> Csv2Dict(string pathToCsvFile)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            using (var reader = new StreamReader(pathToCsvFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) continue;
                    var values = line.Split(';');
                    dictionary.Add(values[0], values[1]);
                }
            }

            return dictionary;
        }
    }

    

}