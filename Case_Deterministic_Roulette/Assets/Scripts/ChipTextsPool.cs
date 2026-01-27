using Events;
using UnityEngine;
using UnityEngine.Pool;

public class ChipTextsPool : MonoBehaviour
{
    [SerializeField] private ChipText prefab;

    private IObjectPool<ChipText> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<ChipText>(
            createFunc: () => Instantiate(prefab, transform),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            maxSize: 50
        );
    }

    private void OnGetChipText(Event_OnGetChipText obj)
    {
        _pool.Get(out var go);

        obj.GetChipTextAction?.Invoke(go);
    }

    private void OnReturnChipText(Event_OnReturnChipText obj)
    {
        _pool.Release(obj.ObjectToReturn);
    }


    private void OnEnable()
    {
        EventBus<Event_OnGetChipText>.Subscribe(OnGetChipText);
        EventBus<Event_OnReturnChipText>.Subscribe(OnReturnChipText);
    }

    private void OnDisable()
    {
        EventBus<Event_OnGetChipText>.Unsubscribe(OnGetChipText);
        EventBus<Event_OnReturnChipText>.Unsubscribe(OnReturnChipText);
    }
}