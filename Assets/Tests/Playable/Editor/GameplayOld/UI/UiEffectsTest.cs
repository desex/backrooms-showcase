using System;
using System.Collections;
using System.Linq;
using Db.Effects;
using Game.UI.Effects;
using Tests.Helpers.Mock;
using Tests.Playable.UITest.Conditions;
using Tests.Playable.Utils;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using ZenjectUtil.Test.Extensions;
using Object = UnityEngine.Object;

namespace Tests.Playable.Gameplay.UI
{
    public class UiEffectsTest : AIntegrationTest
    {
        private const string BLOOD_EFFECT_TEST_SCENE = "BloodEffect_TestScene";
        
        private Mock.UiEffectsParameters _uiEffectsParameters;

        protected override void SubstituteResources()
        {
            _uiEffectsParameters = new Mock.UiEffectsParameters();
            ResourceSubstitute.AddSubstitute<IUiEffectsParameters>(_uiEffectsParameters);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.UI)]
        public IEnumerator BloodEffectShowAfterDamageAndHideAfterRegeneration()
        {
            //ScenesBase.SetScene(BLOOD_EFFECT_TEST_SCENE);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(1f);

            yield return PlayerAssert.IsNotDamaged();

            yield return AssertEffectEnabled(EUiEffectType.BloodEffect, false);
            
            yield return PlayerAssert.IsDamaged();
            
            yield return AssertEffectEnabled(EUiEffectType.BloodEffect, true);
            
            yield return GameplayUtils.SmoothSwipe(Vector2.right);

            for (var i = 0; i < 3; i++)
            {
                yield return PlayerAssert.IsDamaged(1f);
                yield return AssertEffectEnabled(EUiEffectType.BloodEffect, true);
                
                yield return new WaitForSeconds(0.5f);
            }
            
            yield return PlayerAssert.IsNotDamaged();
            
            yield return AssertEffectEnabled(EUiEffectType.BloodEffect, false);

            yield return new WaitForSeconds(2f);
        }

        private IEnumerator AssertEffectEnabled(EUiEffectType effectType, bool isEnabled, float timeOut = 1f)
        {
            yield return AssertUiViewActive<UiEffectsView>();
            
            var uiEffectsView = Object.FindObjectOfType<UiEffectsView>();

            var condition = effectType switch
            {
                EUiEffectType.BloodEffect => CreateBloodEffectCondition(),
                _ => throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null)
            };

            yield return WaitFor(condition, timeOut);

            BoolCondition CreateBloodEffectCondition()
            {
                return new(() =>
                    {
                        var bloodEffectImage = uiEffectsView.GetComponentsInChildren<Image>()
                                                            .First(image => image.name.Contains("BloodEffect"));
                        var currentAlpha = bloodEffectImage.color.a;

                        if (isEnabled)
                            return currentAlpha >= _uiEffectsParameters.BloodEffect.BlinkMinAlpha;

                        return currentAlpha < _uiEffectsParameters.BloodEffect.BlinkMinAlpha;
                    }
                );
            }
        }
    }
}