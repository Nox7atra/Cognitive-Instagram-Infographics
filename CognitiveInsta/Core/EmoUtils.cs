using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace CognitiveInsta.Core
{
    public enum EmotionsType
    {
        Happiness = 0,
        Anger = 1,
        Fear = 2,
        Contempt = 3,
        Neutral = 4,
        Surprise = 5,
        Disgust = 6,
        Sadness = 7
    }
    public static class EmoUtils
    {
        public readonly static string EMO_DATA_FOLDER_NAME = "emotions";
        public static float GetEmoScore(Emotion emotion, EmotionsType type)
        {
            switch (type)
            {
                default:
                case EmotionsType.Happiness:
                    return emotion.Scores.Happiness;
                case EmotionsType.Fear:
                    return emotion.Scores.Fear;
                case EmotionsType.Anger:
                    return emotion.Scores.Anger;
                case EmotionsType.Contempt:
                    return emotion.Scores.Contempt;
                case EmotionsType.Disgust:
                    return emotion.Scores.Disgust;
                case EmotionsType.Neutral:
                    return emotion.Scores.Neutral;
                case EmotionsType.Sadness:
                    return emotion.Scores.Sadness;
                case EmotionsType.Surprise:
                    return emotion.Scores.Surprise;
            }
        }
        public static string GetEmotionColor(EmotionsType type)
        {
            switch (type)
            {
                default:
                case EmotionsType.Happiness:
                    return "#f1c40f";
                case EmotionsType.Fear:
                    return "#34495e";
                case EmotionsType.Anger:
                    return "#c0392b";
                case EmotionsType.Contempt:
                    return "#f39c12";
                case EmotionsType.Disgust:
                    return "#7f8c8d";
                case EmotionsType.Neutral:
                    return "#27ae60";
                case EmotionsType.Sadness:
                    return "#9b59b6";
                case EmotionsType.Surprise:
                    return "#3498db";
            }
        }
        public static string GetEmotionLabel(EmotionsType type)
        {
            switch (type)
            {
                default:
                case EmotionsType.Happiness:
                    return "Happiness";
                case EmotionsType.Fear:
                    return "Fear";
                case EmotionsType.Anger:
                    return "Anger";
                case EmotionsType.Contempt:
                    return "Contempt";
                case EmotionsType.Disgust:
                    return "Disgust";
                case EmotionsType.Neutral:
                    return "Neutral";
                case EmotionsType.Sadness:
                    return "Sadness";
                case EmotionsType.Surprise:
                    return "Surprise";
            }
        }
        public static async Task<List<EmoInfo>> GetEmoData(
            string savedDataFileName, 
            List<InstaInfo> instaInfo,
            string apiKey,
            bool isApiKeyTrial)
        {
            List<EmoInfo> emoInfoList;
            string fileName = string.Concat(savedDataFileName, ".json");

            StorageFolder folder = await ApplicationData.Current.
                LocalFolder.CreateFolderAsync(MainPage.DATA_FOLDER_NAME, CreationCollisionOption.OpenIfExists);
            StorageFolder emoFolder = await folder.CreateFolderAsync(EMO_DATA_FOLDER_NAME,
                CreationCollisionOption.OpenIfExists);

            string emoText;
            if (await emoFolder.TryGetItemAsync(fileName) == null)
            {
                emoInfoList = await GetDataFromMsCognitive(savedDataFileName, instaInfo, apiKey, isApiKeyTrial);
                StorageFile file
                   = await emoFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                emoText = JsonConvert.SerializeObject(emoInfoList);
                await FileIO.WriteTextAsync(file, emoText);
            }
            else
            {
                emoText = await FileIO.ReadTextAsync(await emoFolder.GetFileAsync(fileName));
                emoInfoList = JsonConvert.DeserializeObject<List<EmoInfo>>(emoText);
            }
            return emoInfoList;
        }
        private static async Task<List<EmoInfo>> GetDataFromMsCognitive(
            string userName,
            List<InstaInfo> instaInfo,
            string apiKey,
            bool isApiKeyTrial)
        {
            EmotionServiceClient _EmoClient = new EmotionServiceClient(apiKey);
            List<EmoInfo> emoInfoList = new List<EmoInfo>();
            Emotion[] emoInfo;
            for (int i = 0; i < instaInfo.Count; i++)
            {
                try
                {
                    if (isApiKeyTrial)
                    {
                        await Task.Delay(3000);
                    }
                    emoInfo = await _EmoClient.RecognizeAsync(instaInfo[i].PostImageUrl);
                    EmoInfo info = new EmoInfo();
                    info.ProfileFullName = instaInfo[i].ProfileName;
                    info.ImageUrl = instaInfo[i].PostImageUrl;
                    info.CommentsCount = instaInfo[i].CommentsCount;
                    info.LikesCount = instaInfo[i].LikesCount;
                    info.Emotions = emoInfo.ToList();
                    emoInfoList.Add(info);

                    System.Diagnostics.Debug.Write($"Photo number: {i}\n");
                }
                catch
                {
                }
            }
            System.Diagnostics.Debug.Write("Success!\n");
            return emoInfoList;
        }
    }
}
