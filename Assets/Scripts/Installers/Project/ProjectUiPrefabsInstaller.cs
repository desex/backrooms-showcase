using Project.Ui.Empty;
using Project.Ui.Loading.Controllers.Impl;
using Project.Ui.Loading.Views;
using SimpleUi;
using UnityEngine;
using Zenject;

namespace Installers.Project
{
	[CreateAssetMenu(menuName = "Installers/ProjectUiPrefabsInstaller", fileName = "ProjectUiPrefabsInstaller")]
	public class ProjectUiPrefabsInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private Canvas _canvas;

		[SerializeField] private EmptyView _emptyView;
		[SerializeField] private LoadingView _loadingView;
		[SerializeField] private ReloadingView _reloadingView;

		public override void InstallBindings()
		{
			var canvasView = Container.InstantiatePrefabForComponent<Canvas>(_canvas);
			var canvasTransform = canvasView.transform;

			Container.BindUiView<EmptyController, EmptyView>(_emptyView, canvasTransform);
			Container.BindUiView<LoadingController, LoadingView>(_loadingView, canvasTransform);
			Container.BindUiView<ReloadingController, ReloadingView>(_reloadingView, canvasTransform);
		}
	}
}