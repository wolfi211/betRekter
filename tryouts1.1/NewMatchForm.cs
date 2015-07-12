using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace tryouts1._1
{
    public partial class NewMatchForm : Form
    {
        #region VARIABLE DECLARATIONS
        private TextBox teamPro;
        private TextBox teamCon;
        private TextBox yearStrt;
        private TextBox monthStrt;
        private TextBox dayStrt;
        private TextBox hourStrt;
        private TextBox minuteStrt;
        private TextBox league;
        private ComboBox bestOf;
        private TextBox csgoLnk;
        private TextBox hltvLnk;
        private TextBox betAmnt;
        private ComboBox betTeam;
        private TextBox notes;
        private Label starts;
        private Label versus;
        private Label doubleDot;
        private Label leagueLbl;
        private Label bestOfLbl;
        private Label csgolounge;
        private Label hltvLbl;
        private Label iBet;
        private Label on;
        private Panel teamProPic;
        private Panel teamConPic;
        private Button submit;
        private CheckBox lan;
        private CheckBox wOutQ;
        private Label lanLbl;
        #endregion

        public NewMatchForm()
        {
            this.Load += new EventHandler(formLoad);
            
        }

        private void textBoxClick(object sender, EventArgs e)
        {
            (sender as TextBox).Text = "";
        }

        private void formLoad(Object s, EventArgs e)
        {
            this.Show();
            initializeComponents();
            versus.Focus();

        }

        private void submitClick(object sender, EventArgs e)
        {
            //checking if the user put the teams in or just leave 'em be
            if (String.Equals(teamPro.Text, "Team 1") || 
                String.Equals(teamCon.Text, "Team 2")) 
            {
                MessageBox.Show("You didn't specify the one or both of the Teams!\nCould you do that for me?", "Ooooooops!"); 
                return; 
            }

            //checking if the user put the date in or just leave it be
            if (String.Equals(yearStrt.Text, "Year") ||
                String.Equals(monthStrt.Text, "Month") ||
                String.Equals(dayStrt.Text, "Day"))
            {
                MessageBox.Show("You didn't specify correctly the date.\nCould you do that for me?", "Ooooooops!");
                return;
            }

            //checkig if the league field is empty
            if (String.Equals(league.Text, ""))
            {
                MessageBox.Show("Come on, we both know you missed the Leage.\nJump right back, and correct it!", "Ooooooops!");
                return;
            }

            //checking if the user chose the best of number
            if (bestOf.SelectedIndex == 0)
            {
                MessageBox.Show("What?!\nYou do not want me to remember the max amount of rounds?\nI'm sure it was just a misclick.\nGo, correct it!", "Ooooooops!");
                return;
            }

            //checking if the user gave the amont of money that he/she bet on the match
            if (String.Equals(betAmnt.Text, "####"))
            {
                MessageBox.Show("You think I'm a fool that I will not see the lack of the money?\nGo give me some money to play with!", "Ooooooops!");
                return;
            }
            
            //checking the team favorized
            if (betTeam.SelectedIndex == 0)
            {
                MessageBox.Show("You filthy little bastard, you didn't chose a favorized team!\nI can't push you back in time.\nHowever, I can give you another chance.", "Ooooooops!");
                return;
            }

            //asking the user if the default is acceptable
            if (String.Equals(hourStrt.Text, "00"))
            {
                if (MessageBox.Show("The Clock is set to 00:00 default values.\nAre you sure you do not want to change them?", "Clocky Problems", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            //if blank making sure it is intended
            if (String.Equals(hltvLnk.Text, "HLTV.org Link") || String.Equals(hltvLnk.Text, ""))
            {
                if (MessageBox.Show("The HLTV.org link was left blank.\nAre you sure you did that on purpose?", "Hltv link left blank", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            //csgolounge left blank
            if (String.Equals(csgoLnk.Text, "Counter Strike: Global Offensive Lounge Link") || String.Equals(csgoLnk.Text, ""))
            {
                if (MessageBox.Show("The CS:GO Lounge link was left blank.\nAre you sure you did that on purpose?", "Lounge link left blank", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            connString.AttachDBFilename = "C:\\Users\\wolfdhallg\\Documents\\Visual Studio 2013\\Projects\\tryouts1.1\\tryouts1.1\\Database1.mdf";
            connString.DataSource = "(LocalDB)\\v11.0";
            connString.IntegratedSecurity = true;

            SqlConnection conn = new SqlConnection(connString.ToString());


            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Teams";
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            int i = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    i++;
                }
            }
            reader.Close();

            reader = cmd.ExecuteReader();

            int[] _IDs = new int[i];
            string[] _Names = new string[i];
            bool[] thereIs = new bool[2];

            i = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _IDs[i] = (int)reader.GetValue(reader.GetOrdinal("ID"));
                    _Names[i] = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                    i++;
                }
            }

            reader.Close();

            for (int j = 0; j < _IDs.Length; j++)
            {
                if (teamPro.Text == _Names[j].ToString())
                {
                    thereIs[0] = true;
                }
            }

            if (!thereIs[0])
            {
                MessageBox.Show("Team 1 is not in the Database. Please Check the spelling.");
                return;
            }

            for (int j = 0; j < _IDs.Length; j++)
            {
                if (teamCon.Text == _Names[j].ToString())
                {
                    thereIs[1] = true;
                }
            }

            if (!thereIs[1])
            {
                MessageBox.Show("Team 2 is not in the Database. Please Check the spelling.");
                return;
            }

            cmd.CommandText = "INSERT Matches (t1, t2, Date, League, bo, fav, bet, lan, hltv, csgol) VALUES (";

            for (int j = 0; j < _IDs.Length; j++)
            {
                if (String.Equals(teamPro.Text,_Names[j]))
                {
                    cmd.CommandText += _IDs[j].ToString();
                }
            }

            cmd.CommandText += ",";

            for (int j = 0; j < _IDs.Length; j++)
            {
                if (String.Equals(teamCon.Text,_Names[j]))
                {
                    cmd.CommandText += _IDs[j].ToString();
                }
            }

            cmd.CommandText += ",'";
            cmd.CommandText += yearStrt.Text + "_" + monthStrt.Text + "_" + dayStrt.Text + "_" + hourStrt.Text + ":" + minuteStrt.Text + "','";
            cmd.CommandText += league.Text + "',";
            cmd.CommandText += bestOf.SelectedItem.ToString() + ",";

            for (int j = 0; j < _IDs.Length; j++)
            {
                if (String.Equals(betTeam.SelectedItem.ToString(),_Names[j]))
                {
                    cmd.CommandText += _IDs[j].ToString();
                }
            }

            cmd.CommandText += "," + betAmnt.Text.ToString();
            cmd.CommandText += "," + ((lan.Checked) ? "1" : "0");
            cmd.CommandText += ",'" + hltvLnk.Text;
            cmd.CommandText += "','" + csgoLnk.Text + "')";

            cmd.ExecuteNonQuery();
            conn.Close();
            this.Close();
        }

        private void betTeamGotFocus(Object s, EventArgs e)
        {
            betTeam.Items[1] = teamPro.Text;
            betTeam.Items[2] = teamCon.Text;
        }

        private void initializeComponents()
        {
            #region VARIABLE INITIALIZATION
            teamPro = new TextBox();
            teamCon = new TextBox();
            yearStrt = new TextBox();
            monthStrt = new TextBox();
            dayStrt = new TextBox();
            hourStrt = new TextBox();
            minuteStrt = new TextBox();
            league = new TextBox();
            bestOf = new ComboBox();
            csgoLnk = new TextBox();
            hltvLnk = new TextBox();
            betAmnt = new TextBox();
            betTeam = new ComboBox();
            notes = new TextBox();
            starts = new Label();
            versus = new Label();
            doubleDot = new Label();
            leagueLbl = new Label();
            bestOfLbl = new Label();
            csgolounge = new Label();
            hltvLbl = new Label();
            iBet = new Label();
            on = new Label();
            teamProPic = new Panel();
            teamConPic = new Panel();
            submit = new Button();
            lan = new CheckBox();
            lanLbl = new Label();
            wOutQ = new CheckBox();
            #endregion

            this.Text = "Add new Match";
            this.Height = 400;
            this.Width = 400;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            #region ControlSetups

            teamPro.Text = "Team 1";
            teamPro.Parent = this;
            teamPro.Top = 12 + 50 / 2 - teamPro.Height / 2;
            teamPro.Left = 12;
            teamPro.Click += new EventHandler(textBoxClick);

            teamCon.Text = "Team 2";
            teamCon.Parent = this;
            teamCon.Top = 12 + 50 / 2 - teamCon.Height / 2;
            teamCon.Left = this.ClientRectangle.Right - 12 - teamCon.Width;
            teamCon.Click += new EventHandler(textBoxClick);

            teamProPic.Width = 50;
            teamProPic.Height = 50;
            teamProPic.BackColor = Color.DarkBlue;
            teamProPic.Parent = this;
            teamProPic.Top = 12;
            teamProPic.Left = teamPro.Right + ((teamCon.Left - teamPro.Right - teamProPic.Width) / 7);

            teamConPic.Width = 50;
            teamConPic.Height = 50;
            teamConPic.Parent = this;
            teamConPic.BackColor = Color.DarkBlue;
            teamConPic.Top = 12;
            teamConPic.Left = teamCon.Left - (teamCon.Left - teamPro.Right - teamProPic.Width) / 7 - teamConPic.Width;

            versus.Text = "VS";
            versus.Height = 23;
            versus.Width = 25;
            versus.Top = teamPro.Top + 3;
            versus.Left = this.ClientRectangle.Width / 2 - 10;
            versus.Parent = this;

            starts.Text = "Starts at:";
            starts.Height = 23;
            starts.Width = 50;
            starts.Top = 77;
            starts.Left = 12;
            starts.Parent = this;

            yearStrt.Text = "Year";
            yearStrt.Parent = this;
            yearStrt.Width = 32;
            yearStrt.Top = 74;
            yearStrt.Left = 62;
            yearStrt.Click += new EventHandler(textBoxClick);

            monthStrt.Text = "Month";
            monthStrt.Parent = this;
            monthStrt.Width = 37;
            monthStrt.Top = 74;
            monthStrt.Left = yearStrt.Right + 5;
            monthStrt.Click += new EventHandler(textBoxClick);

            dayStrt.Text = "Day";
            dayStrt.Parent = this;
            dayStrt.Width = 32;
            dayStrt.Top = 74;
            dayStrt.Left = monthStrt.Right + 5;
            dayStrt.Click += new EventHandler(textBoxClick);

            hourStrt.Text = "00";
            hourStrt.Parent = this;
            hourStrt.Width = 23;
            hourStrt.Top = 74;
            hourStrt.Left = dayStrt.Right + 50;
            hourStrt.Click += new EventHandler(textBoxClick);

            doubleDot.Text = ":";
            doubleDot.Height = 23;
            doubleDot.Width = 12;
            doubleDot.Top = 77;
            doubleDot.Left = hourStrt.Right + 5;
            doubleDot.Parent = this;

            minuteStrt.Text = "00";
            minuteStrt.Parent = this;
            minuteStrt.Width = 23;
            minuteStrt.Top = 74;
            minuteStrt.Left = hourStrt.Right + doubleDot.Width + 7;
            minuteStrt.Click += new EventHandler(textBoxClick);

            leagueLbl.Text = "League:";
            leagueLbl.Left = 12;
            leagueLbl.Top = 109;
            leagueLbl.Height = 23;
            leagueLbl.Width = 50;
            leagueLbl.Parent = this;

            league.Text = "";
            league.Left = 62;
            league.Top = 109;
            league.Width = dayStrt.Right - yearStrt.Left;
            league.Parent = this;
            league.Click += new EventHandler(textBoxClick);

            bestOfLbl.Text = "Best of:";
            bestOfLbl.Width = 43;
            bestOfLbl.Height = 23;
            bestOfLbl.Top = 112;
            bestOfLbl.Left = hourStrt.Left - 43;
            bestOfLbl.Parent = this;

            bestOf.Text = "";
            bestOf.Items.Add("");
            bestOf.Items.Add("1");
            bestOf.Items.Add("2");
            bestOf.Items.Add("3");
            bestOf.Items.Add("5");
            bestOf.Left = hourStrt.Left;
            bestOf.Top = 109;
            bestOf.Width = 30;
            bestOf.SelectedIndex = 0;
            bestOf.Height = 23;
            bestOf.Parent = this;
            bestOf.DropDownStyle = ComboBoxStyle.DropDownList;

            csgolounge.Text = "Lounge:";
            csgolounge.Top = 147;
            csgolounge.Left = 12;
            csgolounge.Width = 50;
            csgolounge.Height = 23;
            csgolounge.Parent = this;

            csgoLnk.Text = "Counter Strike: Global Offensive Lounge Link";
            csgoLnk.Width = minuteStrt.Right - yearStrt.Left;
            csgoLnk.Top = 144;
            csgoLnk.Left = yearStrt.Left;
            csgoLnk.Parent = this;
            csgoLnk.Click += new EventHandler(textBoxClick);

            hltvLbl.Text = "HLTV:";
            hltvLbl.Height = 23;
            hltvLbl.Width = 43;
            hltvLbl.Top = 182;
            hltvLbl.Left = 12;
            hltvLbl.Parent = this;

            hltvLnk.Text = "HLTV.org Link";
            hltvLnk.Width = csgoLnk.Width;
            hltvLnk.Left = yearStrt.Left;
            hltvLnk.Top = 179;
            hltvLnk.Parent = this;
            hltvLnk.Click += new EventHandler(textBoxClick);

            iBet.Text = "I bet:    $";
            iBet.Width = 50;
            iBet.Height = 23;
            iBet.Top = 217;
            iBet.Left = 12;
            iBet.Parent = this;

            betAmnt.Text = "####";
            betAmnt.Width = 50;
            betAmnt.Top = 214;
            betAmnt.Left = yearStrt.Left;
            betAmnt.Parent = this;
            betAmnt.Click += new EventHandler(textBoxClick);

            on.Text = "on";
            on.Width = 23;
            on.Height = 23;
            on.Top = 217;
            on.Left = betAmnt.Right + 5;
            on.Parent = this;

            betTeam.Items.Add("");
            betTeam.Items.Add("");
            betTeam.Items.Add("");
            betTeam.SelectedIndex = 0;
            betTeam.Width = 50;
            betTeam.Top = 214;
            betTeam.Left = on.Right;
            betTeam.Parent = this;
            betTeam.GotFocus += new EventHandler(betTeamGotFocus);
            betTeam.DropDownStyle = ComboBoxStyle.DropDownList;

            lanLbl.Text = "LAN:";
            lanLbl.Height = 23;
            lanLbl.Width = 31;
            lanLbl.Top = 217;
            lanLbl.Left = betTeam.Right + 12;
            lanLbl.Parent = this;

            lan.Top = 214;
            lan.Left = lanLbl.Right;
            lan.Width = 23;
            lan.Height = 23;
            lan.Parent = this;

            notes.Text = "Please, feel free to take some notes";
            notes.Multiline = true;
            notes.ScrollBars = ScrollBars.Vertical;
            notes.Width = this.ClientRectangle.Width - 24;
            notes.Height = 100;
            notes.Top = 249;
            notes.Left = 12;
            notes.Parent = this;
            notes.Click += new EventHandler(textBoxClick);

            submit.Text = "Submit";
            submit.Width = this.ClientRectangle.Width - 24 - minuteStrt.Right;
            submit.Height = notes.Top - 12 - yearStrt.Top;
            submit.Top = yearStrt.Top;
            submit.Left = minuteStrt.Right + 12;
            submit.Parent = this;
            submit.Click += new EventHandler(submitClick);


            #endregion
        }
    }
}
