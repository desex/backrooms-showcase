using System.Collections.Generic;
using Game.Utils.Lights;

namespace Db.PassObstacleFeedback
{
    public interface IPassObstacleFeedbackParameters
    {
        IReadOnlyDictionary<ELightEvent, ColorParameters> ColorParameters { get; }
        public ColorParameters DefaultColorParameters { get; }
    }
}