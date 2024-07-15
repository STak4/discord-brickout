using System;
using MackySoft.Navigathena.SceneManagement;
using STak4.brickout.Multiplayer;
using STak4.brickout.UI;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using VContainer.Unity;

namespace STak4.brickout.Presenter
{
    public class TitlePresenter : IStartable
    {
        private TitleView _view;

        public TitlePresenter(TitleView view)
        {
            _view = view;
        }

        public async void Start()
        {
            _view.NextButton.onClick.AddListener(OnNext);
            _view.NameInput.onEndEdit.AddListener(OnEndEdit);

            // UGSの初期化
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                try
                {
                    var ugsInitializer = new UgsInitializer(new UgsEnvironment(), new UgsProfile(_view.NameInput.text));
                    await ugsInitializer.InitializeTask();
                    Debug.Log($"[UGS][Title]Initialization:{UnityServices.State}");
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    throw;
                }
            }
        }

        private async void OnNext()
        {
            var auth = new UgsAuth();
            var profile = new UgsProfile(_view.NameInput.text);
            try
            {
                await auth.SwitchProfile(profile);
                Debug.Log($"[UGS][Title]Signin({AuthenticationService.Instance.Profile}):{AuthenticationService.Instance.IsSignedIn}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var identifier = new BuiltInSceneIdentifier("lobby");
            await GlobalSceneNavigator.Instance.Push(identifier);
        }

        private void OnEndEdit(string input)
        {
            _view.NextButton.interactable = !string.IsNullOrEmpty(input);
        }
    }
}
