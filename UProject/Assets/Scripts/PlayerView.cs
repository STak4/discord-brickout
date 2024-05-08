using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace STak4.brickout
{
    [RequireComponent(typeof(TMP_Text))]
    public class PlayerView : MonoBehaviour
    {
        public void SetText(string txt)
        {
            var tmp = GetComponent<TMP_Text>();
            tmp.text = txt;
        }
    }
}
