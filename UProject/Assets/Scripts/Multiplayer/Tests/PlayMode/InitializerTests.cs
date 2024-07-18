using System.Threading.Tasks;
using NUnit.Framework;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace STak4.brickout.Multiplayer.Tests.PlayMode
{
    public class InitializerTests
    {
        [Test]
        public async Task InitializePass()
        {
            var profileName = "tester1";
            if (UnityServices.State == ServicesInitializationState.Initialized)
            {
                Debug.Log($"[TEST][Initializer] Already initialized by other tests");
                profileName = AuthenticationService.Instance.Profile;
            }
            else
            {
                var initializer = new UgsInitializer(new UgsEnvironment(), new UgsProfile(profileName));
                await initializer.InitializeTask();
            }

            Assert.That(UnityServices.State, Is.EqualTo(ServicesInitializationState.Initialized));
            Assert.That(AuthenticationService.Instance.Profile, Is.EqualTo(profileName));
        }
    }
}
