using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShipController : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private float scaleDuration = 1f;
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private Vector3 targetScale;

    void Start()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
        targetScale = endPoint.localScale;
    }

    public void MoveShip()
    {
        StartCoroutine(MoveShipCoroutine());
    }

    private IEnumerator MoveShipCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / moveDuration); // Smoothly interpolate between 0 and 1

            // Calculate the interpolated position
            Vector3 targetPosition = Vector3.Lerp(initialPosition, endPoint.position, t);

            // Calculate the interpolated scale
            float scaleT = Mathf.SmoothStep(0f, 1f, elapsedTime / scaleDuration); // Smoothly interpolate between 0 and 1
            Vector3 interpolatedScale = Vector3.Lerp(initialScale, targetScale, scaleT);

            // Update the ship's position and scale
            transform.position = targetPosition;
            transform.localScale = interpolatedScale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the ship reaches the exact endpoint position and scale
        transform.position = endPoint.position;
        transform.localScale = targetScale;
    }
}
