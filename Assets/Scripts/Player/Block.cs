﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    [System.Serializable]
    public class Block
    {
        [SerializeField]
        private GameObject first, second;

        public void Deactivate()
        {
            first.SetActive(false);
            second.SetActive(false);
        }
        public void Activate()
        {
            first.SetActive(true);
            second.SetActive(true);
        }
    }
}