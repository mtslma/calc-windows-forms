using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace CalculadoraSemiCientifica
{
    public partial class FormSobre : Form
    {
        public FormSobre()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkRepositorio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = linkRepositorio.Text;
            Process.Start(url);
        }
    }
}
