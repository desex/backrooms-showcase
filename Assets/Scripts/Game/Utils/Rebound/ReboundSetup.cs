namespace Game.Utils.Rebound
{
    public readonly struct ReboundSetup
    {
        public readonly EReboundDirection ReboundDirection;
        public readonly EReboundAngle ReboundAngle;
        public readonly EReboundForce ReboundForce;
        public readonly EReboundForce DownReboundForce;

        public ReboundSetup(
            EReboundDirection reboundDirection,
            EReboundAngle reboundAngle,
            EReboundForce reboundForce,
            EReboundForce downReboundForce
        )
        {
            ReboundDirection = reboundDirection;
            ReboundAngle = reboundAngle;
            ReboundForce = reboundForce;
            DownReboundForce = downReboundForce;
        }
    }
}