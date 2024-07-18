using System.Collections;
using System.Collections.Generic;
using MackySoft.Navigathena.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace STak4.brickout
{
    public class ErrorDialog : MonoBehaviour, IWindow, IDialog
    {
        [SerializeField] private Transform rootContainer;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text promptText;
        [SerializeField] private Button closeButton;
        
        private List<IWindow> _listeners = new List<IWindow>();
        
        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            closeButton.onClick.AddListener(() => OnModalClose(new ModalContext(ModalContext.ModalType.Error,0,"Close by user")));
            rootContainer.gameObject.SetActive(false);
        }
        
        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            var message = $"[{type}]{logString}";
            Debug.Log($"[Debug] Handle log. {message}");
            if (type == LogType.Exception || type == LogType.Error)
            {
                OnModalOpen(new ModalContext(ModalContext.ModalType.Error,0,$"{message}"));
            }
        }

        #region IWindow

        public void Open(IWindowContext context)
        {
            var modalContext = new ModalContext(ModalContext.ModalType.Error, 0, $"{context.GetMessage()}");
            OnModalOpen(modalContext);
        }

        public void Close(IWindowContext context)
        {
            var modalContext = new ModalContext(ModalContext.ModalType.Error, 0, $"{context.GetMessage()}");
            OnModalClose(modalContext);
        }       

        #endregion


        #region IDialog
        public void AddListener(IWindow window)
        {
            _listeners.Add(window);
        }

        public void RemoveListener(IWindow window)
        {
            _listeners.Remove(window);
        }

        public void OnModalOpen(ModalContext context)
        {
            // モーダルを開くときはListenerを閉じる
            var eventContext = new OnModalEventContext(context, $"Modal Open. Message:{context.Message}");
            foreach (var l in _listeners)
            {
                l.Close(eventContext);
            }

            titleText.text = $"予期せぬエラーが発生しました。";
            promptText.text = $"タイトルに戻ります。エラー内容\n {context.Message}";
            
            rootContainer.gameObject.SetActive(true);
        }

        public async void OnModalClose(ModalContext context)
        {
            // モーダルを閉じるときはListenerを開く
            // var eventContext = new OnModalEventContext(context, $"Modal Close. Message:{context.Message}");
            // foreach (var l in _listeners)
            // {
            //     l.Open(eventContext);
            // }
            
            rootContainer.gameObject.SetActive(false);
            
            // アプリ再起動
            // TODO: タイトルに戻るにできるか
            var identifier = new BuiltInSceneIdentifier("title");
            await GlobalSceneNavigator.Instance.Change(identifier);
        }
        
        #endregion

    }
}
