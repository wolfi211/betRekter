using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace tryouts1._1
{
    public partial class PlayersForm : Form
    {
        private Panel[] players;
        private Label[] plyrNms;
        private Button[] hltvBtn;
        private Button[] faceBtn;
        private Button[] twitBtn;
        private Button newPlyr;
        private NewPlayerForm npf;
        private Button[] delete;
        private Button refresh;

        public PlayersForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(formLoad);
        }

        private void formLoad(Object s, EventArgs e)
        {
            init();
            initPlayers();
        }

        private void init()
        {
            this.Text = "Players";
            this.Width = 335;
            this.Height = 837;
            this.Top = 0;
            this.Left = 1200;
            this.AutoScroll = true;

            newPlyr = new Button();
            refresh = new Button();

            refresh.Text = "Refresh";
            refresh.Top = 47;
            refresh.Left = 12;
            refresh.Width = this.ClientRectangle.Width - 32;
            refresh.Parent = this;
            refresh.Click += new EventHandler(refreshPanels);
            
            newPlyr.Text = "Add a Player";
            newPlyr.Top = 12;
            newPlyr.Left = 12;
            newPlyr.Width = this.ClientRectangle.Width - 32;
            newPlyr.Parent = this;
            newPlyr.Click += new EventHandler(addClick);

        }

        private void refreshPanels(object sender, EventArgs e)
        {
            disposePanels();
            initPlayers();
        }

        private void addClick(object sender, EventArgs e)
        {
            npf = new NewPlayerForm();
            npf.Show();
        }

        private void disposePanels()
        {
            foreach (Panel p in players)
            {
                p.Dispose();
            }

            foreach (Label p in plyrNms)
            {
                p.Dispose();
            }

            foreach (Button p in hltvBtn)
            {
                p.Dispose();
            }

            foreach (Button p in faceBtn)
            {
                p.Dispose();
            }

            foreach (Button p in twitBtn)
            {
                p.Dispose();
            }

            foreach (Button p in delete)
            {
                p.Dispose();
            }
        }

        private void initPlayers()
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            connString.AttachDBFilename = "C:\\Users\\wolfdhallg\\Documents\\Visual Studio 2013\\Projects\\tryouts1.1\\tryouts1.1\\Database1.mdf";
            connString.DataSource = "(LocalDB)\\v11.0";
            connString.IntegratedSecurity = true;
            
            SqlConnection conn = new SqlConnection(connString.ToString());


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
            else return;

            reader.Close();

            players = new Panel[i];
            plyrNms = new Label[i];
            twitBtn = new Button[i];
            faceBtn = new Button[i];
            hltvBtn = new Button[i];
            delete = new Button[i];

            for (int j = 0; j < i; j++)
            {
                players[j] = new Panel();
                players[j].Parent = this;
                players[j].Width = 275;
                players[j].Height = 82;
                players[j].Top = 47 + 35 + j * 94;
                players[j].Left = 12;
                players[j].BackColor = Color.FromArgb(255, 216, 107);
                players[j].BorderStyle = BorderStyle.FixedSingle;

                plyrNms[j] = new Label();
                plyrNms[j].Parent = players[j];
                plyrNms[j].Top = 15;
                plyrNms[j].Left = 12;
                plyrNms[j].AutoSize = true;
                plyrNms[j].Text = "Player " + (j + 1).ToString();
                plyrNms[j].Left = players[j].Width / 2 - plyrNms[j].Width / 2;

                twitBtn[j] = new Button();
                twitBtn[j].Text = "Twitter";
                twitBtn[j].Width = (players[j].Width - 12) / 3 - 12;
                twitBtn[j].Top = 47;
                twitBtn[j].Left = 12;
                twitBtn[j].Parent = players[j];
                twitBtn[j].BackColor = this.BackColor;
                twitBtn[j].Click += new EventHandler(linkClick);

                faceBtn[j] = new Button();
                faceBtn[j].Text = "Facebook";
                faceBtn[j].Width = (players[j].Width - 12) / 3 - 12;
                faceBtn[j].Top = 47;
                faceBtn[j].Left = twitBtn[j].Right + 12;
                faceBtn[j].Parent = players[j];
                faceBtn[j].BackColor = this.BackColor;
                faceBtn[j].Click += new EventHandler(linkClick);

                hltvBtn[j] = new Button();
                hltvBtn[j].Text = "HLTV.org";
                hltvBtn[j].Width = (players[j].Width - 12) / 3 - 12;
                hltvBtn[j].Top = 47;
                hltvBtn[j].Left = faceBtn[j].Right + 12;
                hltvBtn[j].Parent = players[j];
                hltvBtn[j].BackColor = this.BackColor;
                hltvBtn[j].Click += new EventHandler(linkClick);

                delete[j] = new Button();
                delete[j].Width = 33;
                delete[j].Height = 23;
                delete[j].Top = 12;
                delete[j].Left = 12;
                delete[j].Parent = players[j];
                delete[j].BackColor = this.BackColor;
                delete[j].Click += new EventHandler(deleteDoubleClick);
            }
            reader.Close();

            reader = cmd.ExecuteReader();

            i = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    plyrNms[i].Text = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                    twitBtn[i].Text += "     _" + reader.GetValue(reader.GetOrdinal("Twitter")).ToString();
                    faceBtn[i].Text += "     _" + reader.GetValue(reader.GetOrdinal("Facebook")).ToString();
                    hltvBtn[i].Text += "     _" + reader.GetValue(reader.GetOrdinal("Hltv")).ToString();
                    delete[i].Text = reader.GetValue(reader.GetOrdinal("ID")).ToString();
                    plyrNms[i].Left = players[i].Width / 2 - plyrNms[i].Width / 2;
                    i++;
                }
            }

            reader.Close();
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
            catch(Exception error)
            {
                MessageBox.Show("There is something wrong with URL you tried to open.");
            }
            
        }

        private void deleteDoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this player?", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            SqlCommand cmd = new SqlCommand();

            connString.AttachDBFilename = "C:\\Users\\wolfdhallg\\Documents\\Visual Studio 2013\\Projects\\tryouts1.1\\tryouts1.1\\Database1.mdf";
            connString.DataSource = "(LocalDB)\\v11.0";
            connString.IntegratedSecurity = true;

            SqlConnection conn = new SqlConnection(connString.ToString());


            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE Players WHERE ID =" + int.Parse(((Button)sender).Text);
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            ((Button)sender).Parent.Dispose();
        }
    }
}
