using System;
using System.Collections;
using System.Linq;
using Data;
using UnityEngine;

public class WheelGO : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PocketGO[] pocketObjects;
    [SerializeField] private BallGO ballObject;

    private void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * speed);
    }

    public void StartBall(NumberSO result, Action onCompleted)
    {
        var targetPocket = pocketObjects.First(x => x.NumberData == result);

        StartCoroutine(HandleAnimation());

        return;

        IEnumerator HandleAnimation()
        {
            ballObject.StartBall();

            yield return new WaitForSeconds(1f);

            ballObject.StopBallAt(targetPocket, onCompleted);
        }
    }

    private void OnValidate()
    {
        pocketObjects = GetComponentsInChildren<PocketGO>();
    }
}