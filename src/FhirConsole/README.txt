FhirConsole.exe converts between Turtle and other FHIR STU3 formats.

DEPENDS ON: 
	Microsoft .Net Runtime on Windows.
	Microsoft Mono must be installed to run this on Linux.

USAGE: 
	FhirConsole.exe [ -FORMAT ] INFILE > OUTFILE
Where:
	-FORMAT indicates the destination format, and
	is one of: -ot -ox or -oj (for turtle, xml or json)

	INFILE is the input file, whose format will be guessed

	OUTFILE is the output file

EXAMPLE:
	./FhirConsole.exe -ot examples/patient-example.json > /tmp/out.ttl

To test round tripping, go back to JSON again:

	./FhirConsole.exe -oj /tmp/out.ttl > /tmp/round.json

Then use a JSON diff tool to compare patient-example.json
with /tmp/round.json, such as the one at http://www.jsondiff.com/ .
