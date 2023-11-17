using UnityEngine;
using UnityEngine.UI;

public class GreenPointRandomizer : MonoBehaviour
{
    public Slider slider;
    public Image greenPoint;
    public RectTransform greenRect;
    public float greenAreaWidth = 0.2f; // Lebar area hijau dalam satuan nilai normalized slider

    private float minPosition;
    private float maxPosition;
    private float greenAreaMin;
    private float greenAreaMax;

    public SliderMovement sliderMovement;

    private void Start()
    {
        // Calculate the minimum and maximum positions for the green point
        minPosition = slider.fillRect.rect.min.x;
        maxPosition = slider.fillRect.rect.max.x;

        // Calculate the minimum and maximum positions for the green area
        float greenAreaSize = (maxPosition - minPosition) * greenAreaWidth;
        greenAreaMin = minPosition + greenAreaSize / 2;
        greenAreaMax = maxPosition - greenAreaSize / 2;

        // Set the local scale of the greenPoint based on greenAreaWidth
        Vector3 localScale = greenPoint.transform.localScale;
        localScale.x = greenAreaWidth;
        greenPoint.transform.localScale = localScale;

        // Randomize the initial position of the green point within the green area
        RandomizeGreenPointPosition();
    }

    public void RandomizeGreenPointPosition()
    {
        // Generate a random position within the green area
        float randomX = Random.Range(greenAreaMin, greenAreaMax);

        // Set the position of the green point
        Vector3 newPosition = greenPoint.transform.localPosition;
        newPosition.x = randomX;
        greenPoint.transform.localPosition = newPosition;

        // Calculate anchor values based on local scale
        float scaleX = slider.transform.localScale.x;
        float anchorMinX = 0.5f - scaleX / 2;
        float anchorMaxX = 0.5f + scaleX / 2;

        // Set the anchor values of the image
        greenRect.anchorMin = new Vector2(anchorMinX, 0f);
        greenRect.anchorMax = new Vector2(anchorMaxX, 1f);
    }
}
