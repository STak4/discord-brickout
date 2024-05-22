using MackySoft.Navigathena.SceneManagement;
using STak4.brickout.UI;
using UnityEngine;
using VContainer.Unity;

namespace STak4.brickout.Presenter
{
    public class LobbyPresenter : IStartable
    {
        private LobbyView _view;

        public LobbyPresenter(LobbyView view)
        {
            _view = view;
        }

        public void Start()
        {
            _view.NextButton.onClick.AddListener(OnNext);
        }

        private async void OnNext()
        {
            var identifier = new BuiltInSceneIdentifier("InGame");
            await GlobalSceneNavigator.Instance.Push(identifier);
        }
    }
}
