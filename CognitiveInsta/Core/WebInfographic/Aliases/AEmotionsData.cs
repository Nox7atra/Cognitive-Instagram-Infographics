using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveInsta.Core.WebInfographic.Aliases
{
    public class AEmotionsData : Alias
    {
        public override string GetResult(ProfileInfo info)
        {
            string result = WEB_DATA_LIST_BEGIN;
            for (int i = 0; i < Enum.GetValues(typeof(EmotionsType)).Length; i++)
            {
                double weight = info.GetEmotionWeight((EmotionsType)i);
                if (weight > MINIMUM_WEIGHT_VALUE)
                    result = string.Concat(
                        result,
                        weight.ToString(),
                        ",");
            }
            return string.Concat(result, WEB_DATA_LIST_END);
        }
    }
}
