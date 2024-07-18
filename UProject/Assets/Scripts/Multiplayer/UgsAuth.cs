using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace STak4.brickout
{
    public class UgsAuth
    {
        private UgsProfile _profile;
        public UgsAuth()
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                Debug.LogError($"[UGS] Not Initialized!");
                return;
            }

            _profile = new UgsProfile(AuthenticationService.Instance.Profile);
        }

        public UniTask SignIn(CancellationToken token = default)
        {
            if (AuthenticationService.Instance.IsSignedIn)
            {
                Debug.LogError($"[UGS] Already signed in");
                throw new Exception("[UGS]Already Signed in");
            }
            
            var options = new SignInOptions
            {
                CreateAccount = true
            };

            return AuthenticationService.Instance.SignInAnonymouslyAsync(options).AsUniTask().AttachExternalCancellation(token);
        }

        public void SignOut(bool clearCredentials = false)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.LogError($"[UGS] Failed to sign-out. not signed in");
                throw new Exception("[UGS] Failed to sign-out. not signed in");
            }
            AuthenticationService.Instance.SignOut(clearCredentials);
        }

        public UniTask SwitchProfile(UgsProfile profile, CancellationToken token = default)
        {
            if (AuthenticationService.Instance.IsSignedIn)
            {
                SignOut();
            }
            AuthenticationService.Instance.SwitchProfile(profile.GetProfileName());

            return SignIn(token);
        }
    }
}
