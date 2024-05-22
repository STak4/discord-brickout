using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace STak4.brickout.DI
{
    public class SampleLifetimeScope : LifetimeScope
    {
        [SerializeField] private SampleView sampleView;
        protected override void Configure(IContainerBuilder builder)
        {
            //ここで依存関係を構築する
            builder.Register<HelloWorldService>(Lifetime.Singleton);
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<SamplePresenter>();
            });
            builder.RegisterComponent(sampleView);
        }
    }

}
