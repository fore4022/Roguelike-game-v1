using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using TMPro;
public class Stat_UI : UI_Popup
{
    enum Buttons
    {
        exit
    }
    enum Images
    {

    }
    private void Start()
    {
        init();
        Managers.Game.player.updateStat += statUpdate;
    }
    protected override void init()
    {
        base.init();
        
    }
    private void statUpdate()
    {

    }
}
