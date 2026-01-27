using System.Linq;
using Data;
using Events;
using UnityEngine;

public class ChipHandler : MonoBehaviour
{
    private ChipText _chipText;

    public void Insert(ChipSO chip, int amount)
    {
        if (_chipText == null)
        {
            EventBus<Event_OnGetChipText>.Publish(new Event_OnGetChipText((chipText) => { _chipText = chipText; }));
        }

        EventBus<Event_OnGetChipObject>.Publish(new Event_OnGetChipObject(chip, (chipObject) =>
        {
            chipObject.Chip = chip;
            chipObject.transform.position = transform.position + (Vector3.up * transform.childCount * 0.015f);

            chipObject.transform.SetParent(transform);

            _chipText.SetText(amount);
            _chipText.transform.position = chipObject.TextTransform.position;
        }));
    }

    public void Clear()
    {
        if (_chipText == null) return;

        foreach (var chipObject in GetComponentsInChildren<ChipObject>())
            EventBus<Event_OnReturnChipObject>.Publish(new Event_OnReturnChipObject(chipObject));

        EventBus<Event_OnReturnChipText>.Publish(new Event_OnReturnChipText(_chipText));

        _chipText = null;
    }

    public void Remove(int amount)
    {
        if (_chipText == null) return;

        EventBus<Event_OnReturnChipObject>.Publish(
            new Event_OnReturnChipObject(transform.GetChild(transform.childCount - 1).GetComponent<ChipObject>()));

        if (amount > 0)
        {
            _chipText.SetText(amount);
            _chipText.transform.position = transform.GetChild(transform.childCount - 1)
                .GetComponent<ChipObject>().TextTransform.position;
            return;
        }

        EventBus<Event_OnReturnChipText>.Publish(new Event_OnReturnChipText(_chipText));

        _chipText = null;
    }
}