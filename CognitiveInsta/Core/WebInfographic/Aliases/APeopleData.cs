using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveInsta.Core.WebInfographic.Aliases
{
    class APeopleData : Alias
    {
        public override string GetResult(ProfileInfo info)
        {
            string result = WEB_DATA_LIST_BEGIN;
            int photoWithoutPeopleCount = info.GetPhotosWithoutPeopleCount();
            result = string.Concat(
                result,
                photoWithoutPeopleCount.ToString(),
                ",",
                (info.InstagramInformation.Count - photoWithoutPeopleCount).ToString());
            return string.Concat(result, WEB_DATA_LIST_END);
        }
    }
}
