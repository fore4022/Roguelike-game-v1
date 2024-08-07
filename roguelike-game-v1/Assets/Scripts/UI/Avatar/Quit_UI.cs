using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Quit_UI : UI_Popup
{
    enum Images
    {
        Quit
    }
    private void Start() { Init(); }
    protected override void Init()
    {
        base.Init();
        bind<Image>(typeof(Images));

        GameObject quit = get<Image>((int)Images.Quit).gameObject;

        AddUIEvent(quit, (PointerEventData data) => { Application.Quit(); }, Define.UIEvent.Click);
    }
}
