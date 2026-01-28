using System.Collections;
using Events;
using Helpers;
using TMPro;
using UnityEngine;

public class InfoMessageComponent : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private RectTransform rectTransform;

    private void OnShowInfoMessage(Event_ShowInfoMessage obj)
    {
        StopAllCoroutines();

        StartCoroutine(HandleMessage());

        return;

        IEnumerator HandleMessage()
        {
            messageText.text = obj.Message;
            canvasGroup.Show();
            rectTransform.anchoredPosition = Vector2.zero;

            const float duration = 1f;
            var time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                rectTransform.anchoredPosition =
                    Vector3.Lerp(Vector2.zero,
                        Vector2.up * 100,
                        Mathf.SmoothStep(0, 1, time / duration));
                yield return new WaitForEndOfFrame();
            }

            canvasGroup.Hide();
        }
    }

    private void OnEnable()
    {
        EventBus<Event_ShowInfoMessage>.Subscribe(OnShowInfoMessage);
    }

    private void OnDisable()
    {
        EventBus<Event_ShowInfoMessage>.Unsubscribe(OnShowInfoMessage);
    }

    private void OnValidate()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        messageText = GetComponentInChildren<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
    }
}