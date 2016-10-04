using System.Collections.Generic;
using Microsoft.ProjectOxford.Emotion.Contract;
namespace CognitiveInsta.Core
{
    public class EmoInfo
    {
        public int LikesCount;
        public int CommentsCount;
        public string ProfileFullName;
        public string ImageUrl;
        public List<Emotion> Emotions;
    }
}
