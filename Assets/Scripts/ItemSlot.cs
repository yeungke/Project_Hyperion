using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private string _slotType;
    [SerializeField] private bool _occupied = false;
    [SerializeField] private GameObject _spriteImg;

    private GameObject _occupant;


    private void Awake()
    {
        //_spriteImg = this.transform.Find("UpgradeSprite").gameObject;
    }

    /// <summary>
    /// Maybe not needed anymore idk
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        
        Debug.Log("ItemSlot onDrop called");

        if (eventData.pointerDrag != null && (!_occupied || _occupant == eventData.pointerDrag.gameObject))
        {
            _occupied = true;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            //_occupant = eventData.pointerDrag.gameObject;
            SetOccupant(eventData.pointerDrag.gameObject);
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
            if (_spriteImg.GetComponent<Image>() != null)
            {
                var colour = _spriteImg.GetComponent<Image>().color;
                colour.a = 0f;
                _spriteImg.GetComponent<Image>().color = colour;
            }
        }
    }

    public string GetSlotType()
    {
        return _slotType;
    }

    public bool isOccupied()
    {
        return _occupant != null;
    }




    public void SetOccupant(GameObject obj)
    {
        _occupant = obj;
        if (_spriteImg.GetComponent<Image>() != null)
        {
            Image? objImg = obj.GetComponent<Image>();
            if (objImg != null)
            {
                var colour = objImg.color;
                colour.a = 0.3f;
                _spriteImg.GetComponent<Image>().sprite = objImg.sprite;
                _spriteImg.GetComponent<Image>().color = colour;
            }
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
