namespace ElasticSearchPlayground.Repository.Models
{
    public class SearchBoost
    {
        public string FieldName { get; set; }
        public double? Boost { get; set; }
    }
}
