using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STak4.brickout
{
    public class OnModalEventContext : IWindowContext
    {
        public ModalContext ModalContext { get; }

        public string Message { get; } = "";
        public OnModalEventContext(ModalContext context, string message)
        {
            ModalContext = context;
            Message = $"[{context.Type}]{message}";
        }
        public string GetMessage()
        {
            return Message;
        }
    }
}
