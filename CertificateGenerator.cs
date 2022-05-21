using GemBox.Presentation;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KiltwalkCertificateGenerator
{
    public class CertificateGenerator
    {
        public void Execute(string inputLocation, string templateLocation)
        {
            List<(string, string)> names = getInputs(inputLocation);

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var index = 1;

            foreach (var (name, amount_raised) in names)
            {
                var pres = PresentationDocument.Load(templateLocation);

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

                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(templateLocation), "Output"));

                string newppt = Path.Combine(Path.GetDirectoryName(templateLocation), 
                                             "Output", index + "_" +
                                             Path.GetFileNameWithoutExtension(templateLocation) + "_" +
                                             name + Path.GetExtension(templateLocation));
                pres.Save(newppt);

                string newpdf = Path.Combine(Path.GetDirectoryName(templateLocation), "Output", index + "_" + name + ".pdf");
                pres.Save(newpdf);

                index++;
            }
        }



        private List<(string, string)> getInputs(string inputsLocation)
        {
            var inputs = new List<(string, string)>();
            using (var reader = new StreamReader(inputsLocation))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    inputs.Add((values[0], values[1]));
                }
            }
            return inputs;
        }
    }
}
