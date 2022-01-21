using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoFinder : MonoBehaviour
{
    [SerializeField] GameObject outline;
    [SerializeField] GameObject currentlego;
    private GameObject lego;
    [SerializeField] Transform currenttargetimage;
    private bool targetfound;

    // Start is called before the first frame update
    void Start()
    {
        targetfound = false;
        outline.SetActive(false);
        lego = Instantiate(currentlego, currenttargetimage);
        lego.SetActive(false);
        outline.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        LegoIndicator();
    }

    public void LegoIndicator()
    {
        if (targetfound)
        {
            // Add the lego
            lego.transform.position = new Vector3(currenttargetimage.transform.position.x, currenttargetimage.transform.position.y + 0.5f, currenttargetimage.transform.position.z);


            //Add the outline
            outline.transform.parent = currenttargetimage;
            outline.transform.position = new Vector3(currenttargetimage.transform.position.x, currenttargetimage.transform.position.y - 0.001f, currenttargetimage.transform.position.z);

        }
    }
    public void Targetfound()
    {
        lego = Instantiate(currentlego, currenttargetimage);
        lego.SetActive(true);

        outline.SetActive(true);
        targetfound = true;
    }

    public void TargetLost()
    {
        outline.SetActive(false);
        lego.SetActive(false);
        targetfound = false;
    }
}
