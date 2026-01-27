using Helpers;
using TMPro;
using UnityEngine;

public class ChipText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void SetText(int amount)
    {
        text.text = StringHelper.FormatChip(amount);
    }
}