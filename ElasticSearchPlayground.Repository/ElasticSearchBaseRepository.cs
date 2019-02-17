using System;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearchPlayground.Repository
{
    public abstract class ElasticSearchBaseRepository
    {
        protected abstract string Url { get; }
        protected abstract string Index { get;  }
        protected abstract string Type { get; }

        public ConnectionSettings ConnectionSettings { get; set; }

        private ElasticClient _client;
        public ElasticClient Client
        {
            get => _client;
            set => _client = value;
        }


        protected Task EnsureConnected()
        {
            if (_client != null) return Task.CompletedTask;

            //set default connection settings
            if (ConnectionSettings == null)
            {
                var uri = new Uri(Url);
                ConnectionSettings = new ConnectionSettings(uri)
                    .DefaultIndex(Index)
                    .DefaultTypeName(Type);
            }

            _client = new ElasticClient(ConnectionSettings);

            return Task.CompletedTask;
        }
    }
}
