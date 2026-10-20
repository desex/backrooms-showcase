using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Services.Providers;
using Game.Utils.Lights.Actions.Impl;
using UnityEngine;

namespace Game.Services.Lights.Impl.Commands.Impl.Parts
{
    public class ChangeColorLightEventAction : ALightEventAction<ChangeColorLightActionData>
    {
        private const string COLOR_NAME = "_BaseColor";
        private const string EMISSION_COLOR_NAME = "_EmissionColor";

        private static readonly int COLOR = Shader.PropertyToID(COLOR_NAME);
        private static readonly int EMISSION_COLOR = Shader.PropertyToID(EMISSION_COLOR_NAME);

        private readonly IGlobalIlluminationResourcesProvider _lightResourcesProvider;

        public ChangeColorLightEventAction(IGlobalIlluminationResourcesProvider lightResourcesProvider)
        {
            _lightResourcesProvider = lightResourcesProvider;
        }

        protected override async UniTask Execute(
            CancellationToken cancellationToken,
            ChangeColorLightActionData actionData
        )
        {
            // Более показать не могу
        }
    }
}