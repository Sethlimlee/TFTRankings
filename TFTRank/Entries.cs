using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFTRank
{
    public class Entries
    {
        public string SummonerName { get; set; }
        public bool HotStreak { get; set; }
        public int Wins { get; set; }
        public bool Veteran { get; set; }
        public int Losses { get; set; }
        public string Rank { get; set; }
        public bool Inactive { get; set; }
        public bool FreshBlood { get; set; }
        public string SummonerId { get; set; }
        public int LeaguePoints { get; set; }
    }
}