using UnityEngine;
using System.Collections.Generic;

public class OrbeCollectionManager : MonoBehaviour
{
    public static OrbeCollectionManager Instance;

    [SerializeField] private List<DetectMouse> orbeObjects = new List<DetectMouse>();
    private Dictionary<OrbeInfo, DetectMouse> orbeDictionary = new Dictionary<OrbeInfo, DetectMouse>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Construir el diccionario
        foreach (var orbe in orbeObjects)
        {
            if (orbe.OrbeInfo != null && !orbeDictionary.ContainsKey(orbe.OrbeInfo))
            {
                orbeDictionary.Add(orbe.OrbeInfo, orbe);
            }
        }
    }

    // Método para registrar orbes dinámicamente
    public void RegisterOrbe(DetectMouse orbe)
    {
        if (!orbeObjects.Contains(orbe))
        {
            orbeObjects.Add(orbe);
            if (orbe.OrbeInfo != null && !orbeDictionary.ContainsKey(orbe.OrbeInfo))
            {
                orbeDictionary.Add(orbe.OrbeInfo, orbe);
            }
        }
    }

    // Método para obtener un orbe por su info
    public DetectMouse GetOrbeByInfo(OrbeInfo info)
    {
        orbeDictionary.TryGetValue(info, out DetectMouse orbe);
        return orbe;
    }

    // Método para obtener todos los orbes
    public List<DetectMouse> GetAllOrbes()
    {
        return new List<DetectMouse>(orbeObjects);
    }
}