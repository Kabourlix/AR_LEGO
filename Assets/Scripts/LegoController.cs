using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class LegoController : MonoBehaviour
{
    /* Units of measurement in this class:
     *    _____
     *  __|___|__ 
     *  |       | ^
     *  |       | | height of a brick = 3
     *  |       | | height of a plate = 1
     *  |_______| v
     *  <------->
     *  width of a brick = 1
     */

    private Dictionary<string, GameObject> existingBricks; // index of all previous bricks
    private List<GameObject> steps; // index of all steps scenes
    private LegoCreator legoCreator;

    [SerializeField] private GameObject legoCreatorObject;
    [SerializeField] private GameObject mainAreaRoot;
    [SerializeField] private GameObject secondAreaRoot;
    [SerializeField] private GameObject missingBrick;

    public void Start()
    {
        existingBricks = new Dictionary<string, GameObject>();
        steps = new List<GameObject>();
        legoCreator = legoCreatorObject.GetComponent<LegoCreator>();
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
        foreach (GameObject x in steps)
        {
            Destroy(x);
        }
        existingBricks.Clear();
        steps.Clear();
    }

    /// <summary>
    /// Remove the currently shown bricks
    /// </summary>
    public void HideSteps()
    {
        foreach (GameObject x in steps)
        {
            x.SetActive(false);
        }
    }

    /// <summary>
    /// Show a brick that has been added previously
    /// </summary>
    public void ShowStep(int step)
    {
        steps[step].SetActiveRecursively(true);
    }

    /// <summary>
    /// Add a brick to an assembly and show it
    /// </summary>
    public void PutBrick(Piece piece, bool mainArea)
    {
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
        newBrick.transform.localPosition = piece.Pos;
        newBrick.transform.localRotation = piece.Orientation;

        GameObject stagingArea = mainArea ? mainAreaRoot : secondAreaRoot;
        steps.Add(Instantiate(newBrick, stagingArea.transform));

        // Show the source on the staging area
        HideSteps();
        ShowStep(steps.Count - 1);
    }

    private GameObject GenerateBrick(string name)
    {
        // Defaults
        int length = 1;
        int width = 1;
        int height = 1;
        Color color = Color.magenta;

        String[] split = name.Split(':'); // brick|plate : length : width : color
        switch (split[0])
        {
            case "plate":   height = 1; break;
            case "brick":   height = 3; break;
        }
        int.TryParse(split[1], out length);
        int.TryParse(split[2], out width);
        ColorUtility.TryParseHtmlString(split[3], out color);

        GameObject generated = legoCreator.legoCreator(height, length, width, color);
        return generated;
    }

    // ------------------------ TEST ------------------------

    private int testStep = 0;
    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            testStep += 1;
            switch (testStep)
            {
                case 1: PutBrick(new Piece("plate:12:12:gray", new Vector3(-6, 0, -6), Quaternion.identity, "main", ""), true); return;
                case 2: PutBrick(new Piece("brick:4:2:red", new Vector3(0, 0, 0), Quaternion.identity, "comp1", ""), false); return;
                case 3: PutBrick(new Piece("brick:4:2:green", new Vector3(0, 3, 0), Quaternion.identity, "comp1", ""), false); return;
                case 4: PutBrick(new Piece("brick:4:2:red", new Vector3(0, 6, 0), Quaternion.identity, "comp1", ""), false); return;
                case 5: PutBrick(new Piece("comp1", new Vector3(-6, 1, -6), Quaternion.identity, "main", ""), true); return;
                case 6: PutBrick(new Piece("comp1", new Vector3(-6, 1, 6), Quaternion.Euler(0, 90, 0), "main", ""), true); return;
                case 7: PutBrick(new Piece("comp1", new Vector3(6, 1, 6), Quaternion.Euler(0, 180, 0), "main", ""), true); return;
                case 8: PutBrick(new Piece("comp1", new Vector3(6, 1, -6), Quaternion.Euler(0, 270, 0), "main", ""), true); return;
                case 9: Clear(); testStep = 0; return;
            }
        }
    }
}
