
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private float _startingPos; // baslangic pozisyonu
    private float _lengthOfSprites; //sprite'larin uzunlugu
    public float speedOfParallax; // parallax efektinin hizi
    public Camera MainCamera; // ana kamera

    private void Start()
    {
        _startingPos = transform.position.x; // sprite'in baslangic x pozisyonu aliniyor
        _lengthOfSprites = GetComponent<SpriteRenderer>().bounds.size.x;    // sprite'larin uzunlugu aliniyor
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<Camera>();
    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && !GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>().isPlayerDead() && Time.timeScale > 0)
        {
        Vector3 Position = MainCamera.transform.position;
        float Temp = Position.x * (1 - speedOfParallax);
        float Distance = Position.x * speedOfParallax;

        Vector3 NewPosition = new Vector3(_startingPos + Distance, transform.position.y, transform.position.z);

        transform.position = NewPosition;

            // arka plan tekarari\\
            if (Temp > _startingPos + (_lengthOfSprites / 2))
            {
                _startingPos += _lengthOfSprites;
            }
            else if (Temp < _startingPos - (_lengthOfSprites / 2))
            {
                _startingPos -= _lengthOfSprites;
            }
        }
    }

}
