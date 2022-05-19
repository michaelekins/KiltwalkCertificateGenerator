using GemBox.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KiltwalkCertificateGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<(string,string)> getInputs()
        {
            var inputs = new List<(string, string)>();
            using (var reader = new StreamReader(textBoxInputs.Text))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    inputs.Add((values[0],values[1]));
                }
            }
            return inputs;
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

            string template = textBoxTemplate.Text;
            List<(string, string)> names = getInputs();

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var index = 1;

            foreach (var (name, amount_raised) in names)
            {
                var pres = PresentationDocument.Load(template);

                foreach (var slide in pres.Slides)
                {
                    foreach (var shape in slide.Content.Drawings.OfType<Shape>())
                    {
                        foreach (var paragraph in shape.Text.Paragraphs)
                        {
                            foreach (var run in paragraph.Elements.OfType<TextRun>())
                            {
                                run.Text = run.Text.Replace("{NAME}", name);
                                run.Text = run.Text.Replace("{AMOUNT}", amount_raised);
                            }
                        }
                    }
                }

                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(template), "Output"));

                string newppt = Path.Combine(Path.GetDirectoryName(template), "Output", index + "_" + Path.GetFileNameWithoutExtension(template) + "_" + name + Path.GetExtension(template));
                pres.Save(newppt);

                string newpdf = Path.Combine(Path.GetDirectoryName(template), "Output", index + "_" + name + ".pdf");
                pres.Save(newpdf);

                index++;
            }

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
