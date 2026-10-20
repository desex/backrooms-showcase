namespace Ecs.Core.Interfaces
{
	public interface ITimeProvider
	{
		float Time { get; }
		float DeltaTime { get; }
		float UnscaledDeltaTime { get; }
		float FixedDeltaTime { get; }
		float TimeScale { get; }
	}
}