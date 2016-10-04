using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveInsta.Core
{
    public class ProfileInfo
    {
        #region private properties
        private string _Name;
        private string _AvatarUrl;
        private List<InstaInfo> _InstagramInformation;
        private List<EmoInfo> _EmotionsInformation;
        private Dictionary<EmotionsType, double> _EmotionsWeights;
        private double _WeightsSum;
        #endregion
        #region public getters
        public string Name
        {
            get
            {
                return _Name;
            }
        }
        public string AvatarUrl
        {
            get
            {
                return _AvatarUrl;
            }
        }
        public List<InstaInfo> InstagramInformation
        {
            get
            {
                return new List<InstaInfo>(_InstagramInformation);
            }
        }
        public List<EmoInfo> EmotionsInformation
        {
            get
            {
                return new List<EmoInfo>(_EmotionsInformation);
            }
        }
        public double AveragePeopleOnPhoto
        {
            get
            {
                return _EmotionsInformation.Average(x => x.Emotions.Count);
            }
        }
        public double AverageLikes
        {
            get
            {
                return _InstagramInformation.Average(x => x.LikesCount);
            }
        }
        public int MaxLikes
        {
            get
            {
                return _InstagramInformation.Max(x => x.LikesCount);
            }
        }
        public string NameOfMaxWeightEmo
        {
            get
            {
                double max = 0;
                EmotionsType emotion = EmotionsType.Happiness;
                foreach(EmotionsType key in _EmotionsWeights.Keys)
                {
                    if(max < _EmotionsWeights[key])
                    {
                        max = _EmotionsWeights[key];
                        emotion = key;
                    }
                }
                return EmoUtils.GetEmotionLabel(emotion);
            }
        }
        //Returns weight of emotion in profile (sum = 1)
        public double GetEmotionWeight(EmotionsType type)
        {
            return _EmotionsWeights[type];
        }
        public string GetUrlPhotoWithEmotion(EmotionsType type)
        {
            string url = string.Empty;
            double maxEmotionScore = 0;
            for(int i = 0; i < _EmotionsInformation.Count; i++)
            {
                EmoInfo emotInf = _EmotionsInformation[i];
                double currentEmotionScore = 0;
                int facesCount = emotInf.Emotions.Count;
                //если человек на фотографии не один - продолжаем дальше
                if (facesCount > 1)
                    continue;
                for (int j = 0; j < emotInf.Emotions.Count; j++)
                {
                    currentEmotionScore += EmoUtils.GetEmoScore(emotInf.Emotions[j], type);
                }
                if(currentEmotionScore > maxEmotionScore)
                {
                    maxEmotionScore = currentEmotionScore;
                    url = emotInf.ImageUrl;
                }
            }
            return url;
        }
        public int GetPhotosWithoutPeopleCount()
        {
            int count = 0;
            for(int i = 0; i < _EmotionsInformation.Count; i++)
            {
                if(_EmotionsInformation[i].Emotions.Count == 0)
                {
                    count++;
                }
            }
            return count;
        }
        #endregion
        public async Task LoadProfileData(
            string userName, 
            string apiKey, 
            bool isApiKeyTrial = true)
        {
            _InstagramInformation = await InstaUtils.GetInfoPostList(userName);
            _EmotionsInformation = await EmoUtils.GetEmoData(
                userName, 
                InstagramInformation, 
                apiKey, 
                isApiKeyTrial);
            _Name = userName;
            _AvatarUrl = InstagramInformation[0].ProfileImageUrl;
            _EmotionsWeights = CalculateEmotionsWeights();
        }
        private Dictionary<EmotionsType, double> CalculateEmotionsWeights()
        {
            Dictionary<EmotionsType, double> weights = new Dictionary<EmotionsType, double>();
            //Calculate weights
            foreach (EmotionsType emoType in Enum.GetValues(typeof(EmotionsType)))
            {
                weights[emoType] = 0;
                for(int i = 0; i < _EmotionsInformation.Count; i++)
                {
                    var emotions = _EmotionsInformation[i].Emotions;
                    for (int j = 0; j < emotions.Count; j++)
                    {
                        weights[emoType] 
                            += EmoUtils.GetEmoScore(emotions[j], emoType) * 1000;
                    }
                }
            }
            //Calculate sum
            double sum = 0;
            var keys = weights.Keys;
            foreach (var key in keys)
            {
                sum += weights[key];
            }
            //Normalize weight
            foreach(var key in keys.ToList())
            {
                double val = weights[key];
                weights[key] = val / sum;
            }
            return weights;
        }
    }
}
