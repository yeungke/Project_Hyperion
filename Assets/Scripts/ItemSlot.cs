using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private string _slotType;
    [SerializeField] private bool _occupied = false;
    private GameObject _occupant;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped into slot");

        if (eventData.pointerDrag != null && (!_occupied || _occupant == eventData.pointerDrag.gameObject))
        {
            _occupied = true;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            _occupant = eventData.pointerDrag.gameObject;
            if (_slotType == "eqp")
            {
                eventData.pointerDrag.GetComponent<ItemDragDrop>()?.Equipped(this.gameObject);
            }
            else if (_slotType == "inv")
            {
                eventData.pointerDrag.GetComponent<ItemDragDrop>()?.CheckEquippedOrParent(this.gameObject);
            }
        }
        else if (eventData.pointerDrag != null && _occupied)
        {
            eventData.pointerDrag.GetComponent<ItemDragDrop>()?.ReturnToSlot();
        }
    }

    public void RemoveOccupant()
    {
        if (_occupant != null)
        {
            _occupant = null;
            /*if (_slotType == "eqp")
            {*/
            _occupied = false;
            //}
        }
    }

    public string GetSlotType()
    {
        return _slotType;
    }

    public void SetOccupant(GameObject obj)
    {
        _occupant = obj;
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
