using System;
using System.Windows.Forms;

namespace KiltwalkCertificateGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonTemplate_Click(object sender, EventArgs e)
        {
            updateTextFromUserFileSelector(textBoxTemplate);
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            updateTextFromUserFileSelector(textBoxInputs);
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            textBoxInfo.Text = "Processing...";
            this.Refresh();

            var certificateGenerator = new CertificateGenerator();

            certificateGenerator.Execute(textBoxInputs.Text, textBoxTemplate.Text);            

            textBoxInfo.Text = "Done!";
        }



        private void updateTextFromUserFileSelector(System.Windows.Forms.TextBox textbox)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textbox.Text = openFileDialog1.FileName;
            }
        }
    }
}
