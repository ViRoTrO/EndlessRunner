using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 startPosition = transform.position;

        float elapsed = 0;

        while (elapsed < duration)
        {
            float randomX = Random.Range(-1f, 1f) * magnitude;
            float randomY = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(startPosition.x + randomX, startPosition.y + randomY, startPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = startPosition;
    }

}
