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
    public partial class NewTeamForm : Form
    {
        private ComboBox playerCount;
        private TextBox hltvLnk;
        private TextBox faceLnk;
        private TextBox twitLnk;
        private TextBox teamName;
        private TextBox[] players;
        private Button submit;
        private Label plyrLbl;
        private Label nameLbl;
        private Label hltvLbl;
        private Label faceLbl;
        private Label twitLbl;
        private Label playersLbl;

        public NewTeamForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(formLoad);
        }

        private void formLoad(Object s, EventArgs e)
        {
            init();
            submit.Focus();
        }

        private void plyrCntIndexChanged(object sender, EventArgs e)
        {
            foreach (TextBox t in players)
            {
                t.Hide();
            }

            for (int i = 0; i < (int)playerCount.SelectedItem; i++)
            {
                players[i].Show();
                this.Height = players[i].Bottom + 47;
            }
        }

        private void init()
        {
            this.Text = "Add New Team";
            this.Width = 300;
            this.Height = 300;
            this.Top = 0;
            this.Left = 0;

            playerCount = new ComboBox();
            plyrLbl = new Label();
            submit = new Button();
            players = new TextBox[25];
            nameLbl = new Label();
            teamName = new TextBox();
            hltvLbl = new Label();
            hltvLnk = new TextBox();
            faceLbl = new Label();
            twitLbl = new Label();
            faceLnk = new TextBox();
            twitLnk = new TextBox();
            playersLbl = new Label();

            playerCount.Parent = this;
            for (int i = 5; i < 26; i++) playerCount.Items.Add(i);
            playerCount.Top = 12;
            playerCount.Left = 12;
            playerCount.SelectedIndex = 0;
            playerCount.Width = 30;
            playerCount.Height = 23;
            playerCount.SelectedIndexChanged += new EventHandler(plyrCntIndexChanged);

            plyrLbl.Text = "players";
            plyrLbl.Top = 15;
            plyrLbl.Left = playerCount.Right + 5;
            plyrLbl.Height = 16;
            plyrLbl.Width = 50;
            plyrLbl.Parent = this;

            submit.Text = "Submit";
            submit.Top = 12;
            submit.Left = plyrLbl.Right;
            submit.Width = this.ClientRectangle.Width - 12 - plyrLbl.Right;
            submit.Parent = this;
            submit.Click += new EventHandler(submitClick);

            nameLbl.Text = "Team name:";
            nameLbl.AutoSize = true;
            nameLbl.Top = 50;
            nameLbl.Left = 12;
            nameLbl.Height = 16;
            nameLbl.Parent = this;

            teamName.Width = 100;
            teamName.Top = 47;
            teamName.Left = nameLbl.Right + 5;
            teamName.Parent = this;

            hltvLbl.AutoSize = true;
            hltvLbl.Text = "HLTV:";
            hltvLbl.Left = 12;
            hltvLbl.Top = 85;
            hltvLbl.Parent = this;

            hltvLnk.Width = this.ClientRectangle.Width - hltvLbl.Right - 5 - 12;
            hltvLnk.Top = 82;
            hltvLnk.Left = hltvLbl.Right + 5;
            hltvLnk.Parent = this;

            twitLbl.Text = "Twitter:";
            twitLbl.AutoSize = true;
            twitLbl.Top = 120;
            twitLbl.Left = 12;
            twitLbl.Parent = this;

            twitLnk.Width = this.ClientRectangle.Width - twitLbl.Right - 5 - 12;
            twitLnk.Top = 117;
            twitLnk.Left = twitLbl.Right + 5;
            twitLnk.Parent = this;

            faceLbl.Text = "FaceBook:";
            faceLbl.AutoSize = true;
            faceLbl.Top = 155;
            faceLbl.Left = 12;
            faceLbl.Parent = this;

            faceLnk.Width = this.ClientRectangle.Width - faceLbl.Right - 5 - 12;
            faceLnk.Top = 152;
            faceLnk.Left = faceLbl.Right + 5;
            faceLnk.Parent = this;

            playersLbl.Text = "Players";
            playersLbl.AutoSize = true;
            playersLbl.Top = 190;
            playersLbl.Left = this.ClientRectangle.Width / 2 - playersLbl.Width / 2 + 32;
            playersLbl.Parent = this;

            for (int i = 0; i < 25; i++)
            {
                players[i] = new TextBox();
                players[i].Width = (this.ClientRectangle.Width - 12) / 3 - 12;
                players[i].Left = 12 + (i % 3) * (12 + players[i].Width);
                players[i].Top = 222 + (i / 3) * 35;
                players[i].Parent = this;
            }

            foreach (TextBox t in players)
            {
                t.Hide();
            }

            for (int i = 0; i < (int)playerCount.SelectedItem; i++)
            {
                players[i].Show();
                this.Height = players[i].Bottom + 47;
            }
        }

        private void submitClick(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            SqlDataReader reader;

            connString.AttachDBFilename = "C:\\Users\\wolfdhallg\\Documents\\Visual Studio 2013\\Projects\\tryouts1.1\\tryouts1.1\\Database1.mdf";
            connString.DataSource = "(LocalDB)\\v11.0";
            connString.IntegratedSecurity = true;

            SqlConnection conn = new SqlConnection(connString.ToString());

            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = conn;
            cmd2.CommandText = "INSERT Teams (Name, Twitter, Facebook, Hltv";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Players";
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
            bool[] thereIs = new bool[(int)playerCount.SelectedItem];

            i = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _IDs[i] = (int) reader.GetValue(reader.GetOrdinal("ID"));
                    _Names[i] = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                    i++;
                }
            }

            reader.Close();

            for (int j = 0; j < (int) playerCount.SelectedItem; j++)
            {
                for (int k = 0; k < _IDs.Length; k++)
                {
                    if (String.Equals(players[j].Text, _Names[k]))
                    {
                        thereIs[j] = true;
                    }
                }
            }

            for (int j = 0; j < thereIs.Length; j++)
            {
                if (!thereIs[j])
                {
                    MessageBox.Show("One or more of the Players are not in the database.");
                    return;
                }
            }

            for (int j = 0; j < (int)playerCount.SelectedItem; j++)
            {
                cmd2.CommandText += ",p" + (j+1).ToString();
            }

            cmd2.CommandText += ") VALUES ('" + teamName.Text + "','" + twitLnk.Text + "','" + faceLnk.Text + "','" + hltvLnk.Text + "'";

            for (int j = 0; j < (int)playerCount.SelectedItem; j++)
            {
                for (int k = 0; k < _IDs.Length; k++)
                {
                    if(String.Equals(players[j].Text, _Names[k]))
                    cmd2.CommandText += "," + _IDs[k].ToString();
                }
                    
            }
            cmd2.CommandText += ")";

            MessageBox.Show(cmd2.CommandText.ToString());

            cmd2.ExecuteNonQuery();
            conn.Close();
            this.Close();
        }
    }
}
