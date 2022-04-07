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
        }
    }



    public void Unequip()
    {
        if (_upgradeSlot != null)
        {
            _upgradeSlot.GetComponent<ItemSlot>()?.SetOccupied(false);
        }
        GameManager.ToggleUpgrade(_name, false);
    }

    public void Equipped(GameObject obj)
    {
        _upgradeSlot = obj;
        if (_name != null)
        {
            GameManager.ToggleUpgrade(_name, true);
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
