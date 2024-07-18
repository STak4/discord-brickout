using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STak4.brickout
{
    public class WindowEventContext : IWindowContext
    {
        public string Message { get; }

        public WindowEventContext(string message)
        {
            Message = message;
        }

        public string GetMessage()
        {
            return Message;
        }
    }
}
