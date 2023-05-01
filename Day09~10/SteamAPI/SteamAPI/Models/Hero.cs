using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAPI.Models
{
    internal class Hero
    {
        public string name { get; set; }             // 캐릭터 이름
        public string timePlayed { get; set; }       // 플레이 타임
        public string winrate { get; set; }          // 승률
        public int avg_dmg { get; set; }             // 평딜
        public int avg_heal { get; set; }            // 평힐
        public string KPL { get; set; }               // 킬뎃율
    }
}
