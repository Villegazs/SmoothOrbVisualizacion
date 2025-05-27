using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DescriptionNavigation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;

    [Header("Debug")]
    [SerializeField] private int currentIndex = 0;

    private List<Image> navigationButtons = new List<Image>();

    public void InitializeNavigation(int descriptionCount)
    {
        currentIndex = 0;
        // Limpiar botones existentes
        foreach (Transform child in buttonsContainer)
        {
            Destroy(child.gameObject);
        }
        navigationButtons.Clear();

        // Crear nuevos botones
        for (int i = 0; i < descriptionCount; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonsContainer);
            Image buttonImage = newButton.GetComponent<Image>();

            // Configurar el sprite inicial
            buttonImage.sprite = (i == currentIndex) ? activeSprite : inactiveSprite;

            // Configurar el evento de clic
            int index = i; // Capturar el valor para el closure

            navigationButtons.Add(buttonImage);
        }
    }

    public void SetCurrentIndex(int newIndex)
    {
        // Validar el índice
        if (newIndex < 0 || newIndex >= navigationButtons.Count) return;
        Debug.Log("New Index" + newIndex);
        // Actualizar sprites
        navigationButtons[currentIndex].sprite = inactiveSprite;
        currentIndex = newIndex;
        navigationButtons[currentIndex].sprite = activeSprite;
    }

    public void OnNavigationButtonClicked(int index)
    {
        SetCurrentIndex(index);
        // Aquí puedes agregar lógica adicional para cambiar la descripción mostrada
    }
}