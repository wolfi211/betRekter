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
    class Details
    {
        Form parentForm;
        Panel cover;
        Panel[] matches;
        Label[] lblDtls;
        Label[] lblCsgo;
        Label[] lblHltv;
        Label[] lblLeag;
        Label[] lblVers;
        Label[] lblBest;
        Label[] lblTmPr;
        Label[] lblTmCn;
        Label[] lblTm1P;
        Label[] lblTm2P;
        Label[] lblAmnt;
        Label[] lblWinn;
        Label[] lblStrt;
        Panel[] pnlTmPr;
        Panel[] pnlTmCn;
        TextBox[] txtNots;
        Panel[] pnlTmNfo;


        public Details(Form f)
        {
            parentForm = f;
            cover = f.GetChildAtPoint(new Point(parentForm.Width - 20, 20)) as Panel;
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
            else return;

            matches = new Panel[cnt];
            lblDtls = new Label[cnt];
            lblCsgo = new Label[cnt];
            lblHltv = new Label[cnt];
            lblLeag = new Label[cnt];
            lblVers = new Label[cnt];
            lblBest = new Label[cnt];
            lblTmPr = new Label[cnt];
            lblTmCn = new Label[cnt];
            lblTm1P = new Label[cnt];
            lblTm2P = new Label[cnt];
            lblAmnt = new Label[cnt];
            lblWinn = new Label[cnt];
            lblStrt = new Label[cnt];
            pnlTmPr = new Panel[cnt];
            pnlTmCn = new Panel[cnt];
            txtNots = new TextBox[cnt];
            pnlTmNfo = new Panel[cnt];

            for (int i = 0; i < matches.Length; i++)
            {
                matches[i] = new Panel();
                matches[i].Width = cover.Width;
                matches[i].Height = cover.Width;
                matches[i].Top = cover.Top;
                matches[i].Left = cover.Left;
                matches[i].Parent = parentForm;
                matches[i].BackColor = cover.BackColor;

                cover.BringToFront();

                lblDtls[i] = new Label();
                lblDtls[i].Text = "Details";
                lblDtls[i].ForeColor = Color.FromArgb(49, 35, 176);
                lblDtls[i].Parent = matches[i];
                lblDtls[i].Height = 23;
                lblDtls[i].Width = 40;
                lblDtls[i].Top = 12;
                lblDtls[i].Left = 12;

                lblHltv[i] = new Label();
                lblHltv[i].Text = "Hltv.Org";
                lblHltv[i].Parent = matches[i];
                lblHltv[i].Height = 23;
                lblHltv[i].Width = 40;
                lblHltv[i].Top = 12;
                lblHltv[i].Left = matches[i].Width / 2 - lblHltv[i].Width / 2;

            }
        }
    }
}
