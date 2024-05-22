using MackySoft.Navigathena.SceneManagement.VContainer;
using VContainer;
using VContainer.Unity;

namespace STak4.brickout.SceneManagement
{
    public class SetupLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterSceneLifecycle<SetupSceneLifecycle>();
        }
    }
}
