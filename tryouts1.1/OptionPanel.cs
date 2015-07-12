using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace tryouts1._1
{
    class OptionPanel
    {
        private Button players;
        private Button teams;
        private Button archive;
        private Button addMatch;
        private Button delMatch;
        private Button save;
        private Button edit;
        private Panel Profit;
        private Label title;
        private Panel parentPanel;
        private int width;
        private NewMatchForm f;
        private TeamForm f2;
        private PlayersForm pf;

        public OptionPanel(Panel p)
        {
            if (p != null)
            {
                this.parentPanel = p;
            }

            if (parentPanel != null)
            {
                title = new Label();
                title.Text = "Tools";
                title.Top = 12;
                title.Left = parentPanel.Width / 2 - 30;
                title.Height = 30;
                title.Parent = parentPanel;
                title.Font = new Font("SanSeriff", 16);
                title.ForeColor = Color.FromArgb(49, 35, 176);

                Profit = new Panel();
                Profit.Width = parentPanel.Width / 4 * 3;
                Profit.Height = 30;
                Profit.Top = 50;
                Profit.Left = parentPanel.Width / 2 - Profit.Width / 2;
                Profit.BackColor = Color.FromArgb(245, 186, 56);
                Profit.Parent = parentPanel;

                players = new Button();
                teams = new Button();
                archive = new Button();
                addMatch = new Button();
                delMatch = new Button();
                save = new Button();

                width = players.Width;

                players.Text = "Players";
                players.Top = parentPanel.Height / 2 - players.Height * 2 - players.Height / 2 - 12 * 2;
                players.Left = parentPanel.Width / 2 - players.Width / 2;
                players.Parent = parentPanel;
                players.BackColor = parentPanel.Parent.Parent.BackColor;
                players.Width = players.Width / 2 + parentPanel.Width / 2 - 12;

                teams.Text = "Teams";
                teams.Top = parentPanel.Height / 2 - teams.Height - teams.Height / 2 - 12;
                teams.Left = parentPanel.Width / 2 - teams.Width / 2;
                teams.Parent = parentPanel;
                teams.BackColor = parentPanel.Parent.Parent.BackColor;
                teams.Width = teams.Width / 2 + parentPanel.Width / 2 - 12;

                addMatch.Text = "Add New";
                addMatch.Top = parentPanel.Height / 2 - addMatch.Height / 2;
                addMatch.Left = parentPanel.Width / 2 - addMatch.Width / 2;
                addMatch.Parent = parentPanel;
                addMatch.BackColor = parentPanel.Parent.Parent.BackColor;
                addMatch.Width = addMatch.Width / 2 + parentPanel.Width / 2 - 12;


                delMatch.Text = "Delete";
                delMatch.Top = parentPanel.Height / 2 + delMatch.Height / 2 + 12;
                delMatch.Left = parentPanel.Width / 2 - delMatch.Width / 2;
                delMatch.Parent = parentPanel;
                delMatch.BackColor = parentPanel.Parent.Parent.BackColor;
                delMatch.Width = delMatch.Width / 2 + parentPanel.Width / 2 - 12;

                archive.Text = "Archive";
                archive.Top = parentPanel.Height / 2 + archive.Height + archive.Height / 2 + 12 * 2;
                archive.Left = parentPanel.Width / 2 - archive.Width / 2;
                archive.Parent = parentPanel;
                archive.BackColor = parentPanel.Parent.Parent.BackColor;
                archive.Width = archive.Width / 2 + parentPanel.Width / 2 - 12;

                save.Text = "Save";
                save.Width = parentPanel.Width - 24;
                save.Top = parentPanel.Height - save.Height - 12;
                save.Left = parentPanel.Width / 2 - save.Width / 2;
                save.Parent = parentPanel;
                save.BackColor = parentPanel.Parent.Parent.BackColor;

                players.Click += new EventHandler(playersClick);
                teams.Click += new EventHandler(teamsClick);
                addMatch.Click += new EventHandler(addMatchClick);
                delMatch.Click += new EventHandler(delMatchClick);
                archive.Click += new EventHandler(archiveClick);
            }
        }

        public void onResized()
        {
            title.Top = 12;
            title.Left = parentPanel.Width / 2 - 30;
            title.Height = 30;

            Profit.Width = parentPanel.Width / 4 * 3;
            Profit.Height = 30;
            Profit.Top = 50;
            Profit.Left = parentPanel.Width / 2 - Profit.Width / 2;

            players.Top = parentPanel.Height / 2 - players.Height * 2 - players.Height / 2 - 12 * 2;
            players.Left = parentPanel.Width / 2 - width / 2;
            players.Width = width / 2 + parentPanel.Width / 2 - 12;

            teams.Top = parentPanel.Height / 2 - teams.Height - teams.Height / 2 - 12;
            teams.Left = parentPanel.Width / 2 - width / 2;
            teams.Width = width / 2 + parentPanel.Width / 2 - 12;

            addMatch.Top = parentPanel.Height / 2 - addMatch.Height / 2;
            addMatch.Left = parentPanel.Width / 2 - width / 2;
            addMatch.Width = width / 2 + parentPanel.Width / 2 - 12;

            delMatch.Top = parentPanel.Height / 2 + delMatch.Height / 2 + 12;
            delMatch.Left = parentPanel.Width / 2 - width / 2;
            delMatch.Width = width / 2 + parentPanel.Width / 2 - 12;

            archive.Top = parentPanel.Height / 2 + archive.Height + archive.Height / 2 + 12 * 2;
            archive.Left = parentPanel.Width / 2 - width / 2;
            archive.Width = width / 2 + parentPanel.Width / 2 - 12;

            save.Width = parentPanel.Width - 24;
            save.Top = parentPanel.Height - save.Height - 12;
            save.Left = parentPanel.Width / 2 - save.Width / 2;
        }

        #region EventHandlers
        private void playersClick(Object s, EventArgs e)
        {
            pf = new PlayersForm();
            pf.Show();
        }

        private void teamsClick(Object s, EventArgs e)
        {
            f2 = new TeamForm();
            f2.Show();
        }

        private void archiveClick(Object s, EventArgs e)
        {

        }

        private void addMatchClick(Object s, EventArgs e)
        {
            f = new NewMatchForm();
            f.Show();
        }

        private void delMatchClick(Object s, EventArgs e)
        {

        }

        private void saveClick(Object s, EventArgs e)
        {

        }

        private void editClick(Object s, EventArgs e)
        {

        }
        #endregion

    }
}
