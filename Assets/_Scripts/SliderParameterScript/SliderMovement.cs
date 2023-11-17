using UnityEngine;
using UnityEngine.UI;

public class SliderMovement : MonoBehaviour
{
    public Slider slider;
    public Image greenPoint;
    public RectTransform greenRect;
    public float stopThreshold; // Threshold for stopping at the green point
    public float speed = 1.0f;
    private float timePassed = 0.0f;
    private bool isMovingForward = true;
    public float ds;

    private void Update()
    {
        // Update timePassed based on the movement speed
        timePassed += Time.deltaTime * speed;

        // Calculate the new slider value using Mathf.PingPong
        //float sliderValue = Mathf.PingPong(timePassed, 1.0f);

        // Set the slider value
        //slider.value = sliderValue;

        // Calculate the stop threshold based on the length of the green area
        float greenAreaLength = greenPoint.rectTransform.anchorMax.x - greenPoint.rectTransform.anchorMin.x;
        stopThreshold = greenAreaLength / 2;

        // Check if the slider is within the green area
        float distance = Mathf.Abs(slider.normalizedValue - greenPoint.rectTransform.anchorMin.x);
        ds = slider.value;

        if (Input.GetKey(KeyCode.Space))
        {
            HandleWin();
        }
    }

    private void HandleWin()
    {
        // Periksa jika slider berada di atas warna hijau
        float distance = Mathf.Abs(slider.normalizedValue - greenPoint.rectTransform.anchorMin.x);
        //Debug.Log(greenPoint.rectTransform.anchorMax.x - greenPoint.rectTransform.anchorMin.x);
        if (ds <= greenRect.anchorMax.x && ds >= greenRect.anchorMin.x)
        {
            // Handle kemenangan di sini jika slider berada di atas warna hijau
            Debug.Log("You win!");
        }
        else
        {
            // Handle kekalahan di sini jika slider tidak berada di atas warna hijau
            Debug.Log("You lose!");
        }
        //if (distance <= greenPoint.rectTransform.anchorMax.x - greenPoint.rectTransform.anchorMin.x)
        //{
        //    // Handle kemenangan di sini jika slider berada di atas warna hijau
        //    Debug.Log("You win!");
        //}
        //else
        //{
        //    // Handle kekalahan di sini jika slider tidak berada di atas warna hijau
        //    Debug.Log("You lose!");
        //}
    }
}
