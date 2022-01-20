using System;
using UnityEngine;
using Data;

public class StepManager : MonoBehaviour
{
    private Notice _currentNotice;

    public Notice CurrentNotice
    {
        get => _currentNotice;
        set => _currentNotice = value; //TODO : À modifier plus tard.
    }
    
    public static StepManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    
}