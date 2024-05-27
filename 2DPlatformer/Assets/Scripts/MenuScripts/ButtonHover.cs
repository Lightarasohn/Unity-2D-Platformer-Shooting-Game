using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Canvas").transform.GetComponent<AudioSource>();
    }
    private void Update()
    {
        Debug.Log(audioSource);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Fare butonun üzerine geldiðinde
        audioSource.clip = hoverSound;
        audioSource.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Butona týklandýðýnda
        audioSource.clip = clickSound;
        audioSource.Play();
    }
}