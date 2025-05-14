using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewOrbeInfo", menuName = "Orbe/Orbe Info")]
public class OrbeInfo : ScriptableObject
{
    public string nombre;
    [TextArea(1,3)]
    public List <string> descripcionList = new List<string>();

}