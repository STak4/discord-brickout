using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STak4.brickout
{
    public interface IWindow
    {
        public void Open(IWindowContext context);
        public void Close(IWindowContext context);
    }
}
