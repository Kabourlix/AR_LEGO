using System;
using UnityEngine;
using Data;
using JetBrains.Annotations;

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

    public event Action<Piece, bool> OnNewBrickToBeDisplayed;

}

public struct Piece
{
    public string Nom;
    public Vector3 Pos;
    public Quaternion Orientation;
    public string CompositeName;

    public Piece(string nom, Vector3 pos, Quaternion orientation, string compositeName)
    {
        this.Nom = nom;
        this.Pos = pos;
        this.Orientation = orientation;
        this.CompositeName = compositeName;
    }
}