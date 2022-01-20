using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class LegoController : MonoBehaviour
{
    public event Action OnNoticeFinished;
    public event Action OnActionImpossible;

    [SerializeField]
    private List<GameObject> brickPrefabs;
    private GameObject currentBrick;

    public void PutPiece(Piece piece, bool mainArea)
    {
        /* crée le prefab "parent_brick" si inexistant.
         * ajoute le prefab "brick" à "parent_brick".
         */
    }










    private void CleanCurrentStep()
    {
        Destroy(currentBrick);
    }

    private void StartStep(int x, int y, int z, int orientation, string piece)
    {
        Vector3 world_position = new Vector3(
            x * 5 * 1.6e-3f, // scale to real lego dimensions
            y * 2 * 1.6e-3f,
            z * 5 * 1.6e-3f
        );
        Quaternion world_orientation = Quaternion.AngleAxis(90 * orientation, Vector3.up);
        currentBrick = Instantiate(brickPrefabs[0], world_position, world_orientation);
    }
}
