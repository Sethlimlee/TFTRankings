using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TFTRank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TFTController : Controller
    {
        // GET: TFT
        public async Task<List<DisplayEntries>> GetRankings()
        {
            var displayEntriesList = new List<DisplayEntries>();
            int rank = 1;

            #region Challengers

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Challengers ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            HttpRequestMessage capsuleChallenger = new HttpRequestMessage
            {
                RequestUri = new System.Uri("https://na1.api.riotgames.com/tft/league/v1/challenger"),
                Method = HttpMethod.Get,
            };

            HttpClient clientChallenger = new HttpClient();
            clientChallenger.DefaultRequestHeaders.Add("X-Riot-Token", "");

            HttpResponseMessage responseChallenger = new HttpResponseMessage();
            responseChallenger = await clientChallenger.GetAsync(capsuleChallenger.RequestUri);

            var rawStreamChallenger = await responseChallenger.Content.ReadAsStringAsync();
            var riotObjectChallenger = JsonConvert.DeserializeObject<JObject>(rawStreamChallenger);
            int countTestC = riotObjectChallenger["entries"].Count();
            var listOfEntriesChallenger = riotObjectChallenger.Value<JArray>("entries").ToObject<List<Entries>>();
            int countChallenger = listOfEntriesChallenger.Count();

            var sortedEntriesChallenger = listOfEntriesChallenger.OrderByDescending(o => o.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesChallenger)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Challenger",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Challengers ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Challengers

            #region GrandMasters

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ GrandMasters ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            HttpRequestMessage capsuleGrandMaster = new HttpRequestMessage
            {
                RequestUri = new System.Uri("https://na1.api.riotgames.com/tft/league/v1/grandmaster"),
                Method = HttpMethod.Get,
            };

            HttpClient clientGrandMaster = new HttpClient();
            clientGrandMaster.DefaultRequestHeaders.Add("X-Riot-Token", "");

            HttpResponseMessage responseGrandMaster = new HttpResponseMessage();
            responseGrandMaster = await clientGrandMaster.GetAsync(capsuleGrandMaster.RequestUri);

            var rawStreamGrandMaster = await responseGrandMaster.Content.ReadAsStringAsync();
            var riotObjectGrandMaster = JsonConvert.DeserializeObject<JObject>(rawStreamGrandMaster);

            var countTest = riotObjectGrandMaster["entries"].Count();

            var listOfEntriesGrandMaster = riotObjectGrandMaster.Value<JArray>("entries").ToObject<List<Entries>>();
            int countGrandMaster = listOfEntriesGrandMaster.Count();
            var sortedEntriesGrandMaster = listOfEntriesGrandMaster.OrderByDescending(o => o.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesGrandMaster)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "GrandMaster",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ GrandMasters ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion GrandMasters

            #region Masters

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Masters ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            HttpRequestMessage capsuleMasters = new HttpRequestMessage
            {
                RequestUri = new System.Uri("https://na1.api.riotgames.com/tft/league/v1/master"),
                Method = HttpMethod.Get,
            };

            HttpClient clientMasters = new HttpClient();
            clientMasters.DefaultRequestHeaders.Add("X-Riot-Token", "");

            HttpResponseMessage responseMasters = new HttpResponseMessage();
            responseMasters = await clientMasters.GetAsync(capsuleMasters.RequestUri);

            var rawStreamMasters = await responseMasters.Content.ReadAsStringAsync();
            var riotObjectMasters = JsonConvert.DeserializeObject<JObject>(rawStreamMasters);
            var countTestm = riotObjectMasters["entries"].Count();

            var listOfEntriesMasters = riotObjectMasters.Value<JArray>("entries").ToObject<List<Entries>>();
            int countMasters = listOfEntriesMasters.Count();

            var sortedEntriesMasters = listOfEntriesMasters.OrderBy(o => o.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesMasters)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Masters",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Masters ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Masters

            #region Diamond1

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Diamond1 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesDiamond1 = new List<Entries>();
            bool toggleDiamond1 = true;
            int pageNumDiamond1 = 1;

            while (toggleDiamond1)
            {
                HttpRequestMessage capsuleDiamond1 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/DIAMOND/I?page={pageNumDiamond1}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientDiamond1 = new HttpClient();
                clientDiamond1.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responseDiamond1 = new HttpResponseMessage();
                responseDiamond1 = await clientDiamond1.GetAsync(capsuleDiamond1.RequestUri);
                if (responseDiamond1.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamDiamond1 = await responseDiamond1.Content.ReadAsStringAsync();
                if (rawStreamDiamond1 == "[]")
                {
                    toggleDiamond1 = false;
                    break;
                }
                var riotObjectDiamond1 = JsonConvert.DeserializeObject<JArray>(rawStreamDiamond1);
                var listOfEntriesDiamond1 = riotObjectDiamond1.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesDiamond1)
                {
                    TotalListOfEntriesDiamond1.Add(entry);
                }
                pageNumDiamond1++;
            }

            var sortedEntriesDiamond1 = TotalListOfEntriesDiamond1.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesDiamond1)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Diamond",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Diamond1 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Diamond1

            #region Diamond2

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Diamond2 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesDiamond2 = new List<Entries>();
            bool toggleDiamond2 = true;
            int pageNumDiamond2 = 1;

            while (toggleDiamond2)
            {
                HttpRequestMessage capsuleDiamond2 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/DIAMOND/II?page={pageNumDiamond2}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientDiamond2 = new HttpClient();
                clientDiamond2.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responseDiamond2 = new HttpResponseMessage();
                responseDiamond2 = await clientDiamond2.GetAsync(capsuleDiamond2.RequestUri);
                if (responseDiamond2.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamDiamond2 = await responseDiamond2.Content.ReadAsStringAsync();
                if (rawStreamDiamond2 == "[]")
                {
                    toggleDiamond2 = false;
                    break;
                }
                var riotObjectDiamond2 = JsonConvert.DeserializeObject<JArray>(rawStreamDiamond2);
                var listOfEntriesDiamond2 = riotObjectDiamond2.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesDiamond2)
                {
                    TotalListOfEntriesDiamond2.Add(entry);
                }

                pageNumDiamond2++;
            }

            var sortedEntriesDiamond2 = TotalListOfEntriesDiamond2.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesDiamond2)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Diamond",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Diamond2 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Diamond2

            #region Diamond3

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Diamond3 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesDiamond3 = new List<Entries>();
            bool toggleDiamond3 = true;
            int pageNumDiamond3 = 1;

            while (toggleDiamond3)
            {
                HttpRequestMessage capsuleDiamond3 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/DIAMOND/III?page={pageNumDiamond3}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientDiamond3 = new HttpClient();
                clientDiamond3.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responseDiamond3 = new HttpResponseMessage();
                responseDiamond3 = await clientDiamond3.GetAsync(capsuleDiamond3.RequestUri);
                if (responseDiamond3.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamDiamond3 = await responseDiamond3.Content.ReadAsStringAsync();
                if (rawStreamDiamond3 == "[]")
                {
                    toggleDiamond3 = false;
                    break;
                }
                var riotObjectDiamond3 = JsonConvert.DeserializeObject<JArray>(rawStreamDiamond3);
                var listOfEntriesDiamond3 = riotObjectDiamond3.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesDiamond3)
                {
                    TotalListOfEntriesDiamond3.Add(entry);
                }

                pageNumDiamond3++;
            }

            var sortedEntriesDiamond3 = TotalListOfEntriesDiamond3.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesDiamond3)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Diamond",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Diamond3 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Diamond3

            #region Diamond4

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Diamond4 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesDiamond4 = new List<Entries>();
            bool toggleDiamond4 = true;
            int pageNumDiamond4 = 1;

            while (toggleDiamond4)
            {
                HttpRequestMessage capsuleDiamond4 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/DIAMOND/IV?page={pageNumDiamond4}"),
                    Method = HttpMethod.Get,
                };

                // test//
                HttpClient clientDiamond4 = new HttpClient();
                clientDiamond4.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responseDiamond4 = new HttpResponseMessage();
                responseDiamond4 = await clientDiamond4.GetAsync(capsuleDiamond4.RequestUri);
                if (responseDiamond4.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamDiamond4 = await responseDiamond4.Content.ReadAsStringAsync();
                if (rawStreamDiamond4 == "[]")
                {
                    toggleDiamond4 = false;
                    break;
                }
                var riotObjectDiamond4 = JsonConvert.DeserializeObject<JArray>(rawStreamDiamond4);
                var listOfEntriesDiamond4 = riotObjectDiamond4.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesDiamond4)
                {
                    TotalListOfEntriesDiamond4.Add(entry);
                }
                pageNumDiamond4++;
            }

            var sortedEntriesDiamond4 = TotalListOfEntriesDiamond4.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesDiamond4)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Diamond",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Diamond4 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Diamond4

            #region Plat1

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Plat1 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesPlat1 = new List<Entries>();
            bool togglePlat1 = true;
            int pageNumPlat1 = 1;

            while (togglePlat1)
            {
                HttpRequestMessage capsulePlat1 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/PLATINUM/I?page={pageNumPlat1}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientPlat1 = new HttpClient();
                clientPlat1.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responsePlat1 = new HttpResponseMessage();
                responsePlat1 = await clientPlat1.GetAsync(capsulePlat1.RequestUri);
                if (responsePlat1.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamPlat1 = await responsePlat1.Content.ReadAsStringAsync();
                if (rawStreamPlat1 == "[]")
                {
                    togglePlat1 = false;
                    break;
                }
                var riotObjectPlat1 = JsonConvert.DeserializeObject<JArray>(rawStreamPlat1);
                var listOfEntriesPlat1 = riotObjectPlat1.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesPlat1)
                {
                    TotalListOfEntriesPlat1.Add(entry);
                }
                pageNumPlat1++;
            }

            var sortedEntriesPlat1 = TotalListOfEntriesPlat1.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesPlat1)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Platinum",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Plat1 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Plat1

            #region Plat2

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Plat2 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesPlat2 = new List<Entries>();
            bool togglePlat2 = true;
            int pageNumPlat2 = 1;

            while (togglePlat2)
            {
                HttpRequestMessage capsulePlat2 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/PLATINUM/II?page={pageNumPlat2}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientPlat2 = new HttpClient();
                clientPlat2.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responsePlat2 = new HttpResponseMessage();
                responsePlat2 = await clientPlat2.GetAsync(capsulePlat2.RequestUri);
                if (responsePlat2.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamPlat2 = await responsePlat2.Content.ReadAsStringAsync();
                if (rawStreamPlat2 == "[]")
                {
                    togglePlat2 = false;
                    break;
                }
                var riotObjectPlat2 = JsonConvert.DeserializeObject<JArray>(rawStreamPlat2);
                var listOfEntriesPlat2 = riotObjectPlat2.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesPlat2)
                {
                    TotalListOfEntriesPlat2.Add(entry);
                }
                pageNumPlat2++;
            }

            var sortedEntriesPlat2 = TotalListOfEntriesPlat2.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesPlat2)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Platinum",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Plat2 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Plat2

            #region Plat3

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Plat3 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesPlat3 = new List<Entries>();
            bool togglePlat3 = true;
            int pageNumPlat3 = 1;

            while (togglePlat3)
            {
                HttpRequestMessage capsulePlat3 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/PLATINUM/III?page={pageNumPlat3}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientPlat3 = new HttpClient();
                clientPlat3.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responsePlat3 = new HttpResponseMessage();
                responsePlat3 = await clientPlat3.GetAsync(capsulePlat3.RequestUri);
                if (responsePlat3.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamPlat3 = await responsePlat3.Content.ReadAsStringAsync();
                if (rawStreamPlat3 == "[]")
                {
                    togglePlat3 = false;
                    break;
                }
                var riotObjectPlat3 = JsonConvert.DeserializeObject<JArray>(rawStreamPlat3);
                var listOfEntriesPlat3 = riotObjectPlat3.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesPlat3)
                {
                    TotalListOfEntriesPlat3.Add(entry);
                }

                pageNumPlat3++;
            }

            var sortedEntriesPlat3 = TotalListOfEntriesPlat3.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesPlat3)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Platinum",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Plat3 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Plat3

            #region Plat4

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Plat4 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesPlat4 = new List<Entries>();
            bool togglePlat4 = true;
            int pageNumPlat4 = 1;

            while (togglePlat4)
            {
                HttpRequestMessage capsulePlat4 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/PLATINUM/IV?page={pageNumPlat4}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientPlat4 = new HttpClient();
                clientPlat4.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responsePlat4 = new HttpResponseMessage();
                responsePlat4 = await clientPlat4.GetAsync(capsulePlat4.RequestUri);
                if (responsePlat4.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamPlat4 = await responsePlat4.Content.ReadAsStringAsync();
                if (rawStreamPlat4 == "[]")
                {
                    togglePlat4 = false;
                    break;
                }
                var riotObjectPlat4 = JsonConvert.DeserializeObject<JArray>(rawStreamPlat4);
                var listOfEntriesPlat4 = riotObjectPlat4.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesPlat4)
                {
                    TotalListOfEntriesPlat4.Add(entry);
                }
                pageNumPlat4++;
            }

            var sortedEntriesPlat4 = TotalListOfEntriesPlat4.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesPlat4)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Platinum",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Plat4 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Plat4

            #region Gold1

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Gold1 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesGold1 = new List<Entries>();
            bool toggleGold1 = true;
            int pageNumGold1 = 1;

            while (toggleGold1)
            {
                HttpRequestMessage capsuleGold1 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/GOLD/I?page={pageNumGold1}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientGold1 = new HttpClient();
                clientGold1.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responseGold1 = new HttpResponseMessage();
                responseGold1 = await clientGold1.GetAsync(capsuleGold1.RequestUri);
                if (responseGold1.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamGold1 = await responseGold1.Content.ReadAsStringAsync();
                if (rawStreamGold1 == "[]")
                {
                    toggleGold1 = false;
                    break;
                }
                var riotObjectGold1 = JsonConvert.DeserializeObject<JArray>(rawStreamGold1);
                var listOfEntriesGold1 = riotObjectGold1.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesGold1)
                {
                    TotalListOfEntriesGold1.Add(entry);
                }

                pageNumGold1++;
            }

            var sortedEntriesGold1 = TotalListOfEntriesGold1.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesGold1)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Gold",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Gold1 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Gold1

            #region Gold2

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Gold2 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesGold2 = new List<Entries>();
            bool toggleGold2 = true;
            int pageNumGold2 = 1;

            while (toggleGold2)
            {
                HttpRequestMessage capsuleGold2 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/GOLD/II?page={pageNumGold2}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientGold2 = new HttpClient();
                clientGold2.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responseGold2 = new HttpResponseMessage();
                responseGold2 = await clientGold2.GetAsync(capsuleGold2.RequestUri);
                if (responseGold2.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamGold2 = await responseGold2.Content.ReadAsStringAsync();
                if (rawStreamGold2 == "[]")
                {
                    toggleGold2 = false;
                    break;
                }
                var riotObjectGold2 = JsonConvert.DeserializeObject<JArray>(rawStreamGold2);
                var listOfEntriesGold2 = riotObjectGold2.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesGold2)
                {
                    TotalListOfEntriesGold2.Add(entry);
                }

                pageNumGold2++;
            }

            var sortedEntriesGold2 = TotalListOfEntriesGold2.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesGold2)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Gold",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Gold2 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Gold2

            #region Gold3

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Gold3 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesGold3 = new List<Entries>();
            bool toggleGold3 = true;
            int pageNumGold3 = 1;

            while (toggleGold3)
            {
                HttpRequestMessage capsuleGold3 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/GOLD/III?page={pageNumGold3}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientGold3 = new HttpClient();
                clientGold3.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responseGold3 = new HttpResponseMessage();
                responseGold3 = await clientGold3.GetAsync(capsuleGold3.RequestUri);
                if (responseGold3.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamGold3 = await responseGold3.Content.ReadAsStringAsync();
                if (rawStreamGold3 == "[]")
                {
                    toggleGold3 = false;
                    break;
                }
                var riotObjectGold3 = JsonConvert.DeserializeObject<JArray>(rawStreamGold3);
                var listOfEntriesGold3 = riotObjectGold3.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesGold3)
                {
                    TotalListOfEntriesGold3.Add(entry);
                }

                pageNumGold3++;
            }

            var sortedEntriesGold3 = TotalListOfEntriesGold3.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesGold3)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Gold",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Gold3 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Gold3

            #region Gold4

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Gold4 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            var TotalListOfEntriesGold4 = new List<Entries>();
            bool toggleGold4 = true;
            int pageNumGold4 = 1;

            while (toggleGold4)
            {
                HttpRequestMessage capsuleGold4 = new HttpRequestMessage
                {
                    RequestUri = new System.Uri($"https://na1.api.riotgames.com/tft/league/v1/entries/GOLD/IV?page={pageNumGold4}"),
                    Method = HttpMethod.Get,
                };

                HttpClient clientGold4 = new HttpClient();
                clientGold4.DefaultRequestHeaders.Add("X-Riot-Token", "");

                HttpResponseMessage responseGold4 = new HttpResponseMessage();
                responseGold4 = await clientGold4.GetAsync(capsuleGold4.RequestUri);
                if (responseGold4.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    System.Threading.Thread.Sleep(120000);
                    continue;
                }
                var rawStreamGold4 = await responseGold4.Content.ReadAsStringAsync();
                if (rawStreamGold4 == "[]")
                {
                    toggleGold4 = false;
                    break;
                }
                var riotObjectGold4 = JsonConvert.DeserializeObject<JArray>(rawStreamGold4);
                var listOfEntriesGold4 = riotObjectGold4.ToObject<List<Entries>>();

                foreach (var entry in listOfEntriesGold4)
                {
                    TotalListOfEntriesGold4.Add(entry);
                }

                pageNumGold4++;
            }

            var sortedEntriesGold4 = TotalListOfEntriesGold4.OrderByDescending(p => p.LeaguePoints).ToList();

            foreach (var entry in sortedEntriesGold4)
            {
                var displayEntry = new DisplayEntries()
                {
                    SummonerName = entry.SummonerName,
                    Rank = rank,
                    Tier = "Gold",
                    Division = entry.Rank,
                    LeaguePoints = entry.LeaguePoints
                };
                rank++;
                displayEntriesList.Add(displayEntry);
            }

            /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Gold4 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ///

            #endregion Gold4

            return displayEntriesList;
        }
    }
}