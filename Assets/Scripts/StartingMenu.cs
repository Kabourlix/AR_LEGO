using UnityEngine;

public class StartingMenu : MonoBehaviour
{
    [SerializeField] private GameObject arCam;
    [SerializeField] private GameObject scanner;
    [SerializeField] private GameObject constructionTarget;
    
    public void OnContinue()
    {
        print("This functionnality has not been implemented yet.");
    }

    public void OnNew()
    {
        print("We build a new lego.");
        Camera.main.gameObject.SetActive(false);
        arCam.SetActive(true);
        scanner.SetActive(true);
        constructionTarget.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
