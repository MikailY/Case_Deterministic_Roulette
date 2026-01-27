using System;
using Data;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChipSelectorComponentWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;
    [SerializeField] private ChipSO data;

    public int Value { get; private set; }
    public ChipSO Data => data;

    public static event Action<ChipSelectorComponentWidget> OnClicked;

    public void Set(int value)
    {
        text.text = StringHelper.FormatChip(value);
        
        button.image.color = data.color;

        Value = value;
    }

    public void Select()
    {
        transform.localScale = Vector3.one * 1.15f;
    }

    public void DeSelect()
    {
        transform.localScale = Vector3.one * 1f;
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnValidate()
    {
        button = GetComponentInChildren<Button>();
    }

    private void OnButtonClicked()
    {
        OnClicked?.Invoke(this);
    }
}