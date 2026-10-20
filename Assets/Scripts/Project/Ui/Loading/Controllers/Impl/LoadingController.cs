using Db.Loading;
using Game.Services.Render;
using Game.Services.SceneLoading;
using Game.Utils.Render;
using Project.Ui.Loading.Views;
using SimpleUi.Abstracts;
using UnityEngine;
using Zenject;

namespace Project.Ui.Loading.Controllers.Impl
{
	public class LoadingController : UiController<LoadingView>, ITickable
	{
		private readonly ISceneLoadingManager _sceneLoadingManager;
		private readonly ILoadingParameters _loadingParameters;
		private readonly ITvLinesService _tvLinesService;
        private readonly IWhiteNoiseService _whiteNoiseService;
        private readonly IGlitchService _glitchService;

        private int _dotsCount = 1;
		private int _tickCounter;
		private bool _isControllerClosed;
		private bool _isFakeCompleteLoadingStart;

		public LoadingController(
			ISceneLoadingManager sceneLoadingManager, 
			ILoadingParameters loadingParameters, 
			ITvLinesService tvLinesService,
            IWhiteNoiseService whiteNoiseService,
            IGlitchService glitchService
        )
		{
			_sceneLoadingManager = sceneLoadingManager;
			_loadingParameters = loadingParameters;
			_tvLinesService = tvLinesService;
            _whiteNoiseService = whiteNoiseService;
            _glitchService = glitchService;
        }
		
		public override void OnShow()
		{
			// Более показать не могу
		}
		
		public void Tick()
		{
			// Более показать не могу
		}

		private void FakeCompleteLoading()
		{
			// Более показать не могу
		}
		
		private void LoadingDotsUpdate()
		{
			// Более показать не могу
		}
		
		public override void OnHide()
		{
			// Более показать не могу
		}
	}
}