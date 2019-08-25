using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.WebCrawler.App;
using Newtonsoft.Json.Linq;

namespace WebCrawler
{
    public class BitStopCrawler : IBitStopCrawler
    {
        private readonly IAppBLL _bll;

        private static readonly HttpClient Client = new HttpClient();
        private static readonly string SpreadsheetId = Environment.GetEnvironmentVariable("BITSTOP_SPREADSHEET_ID");
        private static readonly string Range = Environment.GetEnvironmentVariable("BITSTOP_SPREADSHEET_RANGE");

        private static readonly string ApiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY");
        private const string ApiBase = "https://sheets.googleapis.com/v4/spreadsheets";
        
        private const int ProviderId = 1;

        private const int SoupCategoryId = 1;

        private const int VeganTag = 1;
        private const int GlutenFreeTag = 2;
        private const int LactoseFreeTag = 3;
        
        public BitStopCrawler(IAppBLL bll)
        {
            _bll = bll;
        }
        
        public async Task UpdateFoodItems()
        {
            var lastFoodItemId = 0;
            var priceModifier = "";

            var tagsToAdd = new List<int>();
            
            _bll.FoodItems.ArchiveAllActiveFoodItemsFromProvider(ProviderId);
            await _bll.SaveChangesAsync();

            var parsedObject = await GetFoodItemsJsonObject();

            foreach (var item in parsedObject["values"])
            {
                // skip empty line
                if (!item.HasValues) continue;
                
                // reset priceModifier
                priceModifier = "";
                
                var nameEst = item.First.ToString();
                var price = item.First.Next.ToString();
                var nameEng = item.Last.ToString();
                Console.WriteLine($"est: {nameEst}, eng: {nameEng}, price: {price}");
                
                // skip non-fooditem and faulty lines
                if (price == "" || nameEst == "" || nameEng == "") continue;
                
                #region priceModifiers
                
                if (nameEst.Contains("/"))
                {
                    priceModifier = nameEng.Contains("/") ? nameEng.Substring(nameEng.IndexOf('/') + 1) : "";
                    
                    if (nameEst[0].Equals('/') && lastFoodItemId != 0)
                    {
                        _bll.Prices.Add(new Price()
                        {
                            FoodItemId = lastFoodItemId,
                            PriceValue = decimal.Parse(price.Substring(1)),
                            ModifierName = priceModifier
                        });
                        
                        await _bll.SaveChangesAsync();
                        continue;
                    }

                    nameEst = nameEst.Remove(nameEst.IndexOf('/'));
                    if (nameEng.Contains("/")) nameEng = nameEng.Remove(nameEng.IndexOf('/'));

                }
                
                #endregion

                #region FoodTags

                if (nameEst.Contains("(V)"))
                {
                    tagsToAdd.Add(VeganTag);
                    nameEst = nameEst.Replace("(V)", "");
                    nameEng = nameEng.Replace("(V)", "");
                }
                
                if (nameEst.Contains("(L)"))
                {
                    tagsToAdd.Add(LactoseFreeTag);
                    nameEst = nameEst.Replace("(L)", "");
                    nameEng = nameEng.Replace("(L)", "");
                }
                
                if (nameEst.Contains("(G)"))
                {
                    tagsToAdd.Add(GlutenFreeTag);
                    nameEst = nameEst.Replace("(G)", "");
                    nameEng = nameEng.Replace("(G)", "");
                }

                #endregion
                
                // get the enitity back with attached state id - (- maxint)
                var foodItem = _bll.FoodItems.Add(new FoodItem()
                {
                    ProviderId = ProviderId,
                    NameEst = nameEst.Trim(),
                    NameEng = nameEng.Trim(),
                    DateStart = DateTime.Now,
                });
                
                // ef will update its internally tracked entities
                await _bll.SaveChangesAsync();
                // get the updated entity, now with ID from database
                lastFoodItemId = _bll.FoodItems.GetUpdatesAfterUOWSaveChanges(foodItem).Id;

                _bll.Prices.Add(new Price()
                {
                    FoodItemId = lastFoodItemId,
                    PriceValue = decimal.Parse(price.Substring(1)),
                    ModifierName = priceModifier == "" ? null : priceModifier
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

        private static async Task<JObject> GetFoodItemsJsonObject()
        {
            var responseString = await Client.GetStringAsync($"{ApiBase}/{SpreadsheetId}/values/{Range}?key={ApiKey}");

            return JObject.Parse(responseString);
        }
    }
}