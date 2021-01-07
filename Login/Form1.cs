using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            //For simulation only
            string title = "Login Failed";
            textBoxRecords.Text = string.Empty;

            if (textBoxEmail.Text == string.Empty)
            {
                MessageBox.Show("Please Input Email.", title);
            }
            else if (textBoxPassword.Text == string.Empty)
            {
                MessageBox.Show("Please Input Password.", title);
            }
            else
            {
                var _businessLogic = new BusinessLogic();
                var _dataAccess = new Utilities.DataAccess();
                _businessLogic.ProcessLogin(textBoxEmail.Text, textBoxPassword.Text);
                _businessLogic.ProcessRecords();
                _dataAccess.LoadRecords();
                textBoxRecords.Text = _dataAccess.ToString();
            }


            
        }

        
    }

}

