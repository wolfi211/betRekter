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
    public partial class NewPlayerForm : Form
    {
        private TextBox name;
        private TextBox twit;
        private TextBox face;
        private TextBox hltv;
        private Label nameLbl;
        private Label twitLbl;
        private Label faceLbl;
        private Label hltvLbl;
        private Button submit;

        public NewPlayerForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(formLoad);
        }

        private void formLoad(object sender, EventArgs e)
        {
            init();
        }

        private void init()
        {
            this.Text = "Add a Player";

            name = new TextBox();
            twit = new TextBox();
            face = new TextBox();
            hltv = new TextBox();
            nameLbl = new Label();
            twitLbl = new Label();
            faceLbl = new Label();
            hltvLbl = new Label();
            submit = new Button();

            nameLbl.AutoSize = true;
            nameLbl.Text = "Player Name:";
            nameLbl.Left = 12;
            nameLbl.Top = 15;
            nameLbl.Parent = this;

            twitLbl.AutoSize = true;
            twitLbl.Text = "Twitter:";
            twitLbl.Top = 50;
            twitLbl.Left = 12;
            twitLbl.Parent = this;

            faceLbl.AutoSize = true;
            faceLbl.Text = "FaceBook:";
            faceLbl.Top = 85;
            faceLbl.Left = 12;
            faceLbl.Parent = this;

            hltvLbl.AutoSize = true;
            hltvLbl.Text = "HLTV.org:";
            hltvLbl.Top = 120;
            hltvLbl.Left = 12;
            hltvLbl.Parent = this;

            submit.Text = "Submit";
            submit.Width = this.ClientRectangle.Width - 24;
            submit.Height = 57;
            submit.Top = 152;
            submit.Left = 12;
            submit.Parent = this;
            submit.Click += new EventHandler(submitClick);

            name.Width = this.ClientRectangle.Width - nameLbl.Right - 12 - 5;
            name.Top = 12;
            name.Left = nameLbl.Right + 5;
            name.Parent = this;

            twit.Width = this.ClientRectangle.Width - twitLbl.Right - 12 - 5;
            twit.Top = 47;
            twit.Left = twitLbl.Right + 5;
            twit.Parent = this;

            face.Width = this.ClientRectangle.Width - faceLbl.Right - 12 - 5;
            face.Top = 82;
            face.Left = faceLbl.Right + 5;
            face.Parent = this;

            hltv.Width = this.ClientRectangle.Width - hltvLbl.Right - 12 - 5;
            hltv.Top = 117;
            hltv.Left = hltvLbl.Right + 5;
            hltv.Parent = this;

            this.Height = submit.Bottom + 12 + 35;
        }

        private void submitClick(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            connString.AttachDBFilename = "|DataDirectory|\\Database1.mdf";
            connString.DataSource = "(LocalDB)\\v11.0";
            connString.IntegratedSecurity = true;

            SqlConnection conn = new SqlConnection(connString.ToString());

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT Players (Name, Twitter, Facebook, hltv) VALUES ('"+name.Text+"','"+twit.Text+"','"+face.Text+ "','"+hltv.Text+"')";
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            this.Close();
        }
    }
}
    