using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition = new Vector3(0f,0.8f,0f);
    private float shakeTimer = 0f;
    private float shakeDuration;
    private float shakeMagnitude;

    public void ShakeCamera(float duration, float magnitude)
    {
        //originalPosition = transform.localPosition;
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeTimer = shakeDuration;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            // Generate a random shake offset for the camera
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;

            // Apply the shake offset to the camera's position
            transform.localPosition = originalPosition + shakeOffset;

            // Reduce the shake timer
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset the camera position after the shake duration is over
            transform.localPosition = originalPosition;
        }
    }
}
