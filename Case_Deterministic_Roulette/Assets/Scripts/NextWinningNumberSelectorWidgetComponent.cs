using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextWinningNumberSelectorWidgetComponent : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image background;

    public NumberSO Number { get; private set; }

    public static event Action<NextWinningNumberSelectorWidgetComponent> OnClicked;

    public void Set(NumberSO number)
    {
        text.text = number.value.ToString();
        background.color = number.color;

        Number = number;
    }

    public void Select()
    {
        transform.localScale = Vector3.one * 1.2f;
    }

    public void DeSelect()
    {
        transform.localScale = Vector3.one;
    }

    private void OnClick()
    {
        OnClicked?.Invoke(this);
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnValidate()
    {
        button = GetComponent<Button>();
    }
}