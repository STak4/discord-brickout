using UnityEngine;
using VContainer.Unity;

namespace STak4.brickout.DI
{
    public class SamplePresenter : IStartable
    {
        private readonly HelloWorldService _helloWorldService;
        private readonly SampleView _sampleView;

        public SamplePresenter(HelloWorldService helloWorldService, SampleView sampleView)
        {
            _helloWorldService = helloWorldService;
            _sampleView = sampleView;
        }

        public void Start()
        {
            _sampleView.helloButton.onClick.AddListener(() => _helloWorldService.Hello());
        }
    }
}
