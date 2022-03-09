using UnityEngine;
using UnityEngine.UI;
using Manager;
using System;

namespace Shop
{
    public class UpgradesShop : MonoBehaviour
    {
        [SerializeField]
        private int _priceExpansionAquarium;
        [SerializeField]
        private int _priceExpansionChest;
        private Text _priceTXT;

        private DataPlayerManager _dataPlayerManager;



        private void OnEnable()
        {
            _dataPlayerManager = FindObjectOfType<DataPlayerManager>();
            _priceTXT = transform.Find("PriceTXT").GetComponent<Text>();
            _priceTXT.text = _priceExpansionAquarium.ToString();
            //BoughtExpansion += _dataPlayerManager.OnBoughtExpasion;
        }


    }
}