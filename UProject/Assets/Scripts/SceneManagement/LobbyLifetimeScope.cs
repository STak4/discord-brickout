using MackySoft.Navigathena.SceneManagement.VContainer;
using STak4.brickout.Presenter;
using STak4.brickout.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace STak4.brickout.SceneManagement
{
    public class LobbyLifetimeScope : LifetimeScope
    {
        [SerializeField] private LobbyView lobbyView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterSceneLifecycle<TitleSceneLifecycle>();
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<LobbyPresenter>();
            });
            builder.RegisterComponent(lobbyView);
        }
    }
}
