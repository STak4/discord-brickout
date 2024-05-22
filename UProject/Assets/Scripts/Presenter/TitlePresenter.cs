using MackySoft.Navigathena.SceneManagement;
using STak4.brickout.UI;
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

        public void Start()
        {
            _view.NextButton.onClick.AddListener(OnNext);
        }

        private async void OnNext()
        {
            var identifier = new BuiltInSceneIdentifier("lobby");
            await GlobalSceneNavigator.Instance.Push(identifier);
        }
    }
}
