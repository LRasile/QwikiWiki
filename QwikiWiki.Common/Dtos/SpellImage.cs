using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QwikiWiki.Common.Dtos
{
    public class SpellImage
    {

        public string full { get; set; }
        public string sprite{ get; set; }
        public string group{ get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int h{ get; set; }
        public int w{ get; set; }

        //   "w": 48,
        //   "full": "XenZhaoComboTarget.png",
        //   "sprite": "spell12.png",
        //   "group": "spell",
        //   "h": 48,
        //   "y": 96,
        //   "x": 432
    }
}
