using Albelli.Infrastructure.Caching;

namespace Albelli.Infrastructure.Domain.ForeignExchanges
{
    public class ConversionRatesCacheKey : ICacheKey<ConversionRatesCache>
    {
        public string CacheKey => "ConversionRatesCache";
    }
}