using System;
using System.Collections;
using UnityEngine;

public class BallGO : MonoBehaviour
{
    [SerializeField] private Transform ballVisualObject;
    [SerializeField] private Transform pocketDirectionObject;
    [SerializeField] private Transform ballStartObject;
    [SerializeField] private float initialBallSpeed = -600f;

    private bool _hasStarted;
    private float _currentBallSpeed;

    private void Update()
    {
        if (!_hasStarted) return;

        transform.Rotate(Vector3.up, _currentBallSpeed * Time.deltaTime);
    }

    public void StartBall()
    {
        ballVisualObject.SetParent(ballStartObject.transform);
        ballVisualObject.localPosition = Vector3.zero;

        _currentBallSpeed = initialBallSpeed;

        _hasStarted = true;
    }

    private PocketGO _debugTargetTODOREMOVE;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(ballVisualObject.position, 0.03f);
        Gizmos.DrawLine(ballVisualObject.position, ballVisualObject.forward * 0.2f);

        if (_debugTargetTODOREMOVE == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(_debugTargetTODOREMOVE.CurvePoint.position, 0.03f);
        Gizmos.DrawLine(_debugTargetTODOREMOVE.CurvePoint.position, _debugTargetTODOREMOVE.CurvePoint.forward * 0.2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_debugTargetTODOREMOVE.EntryPoint.position, 0.03f);
        Gizmos.DrawLine(_debugTargetTODOREMOVE.EntryPoint.position, _debugTargetTODOREMOVE.EntryPoint.forward * 0.2f);
    }

    public void StopBallAt(PocketGO targetPocket, Action onCompleted)
    {
        _debugTargetTODOREMOVE = targetPocket;

        StartCoroutine(HandleStopping());

        return;

        IEnumerator HandleStopping()
        {
            yield return StartCoroutine(SlowDownAndPullIn());
            yield return StartCoroutine(ToEntryPoint());
            yield return StartCoroutine(ToTargetPoint());
            onCompleted?.Invoke();
        }

        IEnumerator SlowDownAndPullIn()
        {
            const int targetAmountToSpin = 3;
            var currentPassCounter = 0;
            var lastRawAngle = 0f;
            var startBallSpeed = _currentBallSpeed;
            var targetBallSpeed = _currentBallSpeed / 5f;

            while (currentPassCounter < targetAmountToSpin)
            {
                var angleRaw =
                    Vector3.SignedAngle(ballVisualObject.forward, targetPocket.CurvePoint.forward, Vector3.up);
                var angle = angleRaw;

                if (angle < 0)
                    angle += 360f;

                if (angleRaw > 0 && lastRawAngle < 0)
                    currentPassCounter++;

                _currentBallSpeed = Mathf.Lerp(startBallSpeed, targetBallSpeed,
                    (angle + (currentPassCounter * 360f)) / (360f * targetAmountToSpin));

                if (targetAmountToSpin - 1 == currentPassCounter)
                {
                    ballVisualObject.position =
                        Vector3.Lerp(ballStartObject.position, pocketDirectionObject.position, angle / 360f);
                }

                lastRawAngle = angleRaw;

                yield return new WaitForEndOfFrame();
            }

            ballVisualObject.position = pocketDirectionObject.position;

            _currentBallSpeed = targetBallSpeed;
        }

        IEnumerator ToEntryPoint()
        {
            const float duration = 0.5f;

            var time = 0f;
            var startOffset = targetPocket.CurvePoint.position - ballVisualObject.position;
            var startSpeed = _currentBallSpeed;

            while (time < duration)
            {
                time += Time.deltaTime;

                var desiredBallPosition =
                    Vector3.Lerp(
                        targetPocket.CurvePoint.position - startOffset,
                        targetPocket.EntryPoint.position,
                        time / duration);

                _currentBallSpeed = Mathf.Lerp(startSpeed, 0f, time / duration);

                ballVisualObject.position = Vector3.Lerp(
                    pocketDirectionObject.position,
                    desiredBallPosition,
                    time / duration);

                yield return new WaitForEndOfFrame();
            }

            ballVisualObject.position = targetPocket.EntryPoint.position;
            _currentBallSpeed = 0;
        }

        IEnumerator ToTargetPoint()
        {
            ballVisualObject.SetParent(targetPocket.Target.transform);

            const float duration = 0.25f;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;

                ballVisualObject.position =
                    Vector3.Lerp(
                        targetPocket.EntryPoint.position,
                        targetPocket.Target.position,
                        time / duration);

                yield return new WaitForEndOfFrame();
            }

            _hasStarted = false;
        }
    }
}