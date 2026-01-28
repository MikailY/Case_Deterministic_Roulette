using Events;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsComponent : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button hideButton;
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text totalSpinsText;
    [SerializeField] private TMP_Text totalBetAmountText;
    [SerializeField] private TMP_Text totalWinText;
    [SerializeField] private TMP_Text highestWinText;
    [SerializeField] private TMP_Text overallResultText;

    private void OnStatisticsButtonClicked(Event_OnStatisticsButtonClicked obj)
    {
        EventBus<Event_OnGetBoardSession>.Publish(new Event_OnGetBoardSession(OnGetSession));

        return;

        void OnGetSession(BoardSession session)
        {
            StartCoroutine(balanceText.TextAnimation(0, session.ChipAmount, 1f, "Current balance: {0}"));
            StartCoroutine(totalSpinsText.TextAnimation(0, session.Statistics.TotalSpins, 1f, "Total spins: {0}"));
            StartCoroutine(totalBetAmountText.TextAnimation(0, session.Statistics.TotalBetAmount, 1f,
                "Total bet amount: {0}"));
            StartCoroutine(totalWinText.TextAnimation(0, session.Statistics.TotalWin, 1f, "Total win amount: {0}"));
            StartCoroutine(highestWinText.TextAnimation(0, session.Statistics.HighestWin, 1f,
                "Highest win amount: {0}"));

            if (session.Statistics.OverallResult < 0)
            {
                StartCoroutine(overallResultText.TextAnimation(0, -session.Statistics.OverallResult, 1f,
                    "Overall result: {0}"));
                overallResultText.color = Color.red;
            }
            else
            {
                StartCoroutine(overallResultText.TextAnimation(0, session.Statistics.OverallResult, 1f,
                    "Overall result: {0}"));
                overallResultText.color = Color.green;
            }

            canvasGroup.Show();
        }
    }

    private void HideButtonClicked()
    {
        canvasGroup.Hide();
    }

    private void OnEnable()
    {
        EventBus<Event_OnStatisticsButtonClicked>.Subscribe(OnStatisticsButtonClicked);
        hideButton.onClick.AddListener(HideButtonClicked);
    }

    private void OnDisable()
    {
        EventBus<Event_OnStatisticsButtonClicked>.Unsubscribe(OnStatisticsButtonClicked);
        hideButton.onClick.RemoveListener(HideButtonClicked);
    }

    private void OnValidate()
    {
        hideButton = GetComponentInChildren<Button>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
}