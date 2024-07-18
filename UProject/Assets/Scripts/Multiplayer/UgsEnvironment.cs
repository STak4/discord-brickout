using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STak4.brickout.Multiplayer
{
    /// <summary>
    /// Unity Gaming Servicesの環境にまつわるクラス
    /// </summary>
    public class UgsEnvironment
    {
#if ENV_PROD
        private readonly string envName = "production";
#elif ENV_STG
        private readonly string envName = "stg";
#elif ENV_DEV
        private readonly string envName = "dev";
#else
        private readonly string envName = "undefined";
#endif
        public UgsEnvironment()
        {
        }

        public string GetEnvName()
        {
            return envName;
        }
    }
}