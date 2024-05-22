using MackySoft.Navigathena.SceneManagement;
using UnityEngine;
using VContainer.Unity;

namespace STak4.brickout.Presenter
{
    public class InGamePresenter : IStartable
    {
        private InGameView _view;
        public InGamePresenter(InGameView view)
        {
            _view = view;
        }
        
        public void Start()
        {
            _view.backButton.onClick.AddListener(OnBack);
        }

        private async void OnBack()
        {
            await GlobalSceneNavigator.Instance.Pop();
        }
    }
}
