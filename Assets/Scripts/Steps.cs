using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    private StepManager step;
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] GameObject startMenu;
    private int maxSteps;

    public void Start()
    {
        step = StepManager.Instance;
        confirmationPanel.SetActive(false);
    }

    public void OnNext()
    {
        step.NextStep();
    }

    public void OnBack()
    {
        
    }

    public void OnSave()
    {
        confirmationPanel.SetActive(true);
    }

    public void OnYes()
    {
        step.SaveConstruction();
        startMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnNo()
    {
        confirmationPanel.SetActive(false);
    }
}
