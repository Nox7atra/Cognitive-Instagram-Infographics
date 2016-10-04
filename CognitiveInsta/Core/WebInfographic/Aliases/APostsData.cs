using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveInsta.Core.WebInfographic.Aliases
{
    public class APostsData : Alias
    {
        public override string GetResult(ProfileInfo info)
        {
            string result = WEB_DATA_LIST_BEGIN;
            int postCount = LIKES_FROM_LAST_POST_COUNT;
            if (postCount > info.InstagramInformation.Count)
            {
                postCount = info.InstagramInformation.Count;
            }
            for (int i = 0; i < postCount; i++)
            {
                result = string.Concat(
                    result,
                    (i + 1).ToString(),
                    ",");
            }
            return string.Concat(result, WEB_DATA_LIST_END);
        }
    }
}
