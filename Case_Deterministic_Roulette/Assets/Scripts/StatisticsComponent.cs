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
        EventBus<Event_OnGetBoardSession>.Publish(new Event_OnGetBoardSession(session =>
        {
            balanceText.text = $"Current balance : {StringHelper.FormatChip(session.ChipAmount)}";
            totalSpinsText.text = $"Total spins: {session.Statistics.TotalSpins}";
            totalBetAmountText.text =
                $"Total bet amount : {StringHelper.FormatChip(session.Statistics.TotalBetAmount)}";
            totalWinText.text = $"Total bet amount : {StringHelper.FormatChip(session.Statistics.TotalWin)}";
            highestWinText.text = $"Total bet amount : {StringHelper.FormatChip(session.Statistics.HighestWin)}";

            if (session.Statistics.OverallResult < 0)
            {
                overallResultText.text =
                    $"Overall result : {StringHelper.FormatChip(-session.Statistics.OverallResult)}";
                overallResultText.color = Color.red;
            }
            else
            {
                overallResultText.text =
                    $"Overall result : {StringHelper.FormatChip(session.Statistics.OverallResult)}";
                overallResultText.color = Color.green;
            }

            canvasGroup.Show();
        }));
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