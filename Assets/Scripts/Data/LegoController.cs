using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class LegoController : MonoBehaviour
{
    //private static Vector3 LEGO_SIZE = new Vector3(5 * 1.6e-3f, 2 * 1.6e-3f, 5 * 1.6e-3f);
    private static Vector3 LEGO_SIZE = Vector3.one;

    private Dictionary<string, GameObject> existingBricks;
    private GameObject shownBrick;

    [SerializeField] private GameObject mainAreaRoot;
    [SerializeField] private GameObject secondAreaRoot;
    [SerializeField] private GameObject missingBrick;

    public void Start()
    {
        existingBricks = new Dictionary<string, GameObject>();
    }

    /// <summary>
    /// Remove the bricks and assemblies stored in memory
    /// </summary>
    public void Clear()
    {
        foreach (KeyValuePair<string, GameObject> x in existingBricks)
        {
            Destroy(x.Value);
        }
        Destroy(shownBrick);
        existingBricks.Clear();
    }

    /// <summary>
    /// Hide the brick on the staging areas
    /// </summary>
    public void HideBrick()
    {
        Destroy(shownBrick);
    }

    /// <summary>
    /// Add a brick to an assembly and show it on the main or secondary area
    /// </summary>
    public void PutBrick(Piece piece, bool mainArea)
    {
        

        // Convert lego coordinates into world coordinates
        Vector3 world_position = Vector3.Scale(piece.Pos, LEGO_SIZE);
        Quaternion world_orientation = piece.Orientation;
        
        // Create a new destination if this one is new
        if (!existingBricks.ContainsKey(piece.CompositeName))
        {
            GameObject newComposite = new GameObject(piece.CompositeName);
            newComposite.name = piece.CompositeName;
            newComposite.SetActive(false);
            existingBricks.Add(piece.CompositeName, newComposite);
        }

        // Create a new source if this one is new
        if (!existingBricks.ContainsKey(piece.Nom))
        {
            GameObject generatedBrick = GenerateBrick(piece.Nom);
            generatedBrick.name = piece.Nom + "_generated";
            generatedBrick.SetActive(false);
            existingBricks.Add(piece.Nom, generatedBrick);
        }

        // Copy the source and add it to the destination
        GameObject newBrick = Instantiate(existingBricks[piece.Nom], existingBricks[piece.CompositeName].transform);
        newBrick.name = piece.Nom;
        newBrick.transform.position = world_position;
        newBrick.transform.rotation = world_orientation;

        // Show the source on the staging area
        Destroy(shownBrick);
        GameObject root = mainArea ? mainAreaRoot : secondAreaRoot;
        shownBrick = Instantiate(newBrick, root.transform);
        shownBrick.name = piece.Nom + "_shown";
        shownBrick.SetActiveRecursively(true);
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
            // ajoute brique1 à comp1
            PutBrick(new Piece("brique1", new Vector3(0, 0, 0), Quaternion.identity, "comp1"), false);
        }
        if (Input.GetKeyDown("b"))
        {
            // ajoute brique2 à comp2
            PutBrick(new Piece("brique2", new Vector3(1, 0, 0), Quaternion.identity, "comp1"), false);
        }
        if (Input.GetKeyDown("c"))
        {
            // ajoute comp1 à main
            PutBrick(new Piece("comp1", new Vector3(0, 1, 0), Quaternion.identity, "main"), true);
        }
        if (Input.GetKeyDown("z"))
        {
            Clear();
        }
    }
}
