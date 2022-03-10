using UnityEngine;
using UnityEngine.UI;

namespace Miscellaneous
{
    public class ControlSectionsAchievements : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _sections;
        private int _currentPage;
        [SerializeField]
        private Text _pageTXT;

        void OnEnable()
        {
            _pageTXT.text = string.Concat(_currentPage+1, "/", _sections.Length);

            for (int i = 0; i < _sections.Length; i++)
            {
                _sections[i].SetActive(false);
            }
            _sections[_currentPage].SetActive(true);
        }

        public void NextPage()
        {
            var lenght = _sections.Length - 1;

            for (int i = 0; i < _sections.Length; i++)
            {
                _sections[i].SetActive(false);
            }

            if (_currentPage < lenght)
                _currentPage++;

            _pageTXT.text = string.Concat(_currentPage +1, "/", _sections.Length);
            _sections[_currentPage].SetActive(true);
        }

        public void PreviousPage()
        {
            for (int i = 0; i < _sections.Length; i++)
            {
                _sections[i].SetActive(false);
            }

            if (_currentPage > 0)
                _currentPage--;
            else
                _currentPage = 0;

            _pageTXT.text = string.Concat(_currentPage + 1, "/", _sections.Length);
            _sections[_currentPage].SetActive(true);
        }
    }
}