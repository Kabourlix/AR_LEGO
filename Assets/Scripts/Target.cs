using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject scanner;
    [SerializeField] private GameObject stepProcess;

    public void OnTargetFound()
    {
        print("QR found");
        stepProcess.SetActive(true);
        scanner.SetActive(false);
    }
}
