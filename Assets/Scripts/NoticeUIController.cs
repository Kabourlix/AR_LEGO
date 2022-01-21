using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeUIController : MonoBehaviour
{
    private StepManager _step;
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject sliderGameObject;
    private Slider _slider;
    private int maxSteps;

    public void Start()
    {
        _step = StepManager.Instance;
        confirmationPanel.SetActive(false);
        _slider.GetComponent<Slider>();
        _step.OnPartChanged += IncrementSlider;
    }

    public void SetSliderMax(int max)
    {
        _slider.maxValue = max;
        _slider.value = 0;
    }

    private void IncrementSlider(Part p)
    {
        _slider.value += 1;
    }

    public void OnNext()
    {
        _step.NextStep();
    }

    public void OnBack()
    {
        //Not implemented yet
    }

    public void OnSave()
    {
        confirmationPanel.SetActive(true);
    }

    public void OnYes()
    {
        _step.SaveConstruction();
        startMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnNo()
    {
        confirmationPanel.SetActive(false);
    }
}
