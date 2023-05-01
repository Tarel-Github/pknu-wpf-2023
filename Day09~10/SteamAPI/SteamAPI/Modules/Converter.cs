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
                case "allHeroes":
                    koName = "모든 영웅";
                    break;
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
                case "dVa":
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
                case "cassidy":
                    koName = "캐서디";
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
                case "sojourn":
                    koName = "소전";
                    break;
                case "soldier76":
                    koName = "솔저76";
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
                case "baptiste":
                    job = "힐러";
                    break;
                case "bastion":
                    job = "딜러";
                    break;
                case "doomfist":
                    job = "탱커";
                    break;
                case "mercy":
                    job = "힐러";
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
                case "cassidy":
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
                case "sojourn":
                    job = "딜러";
                    break;
                case "soldier76":
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

        public string Image(string name)
        {
            string path = "";
            switch (name.ToLower())
            {
                case "allheroes":
                    path = "Images/All.png";
                    break;
                case "ana":
                    path = "Images/Hero/Ana.png";
                    break;
                case "ashe":
                    path = "Images/Hero/Ashe.png";
                    break;
                case "baptiste":
                    path = "Images/Hero/Bati.png";
                    break;
                case "bastion":
                    path = "Images/Hero/Bas.png";
                    break;
                case "doomfist":
                    path = "Images/Hero/Doom.png";
                    break;
                case "genji":
                    path = "Images/Hero/Gen.png";
                    break;
                case "hanzo":
                    path = "Images/Hero/Hanzo.png";
                    break;
                case "junkrat":
                    path = "Images/Hero/Junk.png";
                    break;
                case "cassidy":
                    path = "Images/Hero/Mc.png";
                    break;
                case "mei":
                    path = "Images/Hero/Mei.png";
                    break;
                case "pharah":
                    path = "Images/Hero/Para.png";
                    break;
                case "reaper":
                    path = "Images/Hero/Reap.png";
                    break;
                case "sojourn":
                    path = "Images/Hero/So.png";
                    break;
                case "soldier76":
                    path = "Images/Hero/76.png";
                    break;
                case "sombra":
                    path = "Images/Hero/Som.png";
                    break;
                case "symmetra":
                    path = "Images/Hero/Sym.png";
                    break;
                case "tracer":
                    path = "Images/Hero/Tracer.png";
                    break;
                case "widowmaker":
                    path = "Images/Hero/Widow.png";
                    break;
                case "echo":
                    path = "Images/Hero/Echo.png";
                    break;
                case "torbjorn":
                    path = "Images/Hero/Tor.png";
                    break;
                case "zenyatta":
                    path = "Images/Hero/Zenya.png";
                    break;
                case "dva":
                    path = "Images/Hero/Dva.png";
                    break;
                case "mercy":
                    path = "Images/Hero/Mercy.png";
                    break;
                case "orisa":
                    path = "Images/Hero/Ori.png";
                    break;
                case "reinhardt":
                    path = "Images/Hero/Rein.png";
                    break;
                case "roadhog":
                    path = "Images/Hero/Hog.png";
                    break;
                case "sigma":
                    path = "Images/Hero/Sig.png";
                    break;
                case "winston":
                    path = "Images/Hero/Win.png";
                    break;
                case "wrecking ball":
                    path = "Images/Hero/Wreck.png";
                    break;
                case "zarya":
                    path = "Images/Hero/Za.png";
                    break;
                case "junkerqueen":
                    path = "Images/Hero/Q.png";
                    break;
                case "ramattra":
                    path = "Images/Hero/Rama.png";
                    break;
                case "kiriko":
                    path = "Images/Hero/Kiri.png";
                    break;
                default:
                    path = "Images/None.png";
                    break;

            }
            return path;

        }

    }



}
