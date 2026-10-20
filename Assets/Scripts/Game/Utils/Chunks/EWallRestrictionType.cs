namespace Game.Utils.Chunks
{
    //If you change something here then you must change the same here:
    //https://docs.google.com/spreadsheets/d/REDACTED/edit?gid=690196057#gid=690196057
    public enum EWallRestrictionType
    {
        None = 0,
        IfAnotherSideIs = 1,
        IfAnotherSideIsNot = 2,
    }
}