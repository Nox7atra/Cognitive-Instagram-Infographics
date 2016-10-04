using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Storage;

namespace CognitiveInsta.Core
{
    public static class InstaUtils
    {
        public readonly static string INSTA_DATA_FOLDER_NAME = "insta";
        private static async Task<List<InstaInfo>> GetDataFromInsta(string userName)
        {
            List<InstaInfo> links = new List<InstaInfo>();
            bool moreAvailable = true;
            Random ran = new Random();
            string max_id = "1";
            while (moreAvailable)
            {
                var firstTwentyMediaString = await
                    new HttpClient().GetStringAsync($"https://instagram.com/{userName}/media?max_id={max_id}");
                await Task.Delay(ran.Next(100));
                System.Diagnostics.Debug.Write("Parse Instagram...\n");
                var firstTwentyMediaJson = JObject.Parse(firstTwentyMediaString);
                moreAvailable = (bool)firstTwentyMediaJson["more_available"];
                var itemsArray = (JArray)firstTwentyMediaJson["items"];
                foreach (var item in itemsArray)
                {
                    InstaInfo info = new InstaInfo();
                    max_id = (string)item["id"];
                    info.ProfileImageUrl = (string)(item["user"]["profile_picture"]);
                    info.PostImageUrl = (string)(item["images"]["standard_resolution"]["url"]);
                    info.LikesCount = (int)(item["likes"]["count"]);
                    info.CommentsCount = (int)(item["comments"]["count"]);
                    info.ProfileName = (string)(item["user"]["full_name"]);
                    links.Add(info);
                }
            }
            return links;
        }
        public static async Task<List<InstaInfo>> GetInfoPostList(string userName)
        {
            List<InstaInfo> info = new List<InstaInfo>();
            string fileName = string.Concat(userName, ".json");

            StorageFolder folder = await ApplicationData.Current.
                LocalFolder.CreateFolderAsync(MainPage.DATA_FOLDER_NAME, CreationCollisionOption.OpenIfExists);
            StorageFolder instaFolder = await folder.CreateFolderAsync(INSTA_DATA_FOLDER_NAME,
                CreationCollisionOption.OpenIfExists);

            string infoText = string.Empty;
            if (await instaFolder.TryGetItemAsync(fileName) == null)
            {
                info = await GetDataFromInsta(userName);
                StorageFile file
                    = await instaFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                infoText = JsonConvert.SerializeObject(info);
                await FileIO.WriteTextAsync(file, infoText);
            }
            else
            {
                infoText = await FileIO.ReadTextAsync(await instaFolder.GetFileAsync(fileName));
                info = JsonConvert.DeserializeObject<List<InstaInfo>>(infoText);
            }
            return info;
        }
    }
}