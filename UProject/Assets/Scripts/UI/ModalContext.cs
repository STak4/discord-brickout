using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STak4.brickout
{
    public class ModalContext
    {
        public enum ModalType
        {
            Normal,
            Error
        }

        public ModalType Type { get; }

        public int Priority { get; } = 999;

        public string Message { get; } = "";

        public ModalContext(ModalType type, int priority, string message)
        {
            Type = type;
            Priority = priority;
            Message = message;
        }
    }
}
