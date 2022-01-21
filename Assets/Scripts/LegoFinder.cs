using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LegoFinder : MonoBehaviour
{
    [FormerlySerializedAs("currentlego")] [SerializeField] GameObject marker;
    [SerializeField] private Transform[] targets; //0 : small, 1 : middle, 2 : large, 3 : composite
    private bool[] _targetsFound;

    private StepManager _stepManager;

    // Start is called before the first frame update
    void Start()
    {
        _stepManager = StepManager.Instance;
        _stepManager.OnNewBrickToBeDisplayed += IndicateZone;
        _targetsFound = new[] {false, false, false, false};
        marker.SetActive(false);
    }

    private bool once = true;
    private void Update()
    {
        if (once)
        {
            Piece p = new Piece("bebou", Vector3.one, Quaternion.identity, "bebou2", "middle");
            IndicateZone(p,true);
            once = false;
        }
        
    }

    private void IndicateZone(Piece p, bool b)
    {
        switch (p.Size)
        {
            case "small":
                PlaceMarker(0);
                break;
            case "middle":
                PlaceMarker(1);
                break;
            case "large":
                PlaceMarker(2);
                break;
            case "composite":
                PlaceMarker(3);
                break;
            default:
                print("The size isn't recognized !");
                break;
        }
    }

    private void PlaceMarker(int i)
    {
        marker.SetActive(true);
        Vector3 targetPos = targets[i].transform.position;
        marker.transform.position = new Vector3(targetPos.x, targetPos.y-0.01f, targetPos.z+0.02f);
    }

    public void TargetFound(int index)
    {
        _targetsFound[index] = true;
    }

    public void TargetLost(int index)
    {
        _targetsFound[index] = false;
    }
    
}
