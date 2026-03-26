using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tartarus
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Transform parentTransform;
        public CanvasGroup canvasGroup;
        private InventorySlot inventorySlot;

        public int ID;
        public int type;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            inventorySlot = GetComponentInParent<InventorySlot>();
            ID = inventorySlot.ID;
            type = inventorySlot.type;

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = false;
            parentTransform = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();

        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            transform.SetParent(parentTransform);
        }

    }
}
