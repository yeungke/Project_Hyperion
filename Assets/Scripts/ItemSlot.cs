using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private string _slotType;
    [SerializeField] private bool _occupied = false;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped into slot");

        if (eventData.pointerDrag != null && !_occupied)
        {
            _occupied = true;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            if (_slotType == "eqp")
            {
                eventData.pointerDrag.GetComponent<ItemDragDrop>()?.Equipped(this.gameObject);
            }
        }
        else if (eventData.pointerDrag != null && _occupied)
        {
            eventData.pointerDrag.GetComponent<ItemDragDrop>()?.ReturnToSlot();
        }
    }

    public void SetOccupied(bool value)
    {
        _occupied = value;
    }

    public bool GetOccupied()
    {
        return _occupied;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
