using MackySoft.Navigathena.SceneManagement.VContainer;
using STak4.brickout.Presenter;
using STak4.brickout.SceneManagement;
using STak4.brickout.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace STak4.brickout
{
    public class InGameLifetimeScope : LifetimeScope
    {
        [SerializeField] private InGameView titleView;
        
        protected override void Configure (IContainerBuilder builder)
        {
            builder.RegisterSceneLifecycle<InGameSceneLifecycle>();
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<InGamePresenter>();
            });
            builder.RegisterComponent(titleView);
        }
    }
}
