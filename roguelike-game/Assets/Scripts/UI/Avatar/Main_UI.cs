using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Main_UI : UI_Scene
{
    private TextMeshProUGUI timer;
    private TextMeshProUGUI stageName;
    enum Buttons
    {
        Start,
        Help
    }
    enum Images
    {
        StagePanel
    }
    enum TMPro
    {
        Timer,
        StageName
    }
    private void Start() 
    {
        init();

        Transform pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform;

        this.gameObject.transform.SetParent(pos);
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

        rectTransform.sizeDelta = pos.GetComponentInParent<RectTransform>().rect.size;
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
    }
    private void Update() { /*timer.text = $"{}";*/ }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject start = get<Button>((int)Buttons.Start).gameObject;
        GameObject help = get<Button>((int)Buttons.Help).gameObject;
        GameObject stagePanel = get<Image>((int)Images.StagePanel).gameObject;

        timer = get<TextMeshProUGUI>((int)TMPro.Timer);
        stageName = get<TextMeshProUGUI>((int)TMPro.StageName);

        AddUIEvent(start, (PointerEventData data) =>
        {
            //
        }, Define.UIEvent.Click);

        Debug.Log(this.gameObject.transform.parent);
        Debug.Log(this.gameObject.transform.root);
        UI_EventHandler evtHandle = FindParent<UI_EventHandler>(this.gameObject);
        Debug.Log(evtHandle);

        AddUIEvent(stagePanel, (PointerEventData data) => { Managers.UI.showSceneUI<StageSelection_UI>("StageSelection"); }, Define.UIEvent.Click);
        AddUIEvent(stagePanel, (PointerEventData data) =>
        {
#if UNITY_ANDROID
            evtHandle.OnBeginDragHandler.Invoke(data);
#endif
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(stagePanel, (PointerEventData data) =>
        {
#if UNITY_ANDROID
            evtHandle.OnDragHandler.Invoke(data);
#endif
        }, Define.UIEvent.Drag);
        AddUIEvent(stagePanel, (PointerEventData data) =>
        {
#if UNITY_ANDROID
            evtHandle.OnEndDragHandler.Invoke(data);
#endif
        }, Define.UIEvent.EndDrag);
    }
}
