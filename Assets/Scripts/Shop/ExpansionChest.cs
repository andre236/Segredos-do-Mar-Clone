using System;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using Objects;

namespace Shop
{
    public class ExpansionChest : MonoBehaviour
    {

        [SerializeField]
        private int[] _priceEachExpansionChest;

        private Text _titleTXT;
        private Text _priceTXT;
        private Text _currentMaxGoldTXT;
        private Text _nextMaxGoldTXT;

        private DataPlayerManager _dataPlayerManager;
        private GoldChest _goldChest;

        public Func<int, int> BoughtChestExpansion { get; internal set; }

        void Awake()
        {
            _goldChest = FindObjectOfType<GoldChest>();
            _titleTXT = transform.Find("Title").GetComponent<Text>();
            _priceTXT = transform.Find("PriceTXT").GetComponent<Text>();
            _currentMaxGoldTXT = transform.Find("CurrentMaxGoldTXT").GetComponent<Text>();
            _nextMaxGoldTXT = transform.Find("NextMaxGoldTXT").GetComponent<Text>();
            _dataPlayerManager = FindObjectOfType<DataPlayerManager>();

            BoughtChestExpansion += _dataPlayerManager.OnBoughtChestExpansion;
            BoughtChestExpansion += _goldChest.OnBoughtChestExpansion;
        }

        private void OnEnable()
        {
            _currentMaxGoldTXT.text = _dataPlayerManager.CurrentMaxGoldUpgrade[_dataPlayerManager.NumberOfUpgradesChest].ToString();
            _nextMaxGoldTXT.text = _dataPlayerManager.CurrentMaxGoldUpgrade[_dataPlayerManager.NumberOfUpgradesChest+1].ToString();
            _priceTXT.text = _priceEachExpansionChest[_dataPlayerManager.NumberOfUpgradesChest].ToString();

        }

        public void UpgradeChest()
        {
            int lenghtChest = _priceEachExpansionChest.Length - 1;

            if (_dataPlayerManager.BankGold >= _priceEachExpansionChest[_dataPlayerManager.NumberOfUpgradesChest] && _dataPlayerManager.NumberOfUpgradesChest < lenghtChest)
            {
                _dataPlayerManager.OnSpentGold(_priceEachExpansionChest[_dataPlayerManager.NumberOfUpgradesChest]);

                BoughtChestExpansion?.Invoke(_dataPlayerManager.CurrentMaxGoldUpgrade[_dataPlayerManager.NumberOfUpgradesChest +1]);
                
                _priceTXT.text = _priceEachExpansionChest[_dataPlayerManager.NumberOfUpgradesChest].ToString();
                _currentMaxGoldTXT.text = _dataPlayerManager.CurrentMaxGoldUpgrade[_dataPlayerManager.NumberOfUpgradesChest].ToString();
                _nextMaxGoldTXT.text = _dataPlayerManager.CurrentMaxGoldUpgrade[_dataPlayerManager.NumberOfUpgradesChest + 1].ToString();
            }
        }
    }
}