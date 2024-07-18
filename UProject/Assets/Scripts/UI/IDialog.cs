using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STak4.brickout
{
    public interface IDialog
    {
        public void AddListener(IWindow window);
        public void RemoveListener(IWindow window);
        public void OnModalOpen(ModalContext context);
        public void OnModalClose(ModalContext context);
    }
}
