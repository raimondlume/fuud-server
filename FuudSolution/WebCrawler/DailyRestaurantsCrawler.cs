using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.WebCrawler.App;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.EntityFrameworkCore.Internal;

namespace WebCrawler
{
    public class DailyRestaurantsCrawler : IDailyRestaurantsCrawler
    {
        private readonly IAppBLL _bll;

        private const int ITMajaProviderId = 2;
        private const int KuuesKorpusProviderId = 3;

        private const int OpeningHours = 9;
        private const int ClosingHours = 24;

        private static readonly WebClient Client = new WebClient();

        private const string DailyITMajaURL = "https://www.daily.lv/download/?f=dn_daily_nadalamenuu_ttu_it_maja.pdf";

        private const string DailyKuuesKorpusURL =
            "https://www.daily.lv/download/?f=dn_daily_nadalamenuu_ttu_6_korpus.pdf";

        private const string FilePath = "PDFs";

        private const int VeganTag = 1;

        private static readonly List<string> EstonianDays = new List<string>
            {"Esmaspäev", "Teisipäev", "Kolmapäev", "Neljapäev", "Reede"};

        private static readonly List<string> SoupStrings = new List<string>
            {"supp", "seljanka", "rassolnik", "borš", "minestroone"};

        public DailyRestaurantsCrawler(IAppBLL bll)
        {
            _bll = bll;
        }

        public async Task UpdateFoodItems()
        {
            _bll.FoodItems.ArchiveAllActiveFoodItemsFromProvider(ITMajaProviderId);
            _bll.FoodItems.ArchiveAllActiveFoodItemsFromProvider(KuuesKorpusProviderId);

            var ITmajaParsed = await ParsePDF(DailyITMajaURL, "itmaja.pdf");
            await MapStringToFoodItems(ITmajaParsed, ITMajaProviderId);

            var KuuesKorpusParsed = await ParsePDF(DailyKuuesKorpusURL, "kuueskorpus.pdf");
            await MapStringToFoodItems(KuuesKorpusParsed, KuuesKorpusProviderId);
        }

        private async Task MapStringToFoodItems(string parsedPDF, int providerId)
        {
            using (var reader = new StringReader(parsedPDF))
            {
                // skip first two lines
                reader.ReadLine();
                reader.ReadLine();

                var startDay = 0;
                var nameEst = "";
                var nameEng = "";
                var price = "";

                var tagsToAdd = new List<int>();


                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Menüüs")) break;

                    // get day of week
                    if (EstonianDays.Any(s => line.Contains(s)))
                    {
                        var estonianWord = "";
                        foreach (var word in line.Split())
                        {
                            if (EstonianDays.Any(s => s == word)) estonianWord = word;
                        }

                        startDay = EstonianDayToDayOfWeek(estonianWord);
                        continue;
                    }

                    var splitLine = line.Split(' ');
                    price = splitLine.Last();

                    line = line.Replace(price, "");
                    nameEst = line.Trim();

                    nameEng = reader.ReadLine();

                    // edgecase
                    if (EstonianDays.Any(s => nameEng.Contains(s)))
                    {
                        var estonianWord = "";
                        foreach (var word in nameEng.Split())
                        {
                            if (EstonianDays.Any(s => word.Contains(s)))
                                estonianWord = EstonianDays.Find(s => word.Contains(s));
                        }

                        startDay = EstonianDayToDayOfWeek(estonianWord);
                        nameEst = "";
                        nameEng = "";
                        continue;
                    }

                    // add vegan foodtag
                    if (nameEst.Contains("(Vegan)"))
                    {
                        tagsToAdd.Add(VeganTag);
                        nameEst = nameEst.Replace("(Vegan)", "");
                        nameEng = nameEng.Replace("(Vegan)", "");
                    }

                    // get the enitity back with attached state id - (- maxint)
                    var foodItem = _bll.FoodItems.Add(new FoodItem()
                    {
                        ProviderId = providerId,
                        NameEst = nameEst.Trim(),
                        NameEng = nameEng.Trim(),
                        DateStart = DateTime.Now
                            .Subtract(TimeSpan.FromHours(DateTime.Now.Hour))
                            .Subtract(TimeSpan.FromMinutes(DateTime.Now.Minute))
                            .StartOfWeek(DayOfWeek.Monday)
                            .AddDays(startDay - 1)
                            .AddHours(OpeningHours),
                        DateEnd = DateTime.Now
                            .Subtract(TimeSpan.FromHours(DateTime.Now.Hour))
                            .Subtract(TimeSpan.FromMinutes(DateTime.Now.Minute))
                            .StartOfWeek(DayOfWeek.Monday)
                            .AddDays(startDay - 1)
                            .AddHours(ClosingHours),
                    });

                    // ef will update its internally tracked entities
                    await _bll.SaveChangesAsync();
                    // get the updated entity, now with ID from database
                    var lastFoodItemId = _bll.FoodItems.GetUpdatesAfterUOWSaveChanges(foodItem).Id;

                    _bll.Prices.Add(new Price()
                    {
                        FoodItemId = lastFoodItemId,
                        PriceValue = decimal.Parse(price.Trim()),
                        ModifierName = SoupStrings.Any(s => nameEst.ToLower().Contains(s)) ? null : "/ 100g"
                    });

                    foreach (var foodTag in tagsToAdd)
                    {
                        _bll.FoodItemTags.Add(new FoodItemTag()
                        {
                            FoodItemId = lastFoodItemId,
                            FoodTagId = foodTag
                        });
                    }

                    tagsToAdd.Clear();

                    await _bll.SaveChangesAsync();
                }
            }
        }

        private static async Task<string> ParsePDF(string url, string filename)
        {
            var text = new StringBuilder();

            await Client.DownloadFileTaskAsync(
                url, filename);

            if (File.Exists(filename))
            {
                Console.WriteLine("file exists");
                var pdfReader = new PdfReader(filename);

                for (var page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                    var currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8,
                        Encoding.Default.GetBytes(currentText)));
                    text.Append(currentText);
                }

                pdfReader.Close();
            }

            return text.ToString();
        }

        private static int EstonianDayToDayOfWeek(string estonianDay)
        {
            switch (estonianDay.Trim().ToLower())
            {
                case "esmaspäev": return 1;
                case "teisipäev": return 2;
                case "kolmapäev": return 3;
                case "neljapäev": return 4;
                case "reede": return 5;
                default: return 0;
            }
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
