﻿using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        // -- Stats -- //
        private Image _currentExperienceBar;
        private Text _expTXT;
        private Text _levelTXT;
        private Text _coinTXT;
        private Text _goldPerMinuteTXT;
        private Text _AquariumsTXT;

        // -- Chest -- //
        private Text _maxGoldChest;
        private Text _currentGoldChest;

        // -- Pages -- //
        [SerializeField]
        private GameObject[] _pages;

        // -- Shop -- //
        [SerializeField]
        private GameObject _shopPage;

        // -- Food -- //
        [SerializeField]
        private GameObject _foodPage;
        private Text _normalFoodTXT;
        private Text _averageFoodTXT;
        private Text _superFoodTXT;

        private SpriteRenderer _normalFoodSprite;
        private SpriteRenderer _averageFoodSprite;
        private SpriteRenderer _superFoodSprite;

        // -- Remedy -- //
        [SerializeField]
        private GameObject _remedyPage;
        private Text _remedyTXT;

        private SpriteRenderer _remedySprite;

        // -- Cursor -- //
        [SerializeField]
        private Vector2 _offsetSprites;

        public delegate void UIupdateHandler(int amount);

        private void Awake()
        {
            // -- Experience -- //
            _currentExperienceBar = GameObject.Find("CurrentExperienceBar").GetComponent<Image>();
            _expTXT = GameObject.Find("experienceTXT").GetComponent<Text>();
            _levelTXT = GameObject.Find("LevelTXT").GetComponent<Text>();


            _coinTXT = GameObject.Find("CoinTXT").GetComponent<Text>();
            //_goldPerMinuteTXT = GameObject.Find("GoldPerMinuteTXT").GetComponent<Text>();
            _AquariumsTXT = GameObject.FindGameObjectWithTag("Aquarium").GetComponentInChildren<Text>();

            // -- Food's Sprites of Cursor -- //
            _normalFoodSprite = GameObject.Find("NormalFoodSprite").GetComponent<SpriteRenderer>();
            _normalFoodSprite.gameObject.SetActive(false);

            _averageFoodSprite = GameObject.Find("AverageFoodSprite").GetComponent<SpriteRenderer>();
            _averageFoodSprite.gameObject.SetActive(false);

            _superFoodSprite = GameObject.Find("SuperFoodSprite").GetComponent<SpriteRenderer>();
            _superFoodSprite.gameObject.SetActive(false);

            // -- Sprites of Remedy -- //
            _remedySprite = GameObject.Find("RemedySprite").GetComponent<SpriteRenderer>();
            _remedySprite.gameObject.SetActive(false);


            // -- Chest -- //
            _maxGoldChest = GameObject.Find("MaxGoldChestTXT").GetComponent<Text>();
            _currentGoldChest = GameObject.Find("CurrentGoldTXT").GetComponent<Text>();
            _maxGoldChest.gameObject.SetActive(false);
            _currentGoldChest.gameObject.SetActive(false);
        }

        private void Start()
        {
            DeactiveAllPages();
        }
        // -- Shop -- //
        public void ActivePageShop()
        {
            if (!_shopPage.activeSelf)
            {
                _shopPage.SetActive(true);
                DeactiveAllOthersPage(_shopPage);
            }
            else
            {
                _shopPage.SetActive(false);
            }
        }

        // -- Food UI -- //

        public void CheckActivedFoods()
        {
            // If have no one actived on Scene them return.
            if (!_normalFoodSprite.gameObject.activeSelf && !_averageFoodSprite.gameObject.activeSelf && !_superFoodSprite.gameObject.activeSelf)
            {
                return;
            }
            else
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                _normalFoodSprite.transform.position = new Vector3(mousePosition.x + _offsetSprites.x, mousePosition.y + _offsetSprites.y, 0f);
                _averageFoodSprite.transform.position = new Vector3(mousePosition.x + _offsetSprites.x, mousePosition.y + _offsetSprites.y, 0f);
                _superFoodSprite.transform.position = new Vector3(mousePosition.x + _offsetSprites.x, mousePosition.y + _offsetSprites.y, 0f);
            }

        }

        public void ActiveFoodPage() // Reference on Button in the Scene
        {
            if (!_foodPage.activeSelf)
            {
                _foodPage.SetActive(true);
                DeactiveAllOthersPage(_foodPage);
            }
            else
            {
                _foodPage.SetActive(false);
                return;
            }


            _normalFoodTXT = GameObject.Find("AmountNormalFoodTXT").GetComponent<Text>();
            _normalFoodTXT.text = string.Concat("x", FindObjectOfType<DataPlayerManager>().NormalFood.ToString());
            _averageFoodTXT = GameObject.Find("AmountAverageFoodTXT").GetComponent<Text>();
            _averageFoodTXT.text = string.Concat("x", FindObjectOfType<DataPlayerManager>().AverageFood.ToString());
            _superFoodTXT = GameObject.Find("AmountSuperFoodTXT").GetComponent<Text>();
            _superFoodTXT.text = string.Concat("x", FindObjectOfType<DataPlayerManager>().SuperFood.ToString());

        }

        public void OnUsedNormalFood()
        {
            _normalFoodSprite.GetComponent<Animator>().Play(0);
            _normalFoodTXT.text = string.Concat("x", FindObjectOfType<DataPlayerManager>().NormalFood.ToString());
        }

        public void OnUsedAverageFood()
        {
            _averageFoodSprite.GetComponent<Animator>().Play(0);
            _averageFoodTXT.text = string.Concat("x", FindObjectOfType<DataPlayerManager>().AverageFood.ToString());
        }

        public void OnUsedSuperFood()
        {
            _superFoodSprite.GetComponent<Animator>().Play(0);
            _superFoodTXT.text = string.Concat("x", FindObjectOfType<DataPlayerManager>().SuperFood.ToString());
        }

        public void OnNormalFoodSelected()
        {
            if (!_normalFoodSprite.gameObject.activeSelf)
            {
                _normalFoodSprite.gameObject.SetActive(true);
            }
            else
            {
                _normalFoodSprite.gameObject.SetActive(false);
            }

            _averageFoodSprite.gameObject.SetActive(false);
            _superFoodSprite.gameObject.SetActive(false);
        }

        public void OnAverageFoodSelected()
        {
            if (!_averageFoodSprite.gameObject.activeSelf)
            {
                _averageFoodSprite.gameObject.SetActive(true);
            }
            else
            {
                _averageFoodSprite.gameObject.SetActive(false);
            }

            _superFoodSprite.gameObject.SetActive(false);
            _normalFoodSprite.gameObject.SetActive(false);
        }

        public void OnSuperFoodSelected()
        {
            if (!_superFoodSprite.gameObject.activeSelf)
            {
                _superFoodSprite.gameObject.SetActive(true);
            }
            else
            {
                _superFoodSprite.gameObject.SetActive(false);
            }

            _averageFoodSprite.gameObject.SetActive(false);
            _normalFoodSprite.gameObject.SetActive(false);
        }
        // -- Remedy -- //
        public void CheckActivedRemedy()
        {
            if (!_remedySprite.gameObject.activeSelf)
                return;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _remedySprite.transform.position = new Vector3(mousePosition.x + _offsetSprites.x, mousePosition.y + _offsetSprites.y, 0f);

        }

        public void ActiveRemedyPage()
        {
            if (!_remedyPage.activeSelf)
            {
                _remedyPage.SetActive(true);
                DeactiveAllOthersPage(_remedyPage);
            }
            else
            {
                _remedyPage.SetActive(false);
                return;
            }

            _remedyTXT = GameObject.Find("RemedyAmountTXT").GetComponent<Text>();
            _remedyTXT.text = string.Concat("x", FindObjectOfType<DataPlayerManager>().NormalRemedy.ToString());

        }

        public void OnUsedRemedy()
        {
            _remedySprite.GetComponent<Animator>().Play(0);
            _remedyTXT.text = string.Concat("x", FindObjectOfType<DataPlayerManager>().NormalRemedy.ToString());

        }

        public void OnRemedySelected()
        {
            if (!_remedySprite.gameObject.activeSelf)
            {
                _remedySprite.gameObject.SetActive(true);
            }
            else
            {
                _remedySprite.gameObject.SetActive(false);
            }
        }

        // -- Items Generic -- //
        internal void OnUnselectedAllItems()
        {
            _normalFoodSprite.gameObject.SetActive(false);
            _averageFoodSprite.gameObject.SetActive(false);
            _remedySprite.gameObject.SetActive(false);
            _superFoodSprite.gameObject.SetActive(false);
        }

        // -- Fishes on Aquarium -- //

        public void OnFishesAquarium(string currentAquariumAmounText, string maxAquariumSize)
        {
            _AquariumsTXT.text = string.Concat(currentAquariumAmounText , "/" , maxAquariumSize);
        }

        public void OnPlayerGoldEarned(int amount)
        {
            _coinTXT.text = amount.ToString();
        }

        public void OnEarnedExperience(float exp, float nextLevelExp, int currentLevel)
        {

            if (exp != 0)
                _currentExperienceBar.fillAmount = exp / nextLevelExp;
            else
                _currentExperienceBar.fillAmount = 0;

            _expTXT.text = string.Concat(Mathf.Abs(exp).ToString() , "/" , Mathf.Abs(nextLevelExp).ToString());

            _levelTXT.text = currentLevel.ToString();
        }

        public void OnShowCurrentChestGold(int maxChestGold, int currentChestGold, bool activeGameObject)
        {
            _maxGoldChest.text = string.Concat("Capacity: " , maxChestGold.ToString());
            _currentGoldChest.text = string.Concat("Gold: " , currentChestGold.ToString());
            _maxGoldChest.gameObject.SetActive(activeGameObject);
            _currentGoldChest.gameObject.SetActive(activeGameObject);
        }

        // -- Pages -- //
        public void DeactiveAllPages()
        {
            if (_pages.Length <= 0)
                return;

            for (int i = 0; i < _pages.Length; i++)
            {
                _pages[i].SetActive(false);
            }
        }

        public void DeactiveAllOthersPage(GameObject currentPage)
        {
            for (int i = 0; i < _pages.Length; i++)
            {
                if (_pages[i] != currentPage)
                {
                    _pages[i].SetActive(false);
                }
            }
        }

    }
}