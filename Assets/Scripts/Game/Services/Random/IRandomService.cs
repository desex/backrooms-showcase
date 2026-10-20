using System.Collections.Generic;

namespace Game.Services.Random
{
    public interface IRandomService
    {
        TType GetWeightedRandom<TType>(IReadOnlyDictionary<TType, int> weightedValues);
    }
}