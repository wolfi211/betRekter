using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryouts1._1
{
    class Team
    {
        private int ID;
        private string name;
        private string urlFacebook;
        private string urlTwitter;
        private string urlHltv;
        private Player[] players = new Player[25];
        private int cnt = 0;
        private int cnt2 = 0;

        #region CONSTRUCTORS
        public Team()
        {

        }

        public Team(int i)
        {
            ID = i;
        }

        public Team(string s)
        {
            name = s;
        }

        public Team(int i, string s)
        {
            ID = i;
            name = s;
        }
        public Team(string s, string url1, string url2, string url3)
        {
            name = s;
            urlTwitter = url1;
            urlFacebook = url2;
            urlHltv = url3;
        }

        public Team(int i, string s, string url1, string url2, string url3)
        {
            ID = i;
            name = s;
            urlTwitter = url1;
            urlFacebook = url2;
            urlHltv = url3;
        }
        #endregion

        #region GET_SET_METHODS
        #region GET
        public int getID()
        {
            return ID;
        }

        public string getName()
        {
            return name;
        }

        public string getTwitter()
        {
            return urlTwitter;
        }

        public string getFacebook()
        {
            return urlFacebook;
        }

        public string getHltv()
        {
            return urlHltv;
        }

        public Player getPlayer(int i)
        {
            return players[i];
        }

        public Player getNextPlayer()
        {
            if (cnt2 == 25) cnt2 = 0;
            return players[cnt2++];
        }
        #endregion

        #region SET
        public void setID(int i)
        {
            ID = i;
        }

        public void setName(string s)
        {
            name = s;
        }

        public void setTwitter(string s)
        {
            urlTwitter = s;
        }

        public void setFacebook(string s)
        {
            urlFacebook = s;
        }

        public void setHltv(string s)
        {
            urlHltv = s;
        }

        public void addPlayer(Player p)
        {
            if (cnt == 25) cnt = 0;
            players[cnt++] = p;
        }

        public void setPlayer(Player p, int i)
        {
            players[i] = p;
        }
        #endregion
        #endregion

        public void resetCounter()
        {
            cnt = 0;
        }

        public void resetCounter2()
        {
            cnt2 = 0;
        }
    }
}
