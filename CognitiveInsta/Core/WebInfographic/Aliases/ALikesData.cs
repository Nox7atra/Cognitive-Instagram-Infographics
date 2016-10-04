using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveInsta.Core.WebInfographic.Aliases
{
    class ALikesData : Alias
    {
        public override string GetResult(ProfileInfo info)
        {
            string result = WEB_DATA_LIST_BEGIN;
            int postCount = LIKES_FROM_LAST_POST_COUNT;
            if (postCount > info.InstagramInformation.Count)
            {
                postCount = info.InstagramInformation.Count;
            }
            for (int i = postCount - 1; i >= 0; i--)
            {
                result = string.Concat(
                    result,
                    info.InstagramInformation[i].LikesCount.ToString(),
                    ",");
            }
            return string.Concat(result, WEB_DATA_LIST_END);
        }
    }
}
