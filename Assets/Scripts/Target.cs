using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject scanner;
    [SerializeField] private GameObject scannerCanvas;
    [SerializeField] private GameObject confirmationCanvas;
    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject text2;
    private StepManager _stepManager;
    
    private string _buildID;

    private void Awake()
    {
        _stepManager = StepManager.Instance;
    }

    public void OnTargetFound(string id)
    {
        print("QR found");
        _buildID = id; // It's value can be anything apart secondary for the first : give the notice name
        scannerCanvas.SetActive(false);
        confirmationCanvas.SetActive(true);
    }

    private string noticeName;
    public void OnYes()
    {
        if(!_buildID.Equals("secondary")){
            confirmationCanvas.SetActive(false);
            scannerCanvas.SetActive(true);
            text1.SetActive(false);
            text2.SetActive(true);
            noticeName = _buildID;
        }
        else
        {
            confirmationCanvas.SetActive(false);
            _stepManager.StartNotice(noticeName);
        }
    }

    public void OnNo()
    {
        confirmationCanvas.SetActive(false);
        scannerCanvas.SetActive(true);
    }
 
}
