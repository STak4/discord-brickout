using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace STak4.brickout.UI
{
    public class LobbyView : MonoBehaviour
    {
        public Button NextButton;

        public Button CreateButton;
        public Button RefreshButton;
        public TMP_Text MessageText;
        public UgsSessionView SessionViewPrefab;
        public Transform SessionViewParent;
    }
}
