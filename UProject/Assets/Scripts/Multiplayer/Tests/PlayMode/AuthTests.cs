using System.Threading.Tasks;
using NUnit.Framework;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace STak4.brickout.Multiplayer.Tests.PlayMode
{
    public class AuthTests
    {
        public readonly string ProfileName = "tester1";
        [SetUp]
        public async void Setup()
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                var initializer = new UgsInitializer(new UgsEnvironment(), new UgsProfile(ProfileName));
                await initializer.InitializeTask();
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (AuthenticationService.Instance.IsSignedIn)
            {
                AuthenticationService.Instance.SignOut();
            }
        }
        
        [Test]
        public async Task SignInPass()
        {
            var auth = new UgsAuth();
            await auth.SignIn();
            Assert.That(AuthenticationService.Instance.IsSignedIn, Is.EqualTo(true));
            Assert.That(AuthenticationService.Instance.Profile, Is.EqualTo(ProfileName));
        }

        [Test]
        public async Task SignOutPass()
        {
            var auth = new UgsAuth();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await auth.SignIn();
            }

            auth.SignOut();
            Assert.That(AuthenticationService.Instance.IsSignedIn, Is.EqualTo(false));
        }

        [Test]
        public async Task SwitchProfilePass()
        {
            var secondProfile = "tester2";
            var auth = new UgsAuth();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await auth.SignIn();
            }
            
            await auth.SwitchProfile(new UgsProfile(secondProfile));
            Assert.That(AuthenticationService.Instance.IsSignedIn, Is.EqualTo(true));
            Assert.That(AuthenticationService.Instance.Profile, Is.EqualTo(secondProfile));
        }
    }
}
