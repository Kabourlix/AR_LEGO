using System;
using UnityEngine;
using Data;

public class StepManager : MonoBehaviour
{
    private Notice _notice;
    private Part _currentPart;
    private int _currentStepIndex;
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

    #region Make a scriptable object + AWake function
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

    #endregion
    

    public void StartNotice(string mainNoticeName)
    {
        LoadMainNotice(mainNoticeName);
        noticeUI.SetActive(true); // We active the notice UI for this part.s
        //Set the slider status
    }

    private void LoadMainNotice(string noticeName)
    {
        _notice.ExtractMainNotice(noticeName);
        
        _currentStepIndex = 0;
        _currentPart = _notice.GetPart();
        
        NextStep();
    }

    private Piece ConvertStepToPiece(Step s)
    {
        string[] posStr = s.pos.Split(',');
        string[] orStr = s.or.Split(',');
        
        Vector3 pos = new Vector3(int.Parse(posStr[0]), int.Parse(posStr[1]), int.Parse(posStr[2]));
        Vector3 orEuler = new Vector3(int.Parse(orStr[0]), int.Parse(orStr[1]), int.Parse(orStr[2]));
        Quaternion or = Quaternion.Euler(orEuler);
        Piece p = new Piece(s.piece,pos,or,_currentPart.result);
        return p;
    }

    /// <summary>
    /// This function update the part of the construction if needed and provide a new brick to all the subscribed
    /// method to the event OnNewBrickToBeDisplayed.
    /// </summary>
    public void NextStep()
    {
        if (_currentStepIndex >= _currentPart.steps.Count)
        {
            print("We pass to a new part of the notice");
            _currentPart = _notice.GetPart(); //! Pay attention to the moment there is no part anymore ! 
            _currentStepIndex = 0;
        }
        Piece p = ConvertStepToPiece(_currentPart.steps[_currentStepIndex]);
        _currentStepIndex++;
        
        NewBrick(p,_currentPart.main); //We instruct a new brick is to be treated.
    }


    #region Events
    public event Action<Part> OnPartChanged; // Pas sur que ce soit utile

    private void PartChanged(Part part)
    {
        OnPartChanged?.Invoke(part);
    }

    public event Action<Piece, bool> OnNewBrickToBeDisplayed;

    public void NewBrick(Piece p, bool b)
    {
        OnNewBrickToBeDisplayed?.Invoke(p,b);
    }
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