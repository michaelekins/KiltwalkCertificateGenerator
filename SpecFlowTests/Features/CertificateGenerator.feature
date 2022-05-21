Feature: Certificate Generator
***Further read***: **[Learn more about how to generate Living Documentation]
(https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@functionalityTest
Scenario: Kiltwalk certificates are correctly generated
	Given the input file contains 3 name/value pairs including duplicates
	And the Kiltwalk certificate template is used
	When the tool is used to generate certificates
	Then there are 3 .pptx output certificates generated
	And there are 3 .pdf output certificates generated