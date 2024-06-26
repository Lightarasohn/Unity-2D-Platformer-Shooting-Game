using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    private TextMeshProUGUI buttonText;
    private Color originalColor;
    [SerializeField] private Color hoverColor = Color.cyan;

    private void Start()
    {
        buttonText = transform.GetComponentInChildren<TextMeshProUGUI>();
        audioSource = GameObject.FindGameObjectWithTag("Canvas").transform.GetComponent<AudioSource>();
        originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Fare butonun üzerine geldiğinde
        buttonText.color = hoverColor;
        audioSource.clip = hoverSound;
        audioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Fare butonun üzerinden çıktığında
        buttonText.color = originalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Butona tıklandığında
        audioSource.clip = clickSound;
        audioSource.Play();
    }
}
