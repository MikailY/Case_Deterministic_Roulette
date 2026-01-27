using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviousResultsComponentWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText;
    [SerializeField] private Image backgroundImage;

    public void Set()
    {
        gameObject.SetActive(false);
    }

    public void Set(NumberSO result)
    {
        gameObject.SetActive(true);

        numberText.text = result.value.ToString();
        backgroundImage.color = result.color;
    }
}