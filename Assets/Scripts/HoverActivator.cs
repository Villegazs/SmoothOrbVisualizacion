using UnityEngine;
using UnityEngine.EventSystems;

public class HoverActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject hoverObject; // Asigna el objeto "Hover" en el Inspector

    private void Start()
    {
        hoverObject = transform.Find("Hover")?.gameObject;
        // Asegurarse de que el objeto está desactivado al inicio
        if (hoverObject != null)
        {
            hoverObject.SetActive(false);
        }
    }

    // Se llama cuando el cursor entra en el objeto
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverObject != null)
        {
            hoverObject.SetActive(true);
        }
    }

    // Se llama cuando el cursor sale del objeto
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverObject != null)
        {
            hoverObject.SetActive(false);
        }
    }
}
