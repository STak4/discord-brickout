using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STak4.brickout
{
    public class UgsProfile
    {
        private string _profile;
        public UgsProfile(string profile)
        {
            _profile = profile;
        }

        public string GetProfileName()
        {
            return _profile;
        }
    }
}
