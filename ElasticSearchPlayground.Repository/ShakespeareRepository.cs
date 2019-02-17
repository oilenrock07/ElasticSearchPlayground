using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticSearchPlayground.Models;
using ElasticSearchPlayground.Repository.Models;
using Nest;

namespace ElasticSearchPlayground.Repository
{
    public class ShakespeareRepository : ElasticSearchBaseRepository
    {
        protected override string Url => "http://localhost:9200/";
        protected override string Index => "shakespeare";
        protected override string Type => "doc";

        public async Task<ICollection<Shakespeare>> Search(IElasticSearchFilter filters)
        {
            SetConnectionSettings();
            await EnsureConnected().ConfigureAwait(false);

            var result = await Client.SearchAsync<Shakespeare>(s => Selector(s, filters)).ConfigureAwait(false);
            return result.Documents.ToList();
        }

        private void SetConnectionSettings()
        {
            if (ConnectionSettings == null)
            {
                ConnectionSettings = new ConnectionSettings().DefaultMappingFor<Shakespeare>(i => i.IndexName(Index).TypeName(Type));
            }
        }

        private ISearchRequest Selector(SearchDescriptor<Shakespeare> arg, IElasticSearchFilter filter)
        {
            var descriptor = arg.Query(q => q.Bool(b =>
            {
                var musts = new List<Func<QueryContainerDescriptor<Shakespeare>, QueryContainer>>();

                musts.Add(m => m.MatchPhrase(x => x.Field(f => f.Speaker)
                    .Query("cornelio")
                    .Boost(filter.GetSearchBoost(nameof(Shakespeare.Speaker)))));

                return b.Must(musts);
            }));
            return descriptor;
        }
    }
}
