using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject scanner;
    [SerializeField] private GameObject stepProcess;
    [SerializeField] private GameObject scannerCanvas;
    [SerializeField] private GameObject confirmationCanvas;

    public void OnTargetFound()
    {
        print("QR found");
        confirmationCanvas.SetActive(true);
        scannerCanvas.SetActive(false);
    }

    public void OnYes()
    {
        stepProcess.SetActive(true);
        scanner.SetActive(false);
    }

    public void OnNo()
    {
        confirmationCanvas.SetActive(false);
        scannerCanvas.SetActive(true);
    }
}
