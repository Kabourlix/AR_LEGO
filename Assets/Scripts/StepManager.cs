using System;
using UnityEngine;
using Data;

public class StepManager : MonoBehaviour
{
    private Notice _notice;
    private Step[] _currentSubSteps;

    public Step[] CurrentSubSteps
    {
        get => _currentSubSteps;
        set
        {
            _currentSubSteps = value;
            StepsChanged(value);
        }
    }
    
    
    public static StepManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        
        _notice = Notice.Instance;
    }

    public void StartNotice(string mainNoticeName)
    {
        LoadMainNotice(mainNoticeName);
    }

    private void LoadMainNotice(string noticeName)
    {
        _notice.ExtractMainNotice(noticeName);
    }

    public event Action<Step[]> OnStepsChanged;

    private void StepsChanged(Step[] steps)
    {
        OnStepsChanged?.Invoke(steps);
    }

}