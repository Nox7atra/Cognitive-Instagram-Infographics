using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveInsta.Core.WebInfographic.Aliases
{
    public abstract class Alias
    {
        public static readonly double MINIMUM_WEIGHT_VALUE = 0;
        public static readonly string WEB_DATA_LIST_BEGIN = "[";
        public static readonly string WEB_DATA_LIST_END = "]";
        public static readonly int LIKES_FROM_LAST_POST_COUNT = 90;
        public static readonly int EMO_PHOTOS_COUNT = 2;
        public static readonly string ALIAS_PREFIX = "$";
        public abstract string GetResult(ProfileInfo info);
    }
}
