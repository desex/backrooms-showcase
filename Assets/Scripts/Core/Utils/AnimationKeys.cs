using UnityEngine;

namespace Core.Utils
{
	public static class AnimationKeys
	{ 
		public static readonly int Jump = Animator.StringToHash("Jump"); 
		public static readonly int Dead = Animator.StringToHash("Dead"); 
		public static readonly int HitObject = Animator.StringToHash("HitObject"); 
		public static readonly int ShortSlide = Animator.StringToHash("ShortSlide"); 
		public static readonly int LongSlide = Animator.StringToHash("LongSlide"); 
		public static readonly int MonkeyJump = Animator.StringToHash("MonkeyJump"); 
		public static readonly int LongMonkeyJump = Animator.StringToHash("LongMonkeyJump");
        public static readonly int Running = Animator.StringToHash("Running");
        public static readonly int RunInjured = Animator.StringToHash("RunInjured");
        public static readonly int TurningLeft = Animator.StringToHash("TurningLeft");
        public static readonly int TurningRight = Animator.StringToHash("TurningRight");
		public static readonly int RunHitWall = Animator.StringToHash("RunHitWall");
		public static readonly int Roll = Animator.StringToHash("Roll");
		public static readonly int ControllableSlide = Animator.StringToHash("ControllableSlide");
	}
}