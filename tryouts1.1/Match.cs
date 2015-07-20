using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryouts1._1
{
    class Match
    {
        private int ID;
        private Team t1;
        private Team t2;
        private DateTime start;
        private string league;
        private int t1p;
        private int t2p;
        private string urlHltv;
        private string urlCsgo;
        private bool lan;
        private int bo;
        private Team fav;
        private int win;
        private int amntBet;
        private int amntWon;
        private Mapset mapset;

        #region CONSTRUCTORS
        public Match()
        {

        }

        public Match(int i)
        {
            ID = i;
        }

        public Match(int i, Team t1, Team t2)
        {
            ID = i;
            this.t1 = t1;
            this.t2 = t2;
        }

        public Match(Team t1, Team t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }

        public Match(int i, Team t1, Team t2, string date)
        {
            ID = i;
            this.t1 = t1;
            this.t2 = t2;
            start = DateTime.ParseExact(date, "YYYY_MM_dd_HH_mm", CultureInfo.InvariantCulture);
        }

        public Match(int i, Team t1, Team t2, string date, string league, int bo, bool lan, string hltv, string csgo, Team fav, int t1p, int t2p, Mapset m, int amnt)
        {
            ID = i;
            this.t1 = t1;
            this.t2 = t2;
            start = DateTime.ParseExact(date, "YYYY_MM_dd_HH_mm", CultureInfo.InvariantCulture);
            this.league = league;
            this.bo = bo;
            this.lan = lan;
            this.urlHltv = hltv;
            this.urlCsgo = csgo;
            this.fav = fav;
            this.t1p = t1p;
            this.t2p = t2p;
            this.mapset = m;
            this.amntBet = amnt;
        }
        //YYYY_MM_dd_HH_mm
        #endregion

        #region GET_SET METHODS

        #region GET
        public int getID()
        {
            return ID;
        }

        public Team getTeamOne()
        {
            return t1;
        }

        public Team getTeamTwo()
        {
            return t2;
        }

        public Team getFavor()
        {
            return fav;
        }

        public DateTime getStart()
        {
            return start;
        }

        public string getLeague()
        {
            return league;
        }

        public string getHltv()
        {
            return urlHltv;
        }

        public string getCsgo()
        {
            return urlCsgo;
        }

        public int getBestOf()
        {
            return bo;
        }

        public int getTeamOnePercet()
        {
            return t1p;
        }

        public int getTeamTwoPercent()
        {
            return t2p;
        }

        public Mapset getMapset()
        {
            return mapset;
        }

        public int getBetAmount()
        {
            return amntBet;
        }

        public int getWonAmount()
        {
            return amntWon;
        }

        public int getWinner()
        {
            return win;
        }

        public bool getLan()
        {
            return lan;
        }
        #endregion

        #region SET
        public void setID(int i)
        {
            ID = i;
        }

        public void setTeamOne(Team t)
        {
            t1 = t;
        }

        public void setTeamTwo(Team t)
        {
            t2 = t;
        }

        public void setStartDate(string s)
        {
            start = DateTime.ParseExact(s, "YYYY_MM_dd_HH_mm", CultureInfo.InvariantCulture);
        }

        public void setLeague(string s)
        {
            league = s;
        }

        public void setTeamOnePercent(int i)
        {
            t1p = i;
        }

        public void setTeamTwoPercent(int i)
        {
            t2p = i;
        }

        public void setUrlHltv(string s)
        {
            urlHltv = s;
        }

        public void setUrlCsgo(string s)
        {
            urlCsgo = s;
        }

        public void setLan(bool b)
        {
            lan = b;
        }

        public void setBestOf(int i)
        {
            bo = i;
        }

        public void setTeamFavorite(Team t)
        {
            fav = t;
        }

        public void setWinner(int i)
        {
            win = i;
        }

        public void setBetAmount(int i)
        {
            amntBet = i;
        }

        public void setWonAmount(int i)
        {
            amntWon = i;
        }

        public void setMapset(Mapset m)
        {
            mapset = m;
        }
        #endregion
        
        #endregion

    }
}
