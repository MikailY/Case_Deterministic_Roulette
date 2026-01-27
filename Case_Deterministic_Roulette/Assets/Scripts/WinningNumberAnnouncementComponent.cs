using System.Collections;
using Events;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinningNumberAnnouncementComponent : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float animationSpeed;

    private void OnSpinEnded(Event_OnSpinEnded obj)
    {
        StartCoroutine(ShowWinning());

        return;

        IEnumerator ShowWinning()
        {
            rectTransform.anchoredPosition = Vector2.zero;

            canvasGroup.Show();

            text.text = obj.Result.value.ToString();
            backgroundImage.color = obj.Result.color;

            yield return StartCoroutine(AnimateTo(new Vector2(rectTransform.rect.width, 0f), animationSpeed));

            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(AnimateTo(Vector2.zero, animationSpeed));

            canvasGroup.Hide();
        }

        IEnumerator AnimateTo(Vector2 position, float duration)
        {
            var startPosition = rectTransform.anchoredPosition;
            var time = 0f;

            while (time < duration)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, position, time / duration);
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            rectTransform.anchoredPosition = position;
        }
    }

    private void OnEnable()
    {
        EventBus<Event_OnSpinEnded>.Subscribe(OnSpinEnded);
    }

    private void OnDisable()
    {
        EventBus<Event_OnSpinEnded>.Unsubscribe(OnSpinEnded);
    }

    private void OnValidate()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
}