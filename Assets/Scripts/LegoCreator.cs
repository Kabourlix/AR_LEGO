using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LegoCreator : MonoBehaviour
{
    private void Start()
    {
        //legoCreator(3, 8, 2, Color.black);
    }

    [SerializeField] GameObject unitBrick;
    [SerializeField] GameObject brickNub;

    public GameObject legoCreator(int height, int length, int width, Color color)
    {
        GameObject lego = new GameObject();

        // The brick body is a scaled cube
        GameObject body = Instantiate(unitBrick, lego.transform);
        body.transform.localScale = new Vector3(length, height, width);
        
        // Add nubs on the top
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject nub = Instantiate(brickNub, lego.transform);
                nub.transform.localPosition = new Vector3(i, height, j);
            }
        }

        // Change the color of all the children of o
        MeshFilter[] mfs = lego.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter mf in mfs)
        {
            Renderer rend = mf.GetComponent<Renderer>();
            rend.material.color = color;
        }

        return lego;
    }
}
