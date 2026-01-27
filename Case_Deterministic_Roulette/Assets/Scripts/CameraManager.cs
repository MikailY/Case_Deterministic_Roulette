using System;
using System.Collections;
using Events;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform wheelCameraTransform;
    [SerializeField] private Camera mainCamera;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Start()
    {
        _startPosition = mainCamera.transform.position;
        _startRotation = mainCamera.transform.rotation;
    }

    private void OnSpinStarted(Event_OnSpinStarted obj)
    {
        StopAllCoroutines();

        StartCoroutine(AnimateCameraTo(wheelCameraTransform.position, wheelCameraTransform.rotation));
    }

    private void OnReset(Event_OnReset obj)
    {
        StopAllCoroutines();

        StartCoroutine(AnimateCameraTo(_startPosition, _startRotation));
    }

    private IEnumerator AnimateCameraTo(Vector3 position, Quaternion rotation)
    {
        const float duration = 1f;

        var time = 0f;
        var startPosition = mainCamera.transform.position;
        var startRotation = mainCamera.transform.rotation;

        while (time < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, position, time / duration);
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, rotation, time / duration);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        mainCamera.transform.position = position;
        mainCamera.transform.rotation = rotation;
    }

    private void OnEnable()
    {
        EventBus<Event_OnSpinStarted>.Subscribe(OnSpinStarted);
        EventBus<Event_OnReset>.Subscribe(OnReset);
    }

    private void OnDisable()
    {
        EventBus<Event_OnSpinStarted>.Subscribe(OnSpinStarted);
        EventBus<Event_OnReset>.Subscribe(OnReset);
    }
}