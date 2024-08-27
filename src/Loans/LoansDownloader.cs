using System.Net;
using System.Net.Mime;
using Tabula;
using Tabula.Detectors;
using Tabula.Extractors;
using UglyToad.PdfPig;

namespace MintosParser.Loans;

public class LoansDownloader {
    public static async Task<string> DownloadLoansAsync(string ISIN) {
        HttpClient client = new();
        Uri url = new("https://www.mintos.com/en/note-series/" + ISIN + "/view-final-terms-document");
        
        HttpResponseMessage response = await client.GetAsync(url);
        var cStream = await response.Content.ReadAsStreamAsync();
        var fileStream = File.Create( $"{ISIN}.pdf");
        cStream.CopyTo(fileStream);
        fileStream.Close();

        await ExtractTextTest(fileStream.Name);

        return "";
    }

    public static async Task<string> ExtractTextTest(string filePath) {

        using PdfDocument document = PdfDocument.Open(filePath, new ParsingOptions() { ClipPaths = true });
        ObjectExtractor oe = new ObjectExtractor(document);
        PageArea page = oe.Extract(1);

        // detect canditate table zones
        SimpleNurminenDetectionAlgorithm detector = new SimpleNurminenDetectionAlgorithm();
        var regions = detector.Detect(page);

        IExtractionAlgorithm ea = new SpreadsheetExtractionAlgorithm();
        List<Table> tables = ea.Extract(page.GetArea(regions[0].BoundingBox));
        var table = tables[0];
        var rows = table.Rows;

        var OfferedAmount = rows.AsEnumerable().Where(x => x[1].GetText() == "Aggregate Nominal Amount:").First()[2].GetText();
        var MaturityDate = rows.AsEnumerable().Where(x => x[1].GetText() == "Maturity Date:").First()[2].GetText();
        var Currency = rows.AsEnumerable().Where(x => x[1].GetText() == "Specified Currency:").First()[2].GetText();
        var InterestRate = rows.AsEnumerable().Where(x => x[1].GetText() == "Interest Rate:").First()[2].GetText();
        var OfferPrice = rows.AsEnumerable().Where(x => x[1].GetText() == "Offer Price of one Note:").First()[2].GetText();

        
        //page = oe.Extract(2);
        //regions = detector.Detect(page);
        //rows = ea.Extract(page.GetArea(regions[0].BoundingBox))[0].Rows;

        //var RentalAgreementType = rows.AsEnumerable().Where(x => x[1].GetText() == "Rental Agreement type:").First()[2].GetText();

        Console.WriteLine("Initial Price" + OfferPrice);
        Console.WriteLine("Hallo");
        return "";
    }
}