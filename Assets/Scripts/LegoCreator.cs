using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LegoCreator : MonoBehaviour
{

    
    private float xz_gap = 0.096f;
    private float y_gap = 0.032f;

    [SerializeField] GameObject model;

   public GameObject legoCreator(int height, int length, int width, Color color)
    {
        GameObject lego = new GameObject();
        for(int i=0; i<height; i++ )
        {
            for (int j=0; j < length; j++)
            {
                for (int k=0; k < width; k++)
                {
                    Vector3 position = new Vector3(j * xz_gap,i * y_gap, k* xz_gap);
                    Quaternion rotation = new Quaternion(0, 0, 0, 0);
                    var o = Instantiate(model,position,rotation,lego.transform);

                    // Change the color of all the children of o
                    MeshFilter[] mfs = o.GetComponentsInChildren<MeshFilter>();
                    foreach (MeshFilter mf in mfs)
                    {
                        Renderer rend = mf.GetComponent<Renderer>();
                        rend.material.color = color; 
                    }
                  
                }
            }
        }


        return lego;
    }
}
