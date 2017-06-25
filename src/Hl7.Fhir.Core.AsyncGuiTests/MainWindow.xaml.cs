using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Core.AsyncGuiTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string _endpoint = "https://api.hspconsortium.org/rpineda/open";
        private void Sync_OnClick(object sender, RoutedEventArgs e)
        {
            var client = new FhirClient(_endpoint);
            client.PreferredFormat = ResourceFormat.Json;
            client.ReturnFullResource = true;

            var srch = new SearchParams()
                .Where("name=Daniel")
                .LimitTo(10)
                .SummaryOnly()
                .OrderBy("birthdate",
                    SortOrder.Descending);

            Bundle result1 = client.Search<Patient>(srch);
            result1.Entry.Count.Should().BeGreaterOrEqualTo(1);
            while (result1 != null)
            {
                foreach (var x in result1.Entry)
                {
                    Patient p = (Patient)x.Resource;
                    OutputList.Items.Add(DateTime.Now + 
                        $@"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = client.Continue(result1, PageDirection.Next);
            }

            OutputList.Items.Add(DateTime.Now + "- Test Completed");
        }

        private async void AsyncButton_OnClick(object sender, RoutedEventArgs e)
        {
            var client = new FhirClient(_endpoint);
            client.PreferredFormat = ResourceFormat.Json;
            client.ReturnFullResource = true;

            var srch = new SearchParams()
                .Where("name=Daniel")
                .LimitTo(10)
                .SummaryOnly()
                .OrderBy("birthdate",
                    SortOrder.Descending);

            Bundle result1 = await client.SearchAsync<Patient>(srch);
            result1.Entry.Count.Should().BeGreaterOrEqualTo(1);
            while (result1 != null)
            {
                foreach (var x in result1.Entry)
                {
                    Patient p = (Patient)x.Resource;
                    OutputList.Items.Add(DateTime.Now +
                                         $@"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = await client.ContinueAsync(result1, PageDirection.Next);
            }

            OutputList.Items.Add(DateTime.Now + "- Test Completed");
        }
    }
}
