using System;
using UnityEngine;
using UnityEngine.UI;
using Manager;

namespace Shop
{
    public class ExpansionAquarium : MonoBehaviour
    {
        [SerializeField]
        private int[] _priceExpansionAquarium;

        private Text _titleTXT;
        private Text _priceTXT;
        private DataPlayerManager _dataPlayerManager;
        private EventManager _eventManager;

        public Action BoughtAquariumExpansion;

        private void Awake()
        {
            _titleTXT = transform.Find("TitleTXT").GetComponent<Text>();
            _priceTXT = transform.Find("PriceTXT").GetComponent<Text>();
            _dataPlayerManager = FindObjectOfType<DataPlayerManager>();
            _eventManager = FindObjectOfType<EventManager>();

            BoughtAquariumExpansion += _dataPlayerManager.OnBoughtAquariumExpansion;
            BoughtAquariumExpansion += _eventManager.OnBoughtAquariumExpansion;
        }

        private void OnEnable()
        {
            _priceTXT.text = _priceExpansionAquarium[_dataPlayerManager.NumberOfUpgradesAquarium].ToString();

        }

        public void UpgradeAquarium()
        {
            int lenghtAquarium = _priceExpansionAquarium.Length - 1;

            if (_dataPlayerManager.BankGold >= _priceExpansionAquarium[_dataPlayerManager.NumberOfUpgradesAquarium] && _dataPlayerManager.NumberOfUpgradesAquarium < lenghtAquarium)
            {
                _dataPlayerManager.OnSpentGold(_priceExpansionAquarium[_dataPlayerManager.NumberOfUpgradesAquarium]);
                BoughtAquariumExpansion?.Invoke();
                _priceTXT.text = _priceExpansionAquarium[_dataPlayerManager.NumberOfUpgradesAquarium].ToString();
            }
            else
            {
                Debug.Log("Algo deu errado.");
            }
        }

    }
}