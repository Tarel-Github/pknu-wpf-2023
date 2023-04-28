using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SteamAPI.Modules
{
    public class Converter
    {

        public string Lang(string name)
        {
            string koName ="";
            switch (name)
            {
                case "ana":
                    koName = "아나";
                    break;
                case "ashe":
                    koName = "애쉬";
                    break;
                case "baptiste":
                    koName = "바티스트";
                    break;
                case "bastion":
                    koName = "바스티온";
                    break;
                case "brigitte":
                    koName = "브리기테";
                    break;
                case "doomfist":
                    koName = "둠피스트";
                    break;
                case "dva":
                    koName = "디바";
                    break;
                case "echo":
                    koName = "에코";
                    break;
                case "genji":
                    koName = "겐지";
                    break;
                case "hanzo":
                    koName = "한조";
                    break;
                case "junkrat":
                    koName = "정크랫";
                    break;
                case "lucio":
                    koName = "루시우";
                    break;
                case "mccree":
                    koName = "맥크리";
                    break;
                case "mei":
                    koName = "메이";
                    break;
                case "mercy":
                    koName = "메르시";
                    break;
                case "moira":
                    koName = "모이라";
                    break;
                case "orisa":
                    koName = "오리사";
                    break;
                case "pharah":
                    koName = "파라";
                    break;
                case "reaper":
                    koName = "리퍼";
                    break;
                case "reinhardt":
                    koName = "라인하르트";
                    break;
                case "roadhog":
                    koName = "로드호그";
                    break;
                case "sigma":
                    koName = "시그마";
                    break;
                case "soldier: 76":
                    koName = "솔저: 76";
                    break;
                case "sombra":
                    koName = "솜브라";
                    break;
                case "symmetra":
                    koName = "시메트라";
                    break;
                case "torbjorn":
                    koName = "토르비욘";
                    break;
                case "tracer":
                    koName = "트레이서";
                    break;
                case "widowmaker":
                    koName = "위도우메이커";
                    break;
                case "winston":
                    koName = "윈스턴";
                    break;
                case "wrecking ball":
                    koName = "레킹볼";
                    break;
                case "zarya":
                    koName = "자리야";
                    break;
                case "zenyatta":
                    koName = "젠야타";
                    break;
                case "junkerQueen":
                    koName = "정커퀸";
                    break;
                case "ramattra":
                    koName = "라마트라";
                    break;
                case "kiriko":
                    koName = "키리코";
                    break;
                default:
                    koName = name;
                    break;
            }
            return koName;
        }

        public string Job(string name) 
        {
            string job = "";
            switch (name.ToLower())
            {
                case "ana":
                    job = "힐러";
                    break;
                case "ashe":
                    job = "딜러";
                    break;
                case "bastion":
                    job = "딜러";
                    break;
                case "doomfist":
                    job = "탱커";
                    break;
                case "genji":
                    job = "딜러";
                    break;
                case "hanzo":
                    job = "딜러";
                    break;
                case "junkrat":
                    job = "딜러";
                    break;
                case "mccree":
                    job = "딜러";
                    break;
                case "mei":
                    job = "딜러";
                    break;
                case "pharah":
                    job = "딜러";
                    break;
                case "reaper":
                    job = "딜러";
                    break;
                case "soldier: 76":
                    job = "딜러";
                    break;
                case "sombra":
                    job = "딜러";
                    break;
                case "symmetra":
                    job = "딜러";
                    break;
                case "tracer":
                    job = "힐러";
                    break;
                case "widowmaker":
                    job = "딜러";
                    break;
                case "echo":
                    job = "딜러";
                    break;
                case "torbjorn":
                    job = "딜러";
                    break;
                case "zenyatta":
                    job = "힐러";
                    break;
                case "dva":
                    job = "탱커";
                    break;
                case "orisa":
                    job = "탱커";
                    break;
                case "reinhardt":
                    job = "탱커";
                    break;
                case "roadhog":
                    job = "탱커";
                    break;
                case "sigma":
                    job = "탱커";
                    break;
                case "winston":
                    job = "탱커";
                    break;
                case "wrecking ball":
                    job = "탱커";
                    break;
                case "zarya":
                    job = "탱커";
                    break;
                case "junkerqueen":
                    job = "탱커";
                    break;
                case "ramattra":
                    job = "탱커";
                    break;
                case "kiriko":
                    job = "힐러";
                    break;

            }
            return job;
        }



    }



}
