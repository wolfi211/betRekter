using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryouts1._1
{
    class Player
    {
        private string name;
        private string urlTwitter;
        private string urlFacebook;
        private string urlHltv;

        #region CONSTRUCTORS
        public Player()
        {

        }

        public Player(string s)
        {
            name = s;
        }

        public Player(string s, string url1, string url2, string url3)
        {
            name = s;
            urlTwitter = url1;
            urlFacebook = url2;
            urlHltv = url3;
        }
        #endregion

        #region GET_SET_METHODS
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
        #endregion
    }
}
