using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Product")]
public class ProductObject : ScriptableObject
{
    public string Name;
    public string Description;
    public int Price;
    public int Amount;
    public Sprite Sprite;
    public RuntimeAnimatorController ProductAnimatorController;
    public Action<int> BoughtProduct;

}
