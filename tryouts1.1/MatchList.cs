using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;

namespace tryouts1._1
{
    class MatchList
    {
        private Panel[] matches;
        private Panel parentPanel;
        private Form parentForm;
        private Label[] t1Lbl;
        private Label[] t2Lbl;
        private Label[] versus;
        private Label[] dateLbl;
        private Button[] delete;
        private int[] matchID;

        public MatchList(Panel p, Form f)
        {
            this.parentForm = f;
            this.parentPanel = p;
            init();
        }

        private void init()
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            connString.AttachDBFilename = "C:\\Users\\wolfdhallg\\Documents\\Visual Studio 2013\\Projects\\tryouts1.1\\tryouts1.1\\Database1.mdf";
            connString.DataSource = "(LocalDB)\\v11.0";
            connString.IntegratedSecurity = true;

            SqlConnection conn = new SqlConnection(connString.ToString());


            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Matches";
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
            else 
                return;
            reader.Close();

            matches = new Panel[cnt];
            t1Lbl = new Label[cnt];
            t2Lbl = new Label[cnt];
            versus = new Label[cnt];
            dateLbl = new Label[cnt];
            delete = new Button[cnt];
            matchID = new int[cnt];
            
            int[] t1 = new int[cnt];
            int[] t2 = new int[cnt];
            string[] date = new string[cnt];

            reader = cmd.ExecuteReader();

            cnt = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    matchID[cnt] = int.Parse(reader.GetValue(reader.GetOrdinal("ID")).ToString());
                    t1[cnt] = int.Parse(reader.GetValue(reader.GetOrdinal("t1")).ToString());
                    t2[cnt] = int.Parse(reader.GetValue(reader.GetOrdinal("t2")).ToString());
                    date[cnt] = reader.GetValue(reader.GetOrdinal("date")).ToString();
                    cnt++;
                }
            }

            reader.Close();

            for (int i = 0; i < matches.Length; i++)
            {
                matches[i] = new Panel();
                matches[i].Width = parentPanel.Width - 40;
                matches[i].Height = 58 + 35;
                matches[i].Top = 12 + i * (matches[i].Height + 12);
                matches[i].Left = 12;
                matches[i].Parent = parentPanel;
                matches[i].BackColor = Color.FromArgb(255, 216, 107);
                matches[i].BorderStyle = BorderStyle.FixedSingle;
                matches[i].Click += new EventHandler(matchClick);

                t1Lbl[i] = new Label();
                t1Lbl[i].Text = "Team 1";
                t1Lbl[i].Height = 16;
                t1Lbl[i].AutoSize = true;
                t1Lbl[i].Top = 3;
                t1Lbl[i].Left = 3;
                t1Lbl[i].Parent = matches[i];

                t2Lbl[i] = new Label();
                t2Lbl[i].Text = "Team 2";
                t2Lbl[i].Height = 16;
                t2Lbl[i].AutoSize = true;
                t2Lbl[i].Width = 50;
                t2Lbl[i].Top = 3;
                t2Lbl[i].Left = matches[i].Width - 3 - t2Lbl[i].Width;
                t2Lbl[i].Parent = matches[i];

                versus[i] = new Label();
                versus[i].Text = "VS";
                versus[i].Height = 16;
                versus[i].AutoSize = true;
                versus[i].Width = 23;
                versus[i].Top = 3;
                versus[i].Left = matches[i].Width / 2 - versus[i].Width / 2;
                versus[i].Parent = matches[i];

                dateLbl[i] = new Label();
                dateLbl[i].Text = date[i];
                dateLbl[i].Height = 16;
                dateLbl[i].AutoSize = true;
                dateLbl[i].Width = 100;
                dateLbl[i].Top = 25;
                dateLbl[i].Left = matches[i].Width / 2 - dateLbl[i].Width / 2;
                dateLbl[i].Parent = matches[i];

                delete[i] = new Button();
                delete[i].Text = "Delete";
                delete[i].Top = dateLbl[i].Bottom + 3;
                delete[i].Width = 46;
                delete[i].Left = matches[i].Width - 3 - delete[i].Width;
                delete[i].Parent = matches[i];
                delete[i].Text += "   _" + matchID[i].ToString();
                delete[i].BackColor = parentForm.BackColor;
                delete[i].Click += new EventHandler(delClick);

                matches[i].Height = delete[i].Bottom + 3;
            }

            cmd.CommandText = "SELECT * FROM Teams";

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < t1.Length; i++)
                    {
                        if (t1[i] == int.Parse(reader.GetValue(reader.GetOrdinal("ID")).ToString()))
                        {
                            t1Lbl[i].Text = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                        }
                    }
                }
            }

            reader.Close();

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < t2.Length; i++)
                    {
                        if (t2[i] == int.Parse(reader.GetValue(reader.GetOrdinal("ID")).ToString()))
                        {
                            t2Lbl[i].Text = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                        }
                    }
                }
            }

            reader.Close();

            conn.Close();
        }

        private void delClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this match?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Button del = sender as Button;
                int i = Array.IndexOf(delete, del);

                SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
                SqlCommand cmd = new SqlCommand();

                connString.AttachDBFilename = "C:\\Users\\wolfdhallg\\Documents\\Visual Studio 2013\\Projects\\tryouts1.1\\tryouts1.1\\Database1.mdf";
                connString.DataSource = "(LocalDB)\\v11.0";
                connString.IntegratedSecurity = true;

                SqlConnection conn = new SqlConnection(connString.ToString());


                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE Matches WHERE ID = " + matchID[i].ToString();
                cmd.Connection = conn;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                refreshPanel();
            }
        }

        private void matchClick(object sender, EventArgs e)
        {

        }

        private void refreshPanel()
        {
            for (int i = 0; i < matches.Length; i++)
            {
                matches[i].Dispose();
                t1Lbl[i].Dispose();
                t2Lbl[i].Dispose();
                versus[i].Dispose();
                dateLbl[i].Dispose();
                delete[i].Dispose();
            }

            init();
        }

        public void onResize()
        {
            for (int i = 0; i < matches.Length; i++)
            {
                matches[i].Width = parentPanel.Width - 40;
                matches[i].Top = 12 + i * (matches[i].Height + 12);
                matches[i].Left = 12;
            }
        }
    }
}
