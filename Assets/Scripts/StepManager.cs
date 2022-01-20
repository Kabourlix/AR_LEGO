using System;
using UnityEngine;
using Data;
using JetBrains.Annotations;

public class StepManager : MonoBehaviour
{
    private Notice _notice;
    private Part _currentPart;
    [SerializeField] private GameObject noticeUI;

    public Part CurrentPart
    {
        get => _currentPart;
        set
        {
            _currentPart = value;
            PartChanged(value);
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
        noticeUI.SetActive(true); // We active the notice UI for this part.s
    }

    private void LoadMainNotice(string noticeName)
    {
        _notice.ExtractMainNotice(noticeName);
    }


    #region Events
    public event Action<Part> OnPartChanged;

    private void PartChanged(Part part)
    {
        OnPartChanged?.Invoke(part);
    }

    public event Action<Piece, bool> OnNewBrickToBeDisplayed;
    #endregion
    

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