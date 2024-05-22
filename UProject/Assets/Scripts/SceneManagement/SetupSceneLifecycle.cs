using System.Threading;
using Cysharp.Threading.Tasks;
using MackySoft.Navigathena.SceneManagement;
using MackySoft.Navigathena.SceneManagement.VContainer;
using UnityEngine;

namespace STak4.brickout.SceneManagement
{
    public class SetupSceneLifecycle : SceneLifecycleBase
    {
        protected override async UniTask OnEnter(ISceneDataReader reader, CancellationToken cancellationToken)
        {
            Debug.Log($"[Debug][Start] OnEnter");
            await GoNext();
        }

        private async UniTask GoNext()
        {
            var identifier = new BuiltInSceneIdentifier("title");

            await GlobalSceneNavigator.Instance.Replace(identifier);
        }
    }
}
