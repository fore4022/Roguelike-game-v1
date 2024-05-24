using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Android.Types;
public class Inventory_UI : UI_Scene
{
    public SwipeMenu_UI swipeMenu;

    private RectTransform content;
    private ScrollRect inventoryScrollView;
    private Transform pos;

    private Vector2 enterPoint;
    private Vector2 direction;

    private int height;

    enum Buttons
    {
        //
    }
    enum Images
    {
        Panel,
        Panel1,
        Panel2,
        ScrollView
    }
    enum ScrollRects
    {
        InventoryScrollView
    }
    private void Start() 
    {
        if (SceneManager.GetActiveScene().name == "Main") { pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform; }
        else { }

        init();

        if(pos != null)
        {
            this.gameObject.transform.SetParent(pos);
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

            rectTransform.sizeDelta = pos.GetComponentInParent<RectTransform>().rect.size;
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;

            swipeMenu = FindObjectOfType<SwipeMenu_UI>().GetComponent<SwipeMenu_UI>();
        }
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<ScrollRect>(typeof(ScrollRects));

        GameObject scrollView = get<Image>((int)Images.ScrollView).gameObject;

        inventoryScrollView = get<ScrollRect>((int)ScrollRects.InventoryScrollView);
        content = inventoryScrollView.content.gameObject.GetComponent<RectTransform>();

        inventoryScrollView.movementType = ScrollRect.MovementType.Clamped;

        UI_EventHandler evtHandle1 = null;
        if (pos != null) { evtHandle1 = FindParent<UI_EventHandler>(pos.gameObject); }

        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (pos != null) { evtHandle1.OnBeginDragHandler.Invoke(data); }
#if UNITY_EDITOR
            enterPoint = Input.mousePosition;
#endif

#if UNITY_ANDROID
            enterPoint = Input.GetTouch(0).position;
#endif
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (pos != null) { evtHandle1.OnDragHandler.Invoke(data); }
#if UNITY_EDITOR
            direction = (Vector2)Input.mousePosition - enterPoint;
#endif

#if UNITY_ANDROID
            direction = Input.GetTouch(0).position - enterPoint;
#endif
            if (Mathf.Abs(direction.y) / Mathf.Abs(direction.x) > 0.5f) { Scroll(); }
        }, Define.UIEvent.Drag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (pos != null) { evtHandle1.OnEndDragHandler.Invoke(data); }
        }, Define.UIEvent.EndDrag);

        UI_EventHandler evtHandle2 = FindParent<UI_EventHandler>(content.gameObject);

        height = 6;//Managers.Game.c / 4 + (Managers.Game.c % 4 > 0 ? 1 : 0);

        for(int h = 0; h < Mathf.Max(height, 5); h++)//5
        {
            for(int w = 0; w < 4; w++)
            {
                GameObject go = Managers.Resource.instantiate("UI/Slot", content.transform);
                RectTransform rectTransform = go.GetComponent<RectTransform>();

                rectTransform.localScale = new Vector2(1, 1);
                rectTransform.anchorMax = new Vector2(0.5f, 1f);
                rectTransform.anchorMin = new Vector2(0.5f, 1f);
                rectTransform.anchoredPosition = new Vector2(0.5f, 1f);
                rectTransform.localPosition = new Vector2(-360 + 240 * w, -130 - 245 * h);

                AddUIEvent(go, (PointerEventData data) =>
                {
                    //item data
                }, Define.UIEvent.Enter);
                AddUIEvent(go, (PointerEventData data) =>
                {
                    
                }, Define.UIEvent.Click);
                AddUIEvent(go, (PointerEventData data) =>
                {
                    evtHandle2.OnBeginDragHandler.Invoke(data);
                }, Define.UIEvent.BeginDrag);
                AddUIEvent(go, (PointerEventData data) =>
                {
                    evtHandle2.OnDragHandler.Invoke(data);
                }, Define.UIEvent.Drag);
                AddUIEvent(go, (PointerEventData data) =>
                {
                    evtHandle2.OnEndDragHandler.Invoke(data);
                }, Define.UIEvent.EndDrag);
            }
        }

        if(height > 5) { content.offsetMin = new Vector2(content.offsetMin.x, -245 * (height - 5)); }
    }
    private void Scroll() { inventoryScrollView.verticalScrollbar.value = Mathf.Clamp(inventoryScrollView.verticalScrollbar.value + direction.y / (content.rect.height + 245 * (height / 5)), 0, 1); }
}