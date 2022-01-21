using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    private StepManager step;

    public void Start()
    {
        step = StepManager.Instance;
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
        step.SaveConstruction();
    }


}
