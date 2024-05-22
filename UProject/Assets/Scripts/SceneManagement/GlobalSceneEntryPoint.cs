using System.Threading;
using Cysharp.Threading.Tasks;
using MackySoft.Navigathena.SceneManagement;
using UnityEngine;

namespace STak4.brickout.SceneManagement
{
    public class GlobalSceneEntryPoint : SceneEntryPointBase
    {
        [SerializeField] private string startScene;

        // 遷移終了後
        protected override async UniTask OnEnter(ISceneDataReader reader, CancellationToken cancellationToken)
        {
            ISceneIdentifier identifier = new BuiltInSceneIdentifier(startScene);
            await GlobalSceneNavigator.Instance.Push(identifier);
        }
    }

}
