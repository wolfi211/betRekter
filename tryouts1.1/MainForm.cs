using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryouts1._1
{
    public partial class MainForm : Form
    {
        private Panel main;
        private Panel matches;
        private Panel options;
        private Panel oneMatch;
        private int padding = 12;
        private OptionPanel op;
        private MatchList ml;

        public MainForm()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 837;
            
            this.Load += new EventHandler(formLoaded);
        }

        private void formLoaded(Object s, EventArgs e)
        {
            this.Top = 0;
            this.Left = 400;
            mainPanels();
            op = new OptionPanel(options);
            ml = new MatchList(matches, this);
            setEventHandlers();
        }

        private void setEventHandlers()
        {
            matches.Resize += new EventHandler(matchListResize);
            options.Resize += new EventHandler(optionPanelResize);
            this.Resize += new EventHandler(formResize);
        }

        private void mainPanels()
        {
            main = new Panel();
            matches = new Panel();
            options = new Panel();
            oneMatch = new Panel();

            main.Width = this.ClientRectangle.Width;
            main.Height = this.ClientRectangle.Height;
            main.Top = 0;
            main.Left = 0;
            main.BackColor = Color.FromArgb(200, 200, 255);
            main.Parent = this;
            main.BringToFront();

            matches.Width = main.Width / 3 - padding;
            matches.Height = main.Height - padding * 2;
            matches.Top = padding;
            matches.Left = padding;
            matches.BackColor = Color.FromArgb(201,201,201);
            matches.Parent = main;
            matches.AutoScroll = true;

            options.Width = main.Width / 4;
            options.Height = main.Height - padding * 2;
            options.Top = padding;
            options.Left = matches.Right;
            options.BackColor = Color.FromArgb(201, 201, 201);
            options.Parent = main;

            oneMatch.Width = main.Width * 5 / 12 - padding;
            oneMatch.Height = main.Height - padding * 2;
            oneMatch.Top = padding;
            oneMatch.Left = options.Right;
            oneMatch.BackColor = Color.FromArgb(201, 201, 201);
            oneMatch.Parent = main;
        }

        private void formResize(Object s, EventArgs e)
        {
            main.Width = this.ClientRectangle.Width;
            main.Height = this.ClientRectangle.Height;
            main.Top = 0;
            main.Left = 0;

            matches.Width = main.Width / 3 - padding;
            matches.Height = main.Height - padding * 2;
            matches.Top = padding;
            matches.Left = padding;

            options.Width = main.Width / 4;
            options.Height = main.Height - padding * 2;
            options.Top = padding;
            options.Left = matches.Right;

            oneMatch.Width = main.Width * 5 / 12 - padding;
            oneMatch.Height = main.Height - padding * 2;
            oneMatch.Top = padding;
            oneMatch.Left = options.Right;

            this.Validate();
        }

        private void optionPanelResize(Object s, EventArgs e)
        {
            op.onResized();
        }

        private void matchListResize(Object s, EventArgs e)
        {
            ml.onResize();
        }
    }
}
