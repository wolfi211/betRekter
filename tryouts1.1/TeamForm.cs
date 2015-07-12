using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryouts1._1
{
    public partial class TeamForm : Form
    {
        private Panel teamList;
        private Panel details;
        private Button addNewTeam;
        private NewTeamForm ntf;
        private Panel[] teams;
        private Label[] teamNames;
        private Panel[] teamDetails;
        private Button[] twit;
        private Button[] face;
        private Button[] hltv;
        private Panel[] players;
        private Label[] playerNames;
        private Button[] playerTwit;
        private Button[] playerFace;
        private Button[] playerHltv;
        private int[,] plyrs;


        public TeamForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(formLoad);
        }

        private void formLoad(Object s, EventArgs e)
        {
            init();
            initSql();
        }

        private void clickAdd(Object s, EventArgs e)
        {
            ntf = new NewTeamForm();
            ntf.Show();
        }

        private void init()
        {
            this.Width = 400;
            this.Height = 837;
            this.Top = 0;
            this.Left = 0;
            this.Text = "Teams";

            teamList = new Panel();
            details = new Panel();
            addNewTeam = new Button();

            teamList.Width = this.ClientRectangle.Width / 2;
            teamList.Height = this.ClientRectangle.Height - 70;
            teamList.Top = 70;
            teamList.Left = 0;
            teamList.Parent = this;
            teamList.AutoScroll = true;

            details.Width = this.ClientRectangle.Width / 2;
            details.Height = this.ClientRectangle.Height - 70;
            details.Top = 70;
            details.Left = teamList.Right;
            details.Parent = this;
            details.AutoScroll = true;
            details.BorderStyle = BorderStyle.FixedSingle;

            addNewTeam.Text = "Add New Team";
            addNewTeam.Width = this.ClientRectangle.Width - 24;
            addNewTeam.Height = 46;
            addNewTeam.Top = 12;
            addNewTeam.Left = this.ClientRectangle.Width / 2 - addNewTeam.Width / 2;
            addNewTeam.Parent = this;
            addNewTeam.BringToFront();
            addNewTeam.Click += new EventHandler(clickAdd);
        }

        private void initSql()
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            connString.AttachDBFilename = "|DataDirectory|\\Database1.mdf";
            connString.DataSource = "(LocalDB)\\v11.0";
            connString.IntegratedSecurity = true;

            SqlConnection conn = new SqlConnection(connString.ToString());


            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Teams";
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            int cnt = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cnt++;
                }
            }
            else return;

            reader.Close();
            
            teams = new Panel[cnt];
            teamNames = new Label[cnt];
            teamDetails = new Panel[cnt];
            twit = new Button[cnt];
            face = new Button[cnt];
            hltv = new Button[cnt];
            plyrs = new int[cnt, 25];

            for (int i = 0; i < cnt; i++)
            {
                teams[i] = new Panel();
                teams[i].Parent = teamList;
                teams[i].Width = teamList.Width - 24;
                teams[i].Height = 47;
                teams[i].Left = 12;
                teams[i].Top = 12 + i * (47 + 12);
                teams[i].Click += new EventHandler(teamClick);
                teams[i].BackColor = Color.FromArgb(50,255, 216, 107);
                teams[i].BorderStyle = BorderStyle.FixedSingle;

                teamNames[i] = new Label();
                teamNames[i].Text = "Team " + (i + 1).ToString();
                teamNames[i].AutoSize = true;
                teamNames[i].Top = 15;
                teamNames[i].Left = teams[i].Width / 2 - teamNames[i].Width / 2;
                teamNames[i].Parent = teams[i];

                teamDetails[i] = new Panel();
                teamDetails[i].Width = details.Width;
                teamDetails[i].Height = details.Height;
                teamDetails[i].Top = details.Top;
                teamDetails[i].Left = details.Left;
                teamDetails[i].Parent = this;
                teamDetails[i].BorderStyle = BorderStyle.FixedSingle;

                twit[i] = new Button();
                twit[i].Text = "                    Twitter                    ";
                twit[i].Parent = teamDetails[i];
                twit[i].Width = teamDetails[i].Width - 24;
                twit[i].Top = 12;
                twit[i].Left = 12;

                face[i] = new Button();
                face[i].Text = "                 Facebook                       ";
                face[i].Parent = teamDetails[i];
                face[i].Width = teamDetails[i].Width - 24;
                face[i].Top = 47;
                face[i].Left = 12;

                hltv[i] = new Button();
                hltv[i].Text = "                  Hltv.org                      ";
                hltv[i].Parent = teamDetails[i];
                hltv[i].Width = teamDetails[i].Width - 24;
                hltv[i].Top = 82;
                hltv[i].Left = 12;
            }


            reader = cmd.ExecuteReader();

            cnt = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < 25; i++)
                    {
                        if (!String.IsNullOrWhiteSpace(reader.GetValue(reader.GetOrdinal("p" + (i + 1).ToString())).ToString()))
                        {
                            plyrs[cnt, i] = (int) reader.GetValue(reader.GetOrdinal("p" + (i + 1).ToString()));
                        }
                        else
                        {
                            plyrs[cnt, i] = 0;
                        }
                    }
                    twit[cnt].Text += "_" + reader.GetValue(reader.GetOrdinal("Twitter")).ToString();
                    face[cnt].Text += "_" + reader.GetValue(reader.GetOrdinal("Facebook")).ToString();
                    hltv[cnt].Text += "_" + reader.GetValue(reader.GetOrdinal("Hltv")).ToString();
                    teamNames[cnt].Text = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                    cnt++;
                }
            }
            reader.Close();

            int cnt2 = 0;

            for (int i = 0; i < plyrs.GetLength(0); i++)
            {
                for (int j = 0; j < plyrs.GetLength(1); j++)
                {
                    if (plyrs[i, j] != 0)
                    {
                        cnt2++;
                    }
                }
            }

            players = new Panel[cnt2];
            playerFace = new Button[cnt2];
            playerHltv = new Button[cnt2];
            playerTwit = new Button[cnt2];
            playerNames = new Label[cnt2];

            cnt2 = 0;

            for (int i = 0; i < plyrs.GetLength(0); i++)
            {
                for (int j = 0; j < plyrs.GetLength(1); j++)
                {
                    if (plyrs[i, j] != 0)
                    {

                        players[cnt2] = new Panel();
                        players[cnt2].Width = teamDetails[i].ClientRectangle.Width - 24;
                        players[cnt2].Height = 82;
                        players[cnt2].Top = 117 + j * 94;
                        players[cnt2].Left = 12;
                        players[cnt2].BackColor = Color.FromArgb(255, 216, 107);
                        players[cnt2].BorderStyle = BorderStyle.FixedSingle;
                        players[cnt2].Parent = teamDetails[i];

                        playerNames[cnt2] = new Label();
                        playerNames[cnt2].Parent = players[cnt2];
                        playerNames[cnt2].Top = 15;
                        playerNames[cnt2].Left = 12;
                        playerNames[cnt2].AutoSize = true;
                        playerNames[cnt2].Text = "Player " + (j + 1).ToString();
                        playerNames[cnt2].Left = players[j].Width / 2 - playerNames[cnt2].Width / 2;
                        
                        playerTwit[cnt2] = new Button();
                        playerTwit[cnt2].Text = "Twitter";
                        playerTwit[cnt2].Width = (players[cnt2].Width - 12) / 3 - 12;
                        playerTwit[cnt2].Top = 47;
                        playerTwit[cnt2].Left = 12;
                        playerTwit[cnt2].Parent = players[cnt2];
                        playerTwit[cnt2].BackColor = this.BackColor;
                        playerTwit[cnt2].Click += new EventHandler(linkClick);

                        playerFace[cnt2] = new Button();
                        playerFace[cnt2].Text = "Facebook";
                        playerFace[cnt2].Width = (players[cnt2].Width - 12) / 3 - 12;
                        playerFace[cnt2].Top = 47;
                        playerFace[cnt2].Left = playerTwit[cnt2].Right + 12;
                        playerFace[cnt2].Parent = players[cnt2];
                        playerFace[cnt2].BackColor = this.BackColor;
                        playerFace[cnt2].Click += new EventHandler(linkClick);

                        playerHltv[cnt2] = new Button();
                        playerHltv[cnt2].Text = "HLTV.org";
                        playerHltv[cnt2].Width = (players[cnt2].Width - 12) / 3 - 12;
                        playerHltv[cnt2].Top = 47;
                        playerHltv[cnt2].Left = playerFace[cnt2].Right + 12;
                        playerHltv[cnt2].Parent = players[cnt2];
                        playerHltv[cnt2].BackColor = this.BackColor;
                        playerHltv[cnt2].Click += new EventHandler(linkClick);
                        
                        cnt2++;
                    }
                }
            }

            cnt2 = 0;
            cmd.CommandText = "SELECT * FROM Players";

            for (int j = 0; j < plyrs.GetLength(0); j++)
            {
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            if (((int)reader.GetValue(reader.GetOrdinal("ID"))) == plyrs[j, i])
                            {
                                playerNames[cnt2].Text = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                                playerTwit[cnt2].Text += "              _" + reader.GetValue(reader.GetOrdinal("Twitter")).ToString();
                                playerFace[cnt2].Text += "              _" + reader.GetValue(reader.GetOrdinal("Facebook")).ToString();
                                playerHltv[cnt2].Text += "              _" + reader.GetValue(reader.GetOrdinal("Hltv")).ToString();
                                cnt2++;
                            }
                        }
                    }
                }

                reader.Close();
            }
            conn.Close();

        }

        private void linkClick(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string s = b.Text;
            bool _IS = false;
            string link = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (_IS)
                {
                    link += s[i];
                }
                if (s[i] == '_')
                {
                    _IS = true;
                }
            }

            try
            {
                System.Diagnostics.Process.Start(link);
            }
            catch (Exception error)
            {
                MessageBox.Show("There is something wrong with URL you tried to open.");
            }
        }

        private void teamClick(object sender, EventArgs e)
        {
            Panel p = sender as Panel;
            int i = Array.IndexOf(teams, p);
            teamDetails[i].BringToFront();
        }
    }
}
