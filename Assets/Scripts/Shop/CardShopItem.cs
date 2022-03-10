using UnityEngine;
using UnityEngine.UI;
using Manager;
using System;

namespace Shop
{
    public class CardShopItem : MonoBehaviour
    {
        [SerializeField]
        private bool _isUpgradeProduct;

        // -- Product Configs Generic -- //
        [SerializeField]
        private ProductObject _productObject;
        private Text _productTitleTXT;
        private Text _productPriceTXT;
        private Image _productImage;
        private AudioSource _buySound;
        private Animator _animatorProduct;

        private EventManager _eventManager;
        private DataPlayerManager _dataPlayerManager;

        public Action<string, int> BoughtItem { get; internal set; }

        public Action AddedOnCardShopList { get; internal set; }
        public Action BoughtExpansion { get; internal set; }


        private void Awake()
        {
            _dataPlayerManager = FindObjectOfType<DataPlayerManager>();
            _eventManager = FindObjectOfType<EventManager>();

            _productTitleTXT = transform.Find("ProductTitleTXT").GetComponent<Text>();
            _productPriceTXT = transform.Find("ProductPriceTXT").GetComponent<Text>();
            _productImage = transform.Find("ProductSprite").GetComponent<Image>();
            _buySound = transform.Find("BuySound").GetComponent<AudioSource>();
            BoughtItem += _dataPlayerManager.OnBoughtItem;

        }

        private void OnEnable()
        {
            _productTitleTXT.text = _productObject.Name;
            _productPriceTXT.text = _productObject.Price.ToString();
            _productImage.sprite = _productObject.Sprite;
            if (_productObject.ProductAnimatorController != null)
                _animatorProduct.runtimeAnimatorController = _productObject.ProductAnimatorController;
            
        }

        public void BuyingItem()
        {
            if (_dataPlayerManager.BankGold >= _productObject.Price)
            {
                // Spent of Gold
                _buySound.Play();
                _dataPlayerManager.OnSpentGold(_productObject.Price);
                BoughtItem?.Invoke(_productObject.Name, _productObject.Amount);
            }
        }







        //public void BoughtFood()
        //{
        //    if (_dataPlayerManager.BankGold >= _productObject.Price)
        //    {
        //        _dataPlayerManager.OnSpentGold(_productObject.Price);
        //        _dataPlayerManager.OnBoughtItem(_productObject.Name, _productObject.Amount);

        //    }
        //    else
        //    {
        //        Debug.Log("Dinheir insuficiente");
        //    }

        //}
    }
}