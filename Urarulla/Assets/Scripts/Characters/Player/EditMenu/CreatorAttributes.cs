using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class CreatorAttributes : MonoBehaviour
    {
        // Data
        private CharacteristicInfo[] _characteristicInfos;
        private CharacteristicSkill[] _skills;

        public List<CharacteristicSkill> addedSkills;

        // References
        private Transform _characterAttributes;
        private Transform _characterAttributeScrollView;

        private TMP_Text _labelText;
        private TMP_Dropdown _dropdown;

        internal void SetDataAndDropdown()
        {
            _skills = GameManager.CharacteristicSkills;

            _characterAttributes = transform.Find("character-attributes");
            _characterAttributeScrollView = _characterAttributes.Find("scroll-view");
            
            _dropdown = _characterAttributes.Find("add-element-dropdown").GetComponent<TMP_Dropdown>();
            SetDropdown();

            _characterAttributes.Find("randomize-btn").GetComponent<Button>().onClick.AddListener(delegate { RandomizeAttributes(); });
            
            _labelText = _dropdown.transform.Find("label-txt").GetComponent<TMP_Text>();
        }

        private void SetDropdown()
        {
            _dropdown.ClearOptions();
            
            if (_skills == null)
            {
                Debug.LogError("error: _skills is null!");
                return;
            }
            
            var list = new List<TMP_Dropdown.OptionData>();
            foreach (var skill in _skills)
            {
                list.Add(new TMP_Dropdown.OptionData(skill.name));
            }
            
            _dropdown.AddOptions(list);
            _dropdown.onValueChanged.AddListener(delegate { AddSkill(_dropdown.value); });
        }

        private void AddSkill(int index)
        {
            var target = _skills[index];
            Debug.Log($"added a skill to the player {target.name}");
            addedSkills.Add(target);

            _labelText.text = "Lisää vielä (3)...";
        }

        private void RandomizeAttributes()
        {
            for (var i = Random.Range(1, 3); i > 0; i--)
            {
                AddSkill(Random.Range(0, _skills.Length));
            }
        }
    }
}