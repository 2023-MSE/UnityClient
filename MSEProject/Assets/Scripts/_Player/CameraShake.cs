using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;   // 흔들림 지속 시간
    public float shakeMagnitude = 0.1f;  // 흔들림 크기



    
    private Vector3 originalPosition;    // 원래 카메라 위치
    private float currentShakeDuration;  // 현재 흔들림 지속 시간

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (currentShakeDuration > 0)
        {
            // 카메라를 흔들리는 방향으로 이동
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = originalPosition + shakeOffset;

            // 흔들림 지속 시간 감소
            currentShakeDuration -= Time.deltaTime;
        }
        else
        {
            // 흔들림이 끝났을 때 카메라를 원래 위치로 되돌림
            transform.localPosition = originalPosition;
        }
    }

    public void ShakeCamera()
    {
        currentShakeDuration = shakeDuration;
    }
}