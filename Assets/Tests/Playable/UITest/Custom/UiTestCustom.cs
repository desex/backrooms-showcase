using JCMG.EntitasRedux;
using Tests.Playable.UITest.Custom.Conditions.Entity;

// ReSharper disable once CheckNamespace
namespace Plugins.UITest
{
	public abstract partial class UiTest
	{
		protected static EntityMatchCase<TEntity>.Builder AssertEntityMatch<TEntity>(string conditionName)
			where TEntity : class, IEntity
			=> new EntityMatchCase<TEntity>.Builder().SetConditionName(conditionName);
	}
}