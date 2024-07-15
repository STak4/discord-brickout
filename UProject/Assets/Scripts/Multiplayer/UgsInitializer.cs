using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace STak4.brickout.Multiplayer
{
    /// <summary>
    /// Unity Gaming Servicesの初期化にまつわるクラス
    /// </summary>
    public class UgsInitializer
    {
        private readonly UgsEnvironment _environment;
        private readonly UgsProfile _profile;
        public UgsInitializer(UgsEnvironment env, UgsProfile profile)
        {
            _environment = env;
            _profile = profile;
        }

        public UniTask InitializeTask(CancellationToken token = default)
        {
            if (UnityServices.State != ServicesInitializationState.Uninitialized)
            {
                Debug.LogError($"[UGS][Initializer] Already initialized!");
                throw new ServicesInitializationException("Already Initialized");
            }

            var options = new InitializationOptions()
                .SetEnvironmentName(_environment.GetEnvName())
                .SetProfile(_profile.GetProfileName());
            
            return UnityServices.InitializeAsync(options).AsUniTask().AttachExternalCancellation(token);
        }
    }
}
