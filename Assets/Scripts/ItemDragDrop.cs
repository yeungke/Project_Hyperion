using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    [SerializeField] private string _name;

    private RectTransform _position;
    private CanvasGroup _group;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _upgradeSlot;

    private void Awake()
    {
        _position = this.GetComponent<RectTransform>();
        _group = this.GetComponent<CanvasGroup>();
        if (_parent != null)
        {
            _parent.GetComponent<ItemSlot>()?.SetOccupied(true);
            _parent.GetComponent<ItemSlot>()?.SetOccupant(this.gameObject);
        }
    }



    public void Unequip()
    {

        Debug.Log("Unequip Called");
        if (_upgradeSlot != null)
        {
            _upgradeSlot.GetComponent<ItemSlot>()?.SetOccupied(false);
        }
        GameManager.ToggleUpgrade(_name, false);
    }

    public void Equipped(GameObject obj)
    {
        if (_upgradeSlot != null)
        {
            _upgradeSlot.GetComponent<ItemSlot>()?.RemoveOccupant();
            _upgradeSlot = obj;
            Debug.Log("upgrade slot assigned");
        }
        else
        {
            _upgradeSlot = obj;
            Debug.Log("upgrade slot assigned");
        }
        if (_name != null)
        {
            GameManager.ToggleUpgrade(_name, true);
        }
    }

    public void CheckEquippedOrParent(GameObject obj)
    {
        Debug.Log("CheckEquippedOrParent Called");
        /*if (_parent != null && _parent != obj)
        {*/
        if (obj.GetComponent<ItemSlot>()?.GetSlotType() == "eqp") 
        {
            /*_parent.GetComponent<ItemSlot>()?.RemoveOccupant();
            _parent = obj;*/

            if (_upgradeSlot != null)
            {
                Debug.Log("dropped into upgrade slot");
                _upgradeSlot.GetComponent<ItemSlot>()?.RemoveOccupant();
                //_upgradeSlot = obj;
                _upgradeSlot = obj;
            }
        }
        if (obj.GetComponent<ItemSlot>()?.GetSlotType() == "inv")
        {
            Debug.Log("dropped into iventory slot");
            if (_upgradeSlot != null)
            {
                Debug.Log("upgradeSlot not null");
                _upgradeSlot.GetComponent<ItemSlot>()?.RemoveOccupant();
                //_upgradeSlot = obj;
                _upgradeSlot = null;

                Unequip();
            }
            if (_parent != null )
            {
                if (_parent != obj)
                {
                    _parent.GetComponent<ItemSlot>()?.RemoveOccupant();
                }
                _parent = obj;
            }
        }

    }

    public void ReturnToSlot()
    {
        _position.anchoredPosition = _parent.GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("DragStart");
        _group.alpha = 0.5f;
        _group.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _position.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag End");
        _group.alpha = 1f;
        _group.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Clicking");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }
}
