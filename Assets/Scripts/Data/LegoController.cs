using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class LegoController : MonoBehaviour
{
    private Dictionary<string, GameObject> existingBricks;

    [SerializeField] private GameObject mainAreaRoot;
    [SerializeField] private GameObject secondAreaRoot;
    [SerializeField] private GameObject missingBrick;

    public void Start()
    {
        existingBricks = new Dictionary<string, GameObject>();
    }

    public void PutBrick(Piece piece, bool mainArea)
    {
        // Convert lego coordinates into world coordinates
        Vector3 world_position = new Vector3(
            piece.Pos.x * 5 * 1.6e-3f,
            piece.Pos.y * 2 * 1.6e-3f,
            piece.Pos.z * 5 * 1.6e-3f
        );
        Quaternion world_orientation = piece.Orientation;
        
        // Create a new composite if this one is new
        if (!existingBricks.ContainsKey(piece.CompositeName))
        {
            GameObject newComposite = new GameObject(piece.CompositeName);
            newComposite.name = piece.CompositeName;
            //newComposite.SetActive(false);
            existingBricks.Add(piece.CompositeName, newComposite);
        }

        // Create a new piece if this one is new
        if (!existingBricks.ContainsKey(piece.Nom))
        {
            GameObject newBrick = GenerateBrick(piece.Nom);
            newBrick.name = piece.Nom;
            //newBrick.SetActive(false);
            existingBricks.Add(piece.Nom, newBrick);
        }

        // Copy the brick and add it to the composite
        Instantiate(existingBricks[piece.Nom], existingBricks[piece.CompositeName].transform);
    }

    private GameObject GenerateBrick(string name)
    {
        // TODO: générer la pièce
        return Instantiate(missingBrick);
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            PutBrick(new Piece(
                "brique1", new Vector3(0, 0, 0), Quaternion.identity, "comp1"
            ), false);
        }
        if (Input.GetKeyDown("b"))
        {
            PutBrick(new Piece(
                "brique2", new Vector3(1, 0, 0), Quaternion.identity, "comp1"
            ), false);
        }
    }
}
