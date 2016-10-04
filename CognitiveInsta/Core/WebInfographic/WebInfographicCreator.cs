using CognitiveInsta.Core.WebInfographic.Aliases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace CognitiveInsta.Core.WebInfographic
{
    public class WebInfographicCreator
    {
        private static readonly string TEMPLATES_FOLDER_NAME = "webtemplates";
        private static readonly string RESULT_FOLDER_NAME = "result";
        private static readonly string[] aliases = 
           {
                "PROFILE_PHOTO_URL",
                "HAPPY_PHOTO_URL",
                "SURPRISE_PHOTO_URL",
                "ANGRY_PHOTO_URL",
                "SAD_PHOTO_URL",
                "EMOTIONS_DATA",
                "EMOTIONS_COLOR_DATA",
                "EMOTIONS_LABEL_DATA",
                "POSTS_DATA",
                "LIKES_DATA",
                "MAX_LIKES",
                "PEOPLE_DATA",
                "USER_NAME",
                "AVERAGE_LIKES",
                "AVERAGE_PEOPLE_ON_PHOTO",
                "SPIRIT",
                "EMOTIONS_LIKES_DATA"

            };
        public async Task CreateWebInfoGraphic(ProfileInfo info, string templateName)
        {
            string templateText = await ReadTemplate(templateName);
            string result = ProceedTemplate(info, templateText);
            await SaveResult(string.Concat(info.Name, ".html"), result);
            System.Diagnostics.Debug.WriteLine("Add done!");
        }
        private string GetAliasText(string aliasText, ProfileInfo info)
        {
            string result = string.Empty;
            Alias alias;
            switch (aliasText)
            {
                case "USER_NAME":
                    alias = new AUserName();
                    result = alias.GetResult(info);
                    break;
                case "PROFILE_PHOTO_URL":
                    alias = new AProfilePhoto();
                    result = alias.GetResult(info);
                    break;
                case "HAPPY_PHOTO_URL":
                    alias = new AHappyPhoto();
                    result = alias.GetResult(info);
                    break;
                case "SURPRISE_PHOTO_URL":
                    alias = new ASurprisePhoto();
                    result = alias.GetResult(info);
                    break;
                case "ANGRY_PHOTO_URL":
                    alias = new AAngryPhoto();
                    result = alias.GetResult(info);
                    break;
                case "SAD_PHOTO_URL":
                    alias = new ASadPhoto();
                    result = alias.GetResult(info);
                    break;
                case "EMOTIONS_DATA":
                    alias = new AEmotionsData();
                    result = alias.GetResult(info);
                    break;
                case "EMOTIONS_COLOR_DATA":
                    alias = new AEmotionsColorData();
                    result = alias.GetResult(info);
                    break;
                case "EMOTIONS_LABEL_DATA":
                    alias = new AEmotionsLabelData();
                    result = alias.GetResult(info);
                    break;
                case "POSTS_DATA":
                    alias = new APostsData();
                    result = alias.GetResult(info);
                    break;
                case "LIKES_DATA":
                    alias = new ALikesData();
                    result = alias.GetResult(info);
                    break;
                case "MAX_LIKES":
                    alias = new AMaxLikes();
                    result = alias.GetResult(info);
                    break;
                case "PEOPLE_DATA":
                    alias = new APeopleData();
                    result = alias.GetResult(info);
                    break;
                case "AVERAGE_LIKES":
                    alias = new AAverageLikes();
                    result = alias.GetResult(info);
                    break;
                case "AVERAGE_PEOPLE_ON_PHOTO":
                    alias = new AAveragePeopleOnPhoto();
                    result = alias.GetResult(info);
                    break;
                case "SPIRIT":
                    alias = new ASpirit();
                    result = alias.GetResult(info);
                    break;
                case "EMOTIONS_LIKES_DATA":
                    alias = new AEmotionsLikesData();
                    result = alias.GetResult(info);
                    break;
            }
            return result;
        }
        private string ProceedTemplate(ProfileInfo info, string templateText)
        {
            string result = templateText;
            for(int i = 0; i < aliases.Length; i++)
            {
                string alias = aliases[i];
                result = result.Replace(
                    string.Concat(Alias.ALIAS_PREFIX, alias), 
                    GetAliasText(alias, info)); 
            }
            return result;
        }
        private async Task<string> ReadTemplate(string templateName)
        {
            Package package = Package.Current;
            string templateText;
            StorageFolder folder = await package.
                InstalledLocation.GetFolderAsync("Assets");
            StorageFolder templateFolder = await folder.CreateFolderAsync(TEMPLATES_FOLDER_NAME,
                CreationCollisionOption.OpenIfExists);
            templateText = await FileIO.ReadTextAsync(await templateFolder.GetFileAsync(templateName));
            return templateText;
        }
        private async Task SaveResult(string resultFileName, string result)
        {
            StorageFolder folder = await ApplicationData.Current.
                LocalFolder.CreateFolderAsync(MainPage.DATA_FOLDER_NAME, CreationCollisionOption.OpenIfExists);
            StorageFolder resultFolder = await folder.CreateFolderAsync(RESULT_FOLDER_NAME,
                CreationCollisionOption.OpenIfExists);
            StorageFile file
                   = await resultFolder.CreateFileAsync(resultFileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, result);
            await Windows.System.Launcher.LaunchFileAsync(file);
        }
    }
}
