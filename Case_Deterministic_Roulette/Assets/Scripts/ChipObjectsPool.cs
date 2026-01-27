using System.Collections.Generic;
using Data;
using Events;
using UnityEngine;
using UnityEngine.Pool;

public class ChipObjectsPool : MonoBehaviour
{
    private readonly Dictionary<ChipSO, IObjectPool<ChipObject>> _pools = new();

    private void CreatePoolFor(ChipSO chip)
    {
        _pools[chip] = new ObjectPool<ChipObject>(
            createFunc: () => Instantiate(chip.prefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) =>
            {
                obj.transform.SetParent(transform);

                obj.gameObject.SetActive(false);
            },
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            maxSize: 50
        );
    }

    private void OnGetChipObject(Event_OnGetChipObject obj)
    {
        if (!_pools.ContainsKey(obj.Chip))
        {
            CreatePoolFor(obj.Chip);
        }

        _pools[obj.Chip].Get(out var go);

        obj.OnGetObject?.Invoke(go);
    }

    private void OnReturnChipObject(Event_OnReturnChipObject obj)
    {
        _pools[obj.ReturnedObject.Chip].Release(obj.ReturnedObject);
    }

    private void OnEnable()
    {
        EventBus<Event_OnGetChipObject>.Subscribe(OnGetChipObject);
        EventBus<Event_OnReturnChipObject>.Subscribe(OnReturnChipObject);
    }

    private void OnDisable()
    {
        EventBus<Event_OnGetChipObject>.Unsubscribe(OnGetChipObject);
        EventBus<Event_OnReturnChipObject>.Unsubscribe(OnReturnChipObject);
    }
}