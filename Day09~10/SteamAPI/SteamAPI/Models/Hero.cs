using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAPI.Models
{
    internal class Hero
    {
        public string name { get; set; }        // 캐릭터 이름
        public string timePlayed { get; set; }  // 플레이 타임
        public int gamesWon  { get; set; }  // 승리 횟수
        public int weaponAccuracy { get; set; } // 무기 명중률
        public int criticalHitAccuracy { get; set; }    // 치명타 적중률
        public float eliminationsPerLife { get; set; }  // 킬뎃율
        public int multiKillBest { get; set; }          // 연속처치
        public int objectiveKills { get; set; }         // 물체파괴
    }
}
