using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Create New Item/New Item")]
public class Item : ScriptableObject
{
    public enum Rating
    {

    }

    public string itemName;
    public string explanation;
    public string itemImage;

    public int hp = 1;
    public int damage = 1;
    public int skillCooldownReduction;

    public float moveSpeed = 1;
    public float expMagnification = 1;
    public float goldMagnification = 1;
}