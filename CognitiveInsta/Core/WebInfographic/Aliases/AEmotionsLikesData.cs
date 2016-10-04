using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveInsta.Core.WebInfographic.Aliases
{
    public class AEmotionsLikesData : Alias
    {
        
        public override string GetResult(ProfileInfo info)
        {
            Dictionary<EmotionsType, double> emotionLikeWeight = CreateData(info);
            string result = WEB_DATA_LIST_BEGIN;
            for (int i = 0; i < Enum.GetValues(typeof(EmotionsType)).Length; i++)
            {
                double weight = info.GetEmotionWeight((EmotionsType)i);
                if (weight > MINIMUM_WEIGHT_VALUE)
                    result = string.Concat(
                        result,
                        emotionLikeWeight[(EmotionsType)i],
                        ",");
            }
            return string.Concat(result, WEB_DATA_LIST_END);
        }
        private Dictionary<EmotionsType, double> CreateData(ProfileInfo info)
        {
            Dictionary<EmotionsType, double> emotionLikeWeight 
                = new Dictionary<EmotionsType, double>();
            List<EmoInfo> emoInfo = info.EmotionsInformation;
            for (int i = 0; i < emoInfo.Count; i++)
            {
                for (int j = 0; j < Enum.GetValues(typeof(EmotionsType)).Length; j++)
                {
                    var faces = emoInfo[i].Emotions;
                    for (int k = 0; k < faces.Count; k++)
                    {
                        EmotionsType emoType = (EmotionsType)j;
                        if (!emotionLikeWeight.ContainsKey(emoType))
                        {
                            emotionLikeWeight.Add(emoType, 0);
                        }
                        emotionLikeWeight[emoType] =
                            EmoUtils.GetEmoScore(faces[k], emoType) * emoInfo[i].LikesCount;
                    }
                }
            }
            return emotionLikeWeight;
        }
    }
}
