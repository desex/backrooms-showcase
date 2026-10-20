using Db.SoundFxBase;
using Ecs.Managers;

namespace Game.Services.Ghosts.Sound
{
    public interface IGhostSoundDispatcher
    {
        public void AddGhostSoundService(Uid ghostUid);
        public void StopMoveActionSounds(Uid targetGhostUid);
        public void StopCharacterEffectSounds(Uid targetGhostUid);
        public void PlaySound(Uid targetGhostUid, ESoundFxName soundFxName);
        public void StopSound(Uid targetGhostUid, ESoundFxName soundFxName);
    }
}