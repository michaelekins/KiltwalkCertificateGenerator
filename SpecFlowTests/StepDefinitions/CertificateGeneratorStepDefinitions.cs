using System.Diagnostics;

namespace SpecFlowTests.StepDefinitions
{
    [Binding]
    public class CertificateGeneratorStepDefinitions
    {
        private readonly Support.TestContext _context;

        public CertificateGeneratorStepDefinitions()
        {
            _context = new Support.TestContext();
        }

        [Given(@"the input file contains 66 name/value pairs including duplicates")]
        public void GivenTheInputFileContainsNameValuePairsIncludingDuplicates()
        {
            _context.inputsLocation = Path.Combine(Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\..\..\demo"), "inputs.csv");
        }

        [Given(@"the Kiltwalk certificate template is used")]
        public void GivenTheKiltwalkCertificateTemplateIsUsed()
        {
            _context.templateLocation = Path.Combine(Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\..\..\demo"), "Kiltwalk Certificate Template.pptx");
        }

        [When(@"the tool is used to generate certificates")]
        public void WhenTheToolIsUsedToGenerateCertificates()
        {
            var exe = Path.Combine(Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\..\..\bin\Debug"), "KiltwalkCertificateGenerator.exe");
            //Process.Start(exe, $"{_context.inputsLocation} {_context.templateLocation}").WaitForExit();

            //Process process = new Process();
            //process.StartInfo.FileName = exe;
            //process.StartInfo.Arguments = $"{_context.inputsLocation} {_context.templateLocation}";

            //process.Start();

            //process.WaitForExit();
            //// Check for sucessful completion
            //var run = (process.ExitCode == 0) ? true : false;

            Process.Start(@"C:\\temp\\diff.txt").WaitForExit();SocketsHttpConnectionContext 
        }

        [Then(@"there are (.*) \.(.*) output certificates generated")]
        public void ThenThereAre_PptOutputCertificatesGenerated(int expectedCount, string fileType)
        {
            
            DirectoryInfo di = new DirectoryInfo(Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\..\..\demo\Output"));
            int numPpt = di.GetFiles($"*.{fileType}", SearchOption.AllDirectories).Length;

            numPpt.Should().Be(expectedCount);
        }
    }
}
