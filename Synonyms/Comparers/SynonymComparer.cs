using Synonyms.Models;
using System;
using System.Collections.Generic;

namespace Synonyms.Comparers
{
    class SynonymComparer : IEqualityComparer<SynonymItem>
    {
        public bool Equals(SynonymItem a, SynonymItem b)
        {
            if ((a == null && b != null) ||
                (a != null && b == null)) return false;

            return ((a == null && b == null) ||
                    (a.Term == b.Term && a.Synonym == b.Synonym) ||
                    (a.Term == b.Synonym && a.Synonym == b.Term));
        }

        public int GetHashCode(SynonymItem item)
        {
            int hash = 0;
            var joined = item.Term + "|" + item.Synonym;
            for (int i = 0; i < joined.Length; i++)
            {
                var c = joined[i] - '0';
                hash += (int)Math.Pow(1.2, c);
            }
            return hash;
        }
    }

}
