using System.Threading;
using Cysharp.Threading.Tasks;
using MackySoft.Navigathena.SceneManagement.Utilities;
using MackySoft.Navigathena.SceneManagement.VContainer;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace STak4.brickout.SceneManagement
{
    public class SetupScopedSceneEntryPoint : ScopedSceneEntryPoint
    {
        const string kRootSceneName = "common";

        protected override async UniTask<LifetimeScope> EnsureParentScope (CancellationToken cancellationToken)
        {
            // Load root scene.
            if (!SceneManager.GetSceneByName(kRootSceneName).isLoaded)
            {
                await SceneManager.LoadSceneAsync(kRootSceneName, LoadSceneMode.Additive)
                    .ToUniTask(cancellationToken: cancellationToken);
            }

            Scene rootScene = SceneManager.GetSceneByName(kRootSceneName);

#if UNITY_EDITOR
            // Reorder root scene.
            UnityEditor.SceneManagement.EditorSceneManager.MoveSceneBefore(rootScene, gameObject.scene);
#endif

            // Build root LifetimeScope container.
            if (rootScene.TryGetComponentInScene(out LifetimeScope rootLifetimeScope, true) && rootLifetimeScope.Container == null)
            {
                await UniTask.RunOnThreadPool(() => rootLifetimeScope.Build(), cancellationToken: cancellationToken);
            }
            return rootLifetimeScope;
        }
    }
}
