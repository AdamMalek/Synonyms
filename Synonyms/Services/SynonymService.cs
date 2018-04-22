using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Options;
using Synonyms.Models;
using Synonyms.Comparers;
using Synonyms.Models.Entities;
using Synonyms.Models.Options;
using System;

namespace Synonyms.Services
{
    class SynonymService : ISynonymService
    {
        private readonly IOptions<ConnectionStrings> _connectionStrings;

        public SynonymService(IOptions<ConnectionStrings> connStr)
        {
            _connectionStrings = connStr;
        }

        public void AddSynonym(string term, string synonyms)
        {
            term = term?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new ArgumentException("Invalid term");
            }

            synonyms = synonyms?.Replace(term, "") ?? "";
            synonyms = string.Join(",",
                                   synonyms.Split(",", System.StringSplitOptions.RemoveEmptyEntries)
                                            .Select(x => x.Trim().ToLower())
                                            .Distinct()
                                   );

            if (string.IsNullOrWhiteSpace(synonyms))
            {
                throw new ArgumentException("Invalid synonyms");
            }

            using (var conn = new SqlConnection(_connectionStrings.Value.SynonymsDb))
            {
                conn.Execute("insert into Synonyms (Term, Synonyms) values (@term, @synonyms)",
                              new
                              {
                                  term,
                                  synonyms
                              });
            }
        }

        public Dictionary<string, string[]> GetAll()
        {
            using (var conn = new SqlConnection(_connectionStrings.Value.SynonymsDb))
            {
                var items = conn.Query<SynonymEntity>("select * from Synonyms");

                return items.GroupBy(x => x.Term)
                            .SelectMany(x => x.SelectMany(y => y.Synonyms.Split(','))
                                                .Distinct()
                                                .Select(y => new SynonymItem { Term = x.Key, Synonym = y })
                                        )
                            .Distinct(new SynonymComparer())
                            .SelectMany(x => new[] { x, x.Inverse() })
                            .OrderBy(x => x.Term)
                            .GroupBy(x => x.Term)
                            .ToDictionary(x => x.Key,
                                          x => x.Select(y => y.Synonym)
                                                .OrderBy(y => y)
                                                .ToArray()
                                          );
            }
        }
    }
}
