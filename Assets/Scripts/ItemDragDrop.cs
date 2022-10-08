using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    [SerializeField] private string _name;

    private RectTransform _position;
    private CanvasGroup _group;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _upgradeSlot;

    private GraphicRaycaster graphicRaycaster;
    private void Awake()
    {
        _position = this.GetComponent<RectTransform>();
        _group = this.GetComponent<CanvasGroup>();

        graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();

        if (_parent != null)
        {
            _parent.GetComponent<ItemSlot>()?.SetOccupied(true);
            _parent.GetComponent<ItemSlot>()?.SetOccupant(this.gameObject);
        }
    }



    public void Unequip()
    {

        //Debug.Log("Unequip Called");
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
           // Debug.Log("upgrade slot assigned");
        }
        else
        {
            _upgradeSlot = obj;
           // Debug.Log("upgrade slot assigned");
        }
        if (_name != null)
        {
            GameManager.ToggleUpgrade(_name, true);
        }
    }

    public void CheckEquippedOrParent(GameObject obj)
    {
        if (obj.GetComponent<ItemSlot>()?.GetSlotType() == "eqp") 
        {

            if (_upgradeSlot != null)
            {
                _upgradeSlot.GetComponent<ItemSlot>()?.RemoveOccupant();
                _upgradeSlot = obj;
            }
        }
        if (obj.GetComponent<ItemSlot>()?.GetSlotType() == "inv")
        {
            if (_upgradeSlot != null)
            {
                _upgradeSlot.GetComponent<ItemSlot>()?.RemoveOccupant();
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
       // _group.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _position.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag End");
        _group.alpha = 1f;
        //_group.blocksRaycasts = true;
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
        Debug.Log("Item OnDrop called");
        bool placed = false;
        var results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);

        foreach (var hit in results)
        {
            if (hit.gameObject.GetComponent<ItemSlot>() != null)
            {
                if (hit.gameObject.GetComponent<ItemSlot>().isOccupied() == false)
                {
                    CheckEquippedOrParent(hit.gameObject);
                    hit.gameObject.GetComponent<ItemSlot>().SetOccupant(this.gameObject);
                    this.GetComponent<RectTransform>().anchoredPosition = hit.gameObject.GetComponent<RectTransform>().anchoredPosition;
                    placed = true;
                    break;
                }
            }
        }
        if (placed == false)
        {
            if (_upgradeSlot != null)
            {
                if (this.GetComponent<RectTransform>().anchoredPosition != _upgradeSlot.GetComponent<RectTransform>().anchoredPosition)
                {
                    this.GetComponent<RectTransform>().anchoredPosition = _upgradeSlot.GetComponent<RectTransform>().anchoredPosition;
                }
            }
            else if (_parent != null)
            {
                if (this.GetComponent<RectTransform>().anchoredPosition != _parent.GetComponent<RectTransform>().anchoredPosition)
                {
                    this.GetComponent<RectTransform>().anchoredPosition = _parent.GetComponent<RectTransform>().anchoredPosition;
                }
            }
        }
    }
}
