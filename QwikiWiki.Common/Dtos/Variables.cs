using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QwikiWiki.Common.Dtos
{
    public class Variables
    {
        public string key { get; set; }
        public string link { get; set; }
        public float[] coeff { get; set; }
        //   "link": "attackdamage",
        //   "coeff": [1.25],
        //   "key": "f1"
    }
}
