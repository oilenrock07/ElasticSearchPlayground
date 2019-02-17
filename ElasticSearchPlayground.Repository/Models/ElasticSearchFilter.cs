using System;
using System.Collections.Generic;
using System.Linq;

namespace ElasticSearchPlayground.Repository.Models
{
    public interface IElasticSearchFilter
    {
        ICollection<SearchBoost> SearchBoosts { get; set; }
        double? GetSearchBoost(string fieldName);
    }

    public class ElasticSearchFilter : IElasticSearchFilter
    {
        public ICollection<SearchBoost> SearchBoosts { get; set; }

        public double? GetSearchBoost(string fieldName)
        {
            return SearchBoosts?.FirstOrDefault(x =>
                    string.Equals(x.FieldName, fieldName, StringComparison.InvariantCultureIgnoreCase))
                ?.Boost;
        }
    }


}
