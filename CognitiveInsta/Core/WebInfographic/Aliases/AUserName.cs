using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveInsta.Core.WebInfographic.Aliases
{
    public class AUserName : Alias
    {
        public override string GetResult(ProfileInfo info)
        {
            return info.Name;
        }
    }
}
