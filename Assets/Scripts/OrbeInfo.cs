using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOrbeInfo", menuName = "Orbe/Orbe Info")]
public class OrbeInfo : ScriptableObject
{
    public string nombre;
    [TextArea(3, 10)]
    public string descripcion;

}