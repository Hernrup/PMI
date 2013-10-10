using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace PMI.Authenticate
{
    public partial class Authenticate : Form
    {

        private TextBox tbToken;
        private WebBrowser wbBrowser;
        private Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Button btOk;
        private Timer timer;
        private const string AUTHENTICATION_URL = "https://trello.com/1/authorize?key={0}&name={1}&expiration=never&response_type=token&scope=read,write";
        private const string AUTHENTICATION_TOKEN_URL = "https://trello.com/1/token/approve";

        public Authenticate()
        {
            InitializeComponent();
            setup();
            
        }

        public void setup() {
            setUrl(ConfigurationManager.AppSettings["key"], ConfigurationManager.AppSettings["applicationName"]);
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(checkForToken);
            timer.Enabled = true;
            timer.Start();
        }

        public void setUrl(string sKey,string sName) {
            string sUrl = string.Format(AUTHENTICATION_URL, sKey, sName);
            this.wbBrowser.Url = new Uri(sUrl);
        }

        public void saveToken(string sToken) {
            string sPath = string.Format("./{0}",ConfigurationManager.AppSettings["tokenFile"]);

            using (StreamWriter sw = File.CreateText(sPath))
            {
                sw.Write(sToken);
            }	
        }

        public void checkForToken(object sender, EventArgs e)
        {
            if (this.wbBrowser.Url == null) {
                return;
            }

            string sUrl = this.wbBrowser.Url.AbsoluteUri;
            string sSoure = this.wbBrowser.DocumentText;

            if (string.IsNullOrWhiteSpace(sSoure)) {
                return;
            }

            if (sUrl == AUTHENTICATION_TOKEN_URL)
            {
                Match match = Regex.Match(sSoure, @"([A-Za-z0-9]{64})", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    // Finally, we get the Group value and display it.
                    string sToken = match.Groups[1].Value;
                    this.setToken(sToken);
                }
            }
        }

        public void setToken(string sToken) {
            this.tbToken.Text = sToken;
            
        }
        
        private void tbToken_TextChanged(object sender, EventArgs e)
        {
            btOk.Enabled = (tbToken.TextLength > 0);
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            this.saveToken(tbToken.Text);
            MessageBox.Show("Token saved successfully");
            this.Close();
        }
        private void InitializeComponent()
        {
            this.btOk = new System.Windows.Forms.Button();
            this.tbToken = new System.Windows.Forms.TextBox();
            this.wbBrowser = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Enabled = false;
            this.btOk.Location = new System.Drawing.Point(897, 785);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // tbToken
            // 
            this.tbToken.Location = new System.Drawing.Point(57, 787);
            this.tbToken.Name = "tbToken";
            this.tbToken.Size = new System.Drawing.Size(834, 20);
            this.tbToken.TabIndex = 1;
            this.tbToken.TextChanged += new System.EventHandler(this.tbToken_TextChanged);
            // 
            // wbBrowser
            // 
            this.wbBrowser.Location = new System.Drawing.Point(-4, 0);
            this.wbBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbBrowser.Name = "wbBrowser";
            this.wbBrowser.Size = new System.Drawing.Size(987, 744);
            this.wbBrowser.TabIndex = 2;
            
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 790);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Token";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(983, 836);
            this.shapeContainer1.TabIndex = 4;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = -1;
            this.lineShape1.X2 = 986;
            this.lineShape1.Y1 = 744;
            this.lineShape1.Y2 = 744;
            // 
            // Authenticate
            // 
            this.AccessibleName = "";
            this.ClientSize = new System.Drawing.Size(983, 836);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wbBrowser);
            this.Controls.Add(this.tbToken);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "Authenticate";
            this.Text = "Authenticate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    

    }
}
