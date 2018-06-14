using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public interface IUseable
    {
        Sprite MyIcon
        {
            get;
        }
        void Use();
    }
}