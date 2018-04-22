using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synonyms.Models
{
    internal class SynonymItem
    {
        public string Term { get; set; }
        public string Synonym { get; set; }

        public SynonymItem Inverse()
        {
            return new SynonymItem
            {
                Term = Synonym,
                Synonym = Term,
            };
        }
    }
}
