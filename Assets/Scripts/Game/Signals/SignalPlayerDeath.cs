using Libs.Analytics;

namespace Game.Signals
{
    public class SignalPlayerDeath : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;
        
        public string DeathReason;
        public string ChunkName;
        public string MoveAtDeath;
        public string PlaceWhereThePlayerDied;
        public string LightEvent;
        public string Screamer;
        public string PreviousObstacle;
        public string SequenceName;

        public SignalPlayerDeath(
            string deathReason,
            string chunkName,
            string moveAtDeath,
            string placeWhereThePlayerDied,
            string lightEvent,
            string screamer,
            string previousObstacle,
            string sequenceName
        )
        {
            DeathReason = deathReason;
            ChunkName = chunkName;
            MoveAtDeath = moveAtDeath;
            PlaceWhereThePlayerDied = placeWhereThePlayerDied;
            LightEvent = lightEvent;
            Screamer = screamer;
            PreviousObstacle = previousObstacle;
            SequenceName = sequenceName;
        }
    }
}