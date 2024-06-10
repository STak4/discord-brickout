using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STak4.brickout
{
    [Serializable]
    public class UniqueId
    {
        public string Id;
        public Guid Guid;

        public UniqueId()
        {
            Guid = Guid.NewGuid();
            Id = $"Player[{Guid.ToString()}]";
        }

        public override string ToString()
        {
            return Id;
        }

        public void Log()
        {
            Debug.Log($"ID:{Id}");
        }
    }
}
