using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Data
{
    public class LegoPrefabManager : MonoBehaviour
    {
        #region Scriptable object
        public static LegoPrefabManager Instance;

        private void Awake()
        {
            if(Instance != null && Instance != this)
                Destroy(gameObject);

            Instance = this;
        }
        #endregion
        
    }
}