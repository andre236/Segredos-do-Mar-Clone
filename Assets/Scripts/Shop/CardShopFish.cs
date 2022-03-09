using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Entities;
using Manager;

namespace Shop
{
    public class CardShopFish : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        // -- Fish Configs -- // 
        [SerializeField]
        private FishObject _fishSettings;
        private Text _nameFishTXT;
        private Text _revenueTXT;
        private Text _lifeTimeTXT;
        private Text _requiredLevelTXT;
        private Text _productTitleTXT;
        private Text _productPriceTXT;

        private Image _productImage;
        private Animator _animatorFish;
        private Button _buyButton;
        private GameObject _popUp;

        private EventManager _eventManager;
        private DataPlayerManager _dataPlayerManager;
        public GameObject PrefabFish { get; private set; }

        public Action AddedFishList { get; internal set; }
        public Action AddedOnCardShopList { get; private set; }

        void Awake()
        {
            _dataPlayerManager = FindObjectOfType<DataPlayerManager>();
            _eventManager = FindObjectOfType<EventManager>();
            _popUp = transform.Find("PopUp").gameObject;
            _lifeTimeTXT = _popUp.transform.Find("LifeSpanTXT").GetComponent<Text>();
            _nameFishTXT = _popUp.transform.Find("NameFishTXT").GetComponent<Text>();
            _revenueTXT = _popUp.transform.Find("RevenueTXT").GetComponent<Text>();
            _requiredLevelTXT = transform.Find("PopUp").transform.Find("RequiredLevelTXT").GetComponent<Text>();

            _productTitleTXT = transform.Find("ProductTitleTXT").GetComponent<Text>();
            _productPriceTXT = transform.Find("ProductPriceTXT").GetComponent<Text>();
            _productImage = transform.Find("ProductSprite").GetComponent<Image>();
            _animatorFish = transform.Find("ProductSprite").GetComponent<Animator>();
            _buyButton = transform.Find("BuyButton").GetComponent<Button>();
            AddedFishList += _eventManager.OnAddedFishList;
        }



        private void OnEnable()
        {
            PrefabFish = _fishSettings.PrefabFish;

            _productImage.sprite = _fishSettings.SpriteFish;
            _animatorFish.runtimeAnimatorController = _fishSettings.ControllerAnimImage;

            //Text
            _productTitleTXT.text = _fishSettings.NameFish;
            _nameFishTXT.text = _fishSettings.NameFish;
            _productPriceTXT.text = _fishSettings.PriceFish.ToString();

            _revenueTXT.text = string.Concat("Revenue:     ", Mathf.Abs((_fishSettings.GoldRevenue * 60) / _fishSettings.DelayGold).ToString("F2"), " per minute.");
            _lifeTimeTXT.text = string.Concat("Lifespan:     ", Mathf.Round(_fishSettings.MaxLifeTime / 60), " minutes");
            _requiredLevelTXT.text = string.Concat("Required Level: ", _fishSettings.RequiredLevel.ToString());

            //AddedOnCardShopList += _eventManager.OnAddedOnCardShopList;
            _popUp.SetActive(false);
        }

        private void Update()
        {
            if (!gameObject.activeSelf)
                return;
            else
                _buyButton.interactable = _dataPlayerManager.CurrentLevel >= _fishSettings.RequiredLevel;
        }

        public void BuyFish()
        {
            if (_dataPlayerManager.BankGold >= _fishSettings.PriceFish && _eventManager.FishList.Count < _dataPlayerManager.MaxSizeAquarium)
            {
                // Spent of Gold
                _dataPlayerManager.OnSpentGold(_fishSettings.PriceFish);
                Instantiate(PrefabFish, GameObject.Find("FishesOnAquarium").transform);
                AddedFishList?.Invoke();
            }
            else
            {
                // Pop up showing the player haven't money
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _popUp.transform.SetParent(GameObject.Find("CurrentCardPopUp").transform);
            _popUp.SetActive(true);

        }

        public void OnPointerExit(PointerEventData eventData)
        {

            _popUp.transform.SetParent(gameObject.transform);
            _popUp.SetActive(false);

        }
    }
}