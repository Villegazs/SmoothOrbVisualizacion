using TMPro;
using UnityEngine;
using System;

public class OrbeDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tittleText;
    [SerializeField] TextMeshProUGUI descText;
    private DetectMouse actualOrbPart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void ShowOrbeInfo(OrbeInfo info, DetectMouse actualPart)
    {
        tittleText.text = info.nombre;
        descText.text = info.descripcion;
        actualOrbPart = actualPart;
    }

    public void HideOrbeInfo()
    {
        tittleText.text = string.Empty;
        descText.text = string.Empty;
        actualOrbPart.EndExamination();
    }
}
