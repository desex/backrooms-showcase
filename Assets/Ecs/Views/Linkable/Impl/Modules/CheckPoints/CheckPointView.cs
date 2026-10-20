using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.Checkpoints;
using Db.SoundFxBase;
using Game.Services.CheckPoints;
using Game.Services.Score;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.Checkpoint;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.CheckPoints
{
    public class CheckPointView : ObjectView
    {
        [field: Title("Triggers")]
        [SerializeField] private BoxCollider _enterTrigger;

        [Title("Shader Data")]
        [SerializeField] private Renderer[] _renderers;

        private static readonly int InputTextureShaderId = Shader.PropertyToID("_BaseMap");
        private static readonly int ColumnCountShaderId = Shader.PropertyToID("_ColumnCount");
        private static readonly int RowCountShaderId = Shader.PropertyToID("_RowCount");
        private static readonly int FpsCountShaderId = Shader.PropertyToID("_FPS");
        private static readonly int FramesCountShaderId = Shader.PropertyToID("_FramesCount");
        private static readonly int IsStartedShaderId = Shader.PropertyToID("_StartAnimation");
        private static readonly int StartTimeShaderId = Shader.PropertyToID("_StartTime");
        private static readonly int ShouldLoopShaderId = Shader.PropertyToID("_ShouldLoop");

        private MaterialPropertyBlock _materialPropertyBlock;
        private IScoreService _scoreService;
        private ISoundService _soundService;
        private GameEntity _self;
        private CancellationTokenSource _token;
        private ICheckpointsParameters _checkpointsParameters;
        private ICheckpointScoreService _checkpointScoreService;

        [Inject]
        private void Construct(
            ISoundService soundService,
            IScoreService scoreService,
            ICheckpointScoreService checkpointScoreService,
            ICheckpointsParameters checkpointsParameters
        )
        {
            // Более показать не могу
        }

        private void OnDisable()
        {
            // Более показать не могу
        }

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void ChangeState(GameEntity entity, ECheckpointTvState value)
        {
            // Более показать не могу
        }

        private void TriggerEnter(Collider other)
        {
            // Более показать не могу
        }

        private void ChangeScore()
        {
            // Более показать не могу
        }

        private void Active(bool isActive) {
            // Более показать не могу
        }

        private void PlayOff()
        {
            // Более показать не могу
        }

        private async void PlayNoise()
        {
            // Более показать не могу
        }

        private async void PlayInformation()
        {
            // Более показать не могу
        }

        private async void PlayNewRecord()
        {
            // Более показать не могу
        }

        private void SetShaderProperties(Texture2D texture2D, int rowCount, int columnCount, bool isLoop)
        {
            // Более показать не могу
        }

        private float GetDuration(int rowCount, int columnCount)
        {
            // Более показать не могу
            return default;
        }
    }
}