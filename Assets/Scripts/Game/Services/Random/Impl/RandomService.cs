using System.Collections.Generic;
using PdUtils.RandomProvider;

namespace Game.Services.Random.Impl
{
    public sealed class RandomService : IRandomService
    {
        private readonly IRandomProvider _randomProvider;

        public RandomService(IRandomProvider randomProvider)
        {
            _randomProvider = randomProvider;
        }

        public TType GetWeightedRandom<TType>(IReadOnlyDictionary<TType, int> weightedValues)
        {
            var totalWeight = 0;

            foreach (var weight in weightedValues.Values)
                totalWeight += weight;

            totalWeight++; //add this because Range() exclusive for the last value

            var randomNumber = _randomProvider.Range(0, totalWeight);
            var searchValue = 0;

            foreach (var typeWeightPair in weightedValues)
            {
                searchValue += typeWeightPair.Value;

                if (searchValue >= randomNumber)
                    return typeWeightPair.Key;
            }

            return default;
        }
    }
}