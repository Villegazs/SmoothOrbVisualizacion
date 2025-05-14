using TMPro;
using UnityEngine;
using System;

public class OrbeDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descText;
    private DetectMouse actualOrbPart;
    OrbeInfo currentOrbeInfo;
    private int currentDescIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void ShowOrbeInfo(OrbeInfo info, DetectMouse actualPart)
    {
        currentOrbeInfo = info;
        currentDescIndex = 0;
        titleText.text = currentOrbeInfo.nombre;
        descText.text = currentOrbeInfo.descripcionList[currentDescIndex];
        actualOrbPart = actualPart;
    }

    public void HideOrbeInfo()
    {
        titleText.text = string.Empty;
        descText.text = string.Empty;
        actualOrbPart.EndExamination();
    }

    public void NextDescription()
    {
        if (currentOrbeInfo == null) return;

        currentDescIndex++;
        if (currentDescIndex >= currentOrbeInfo.descripcionList.Count)
        {
            currentDescIndex = 0; // Vuelve al inicio
        }

        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        if (currentOrbeInfo != null && currentDescIndex >= 0 && currentDescIndex < currentOrbeInfo.descripcionList.Count)
        {
            titleText.text = currentOrbeInfo.nombre;
            descText.text = currentOrbeInfo.descripcionList[currentDescIndex];
        }

        UpdateNavigationButtons();
    }

    private void UpdateNavigationButtons()
    {

    }
}
