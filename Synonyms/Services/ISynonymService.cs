using Synonyms.Models.Entities;
using System.Collections.Generic;

namespace Synonyms.Services
{
    public interface ISynonymService
    {
        void AddSynonym(string term, string synonyms);

        Dictionary<string, string[]> GetAll();
    }
}
