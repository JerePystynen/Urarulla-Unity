using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DiMe.Urarulla
{
    public class CreatorAttributes : MonoBehaviour
    {
        [SerializeField] private CharacteristicInfos _characteristicInfos;
        [SerializeField] private Dictionary<string, Characteristics> _skillDictionary;
        [SerializeField] private CharacteristicSkill[] _skills;
        
        private TMP_Dropdown _dropdown;

        private void Start()
        {
            _characteristicInfos = GameManager.Instance.characteristicInfos;
            _skills = _characteristicInfos.skills;
            _dropdown = transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
            SetDropdown();
        }

        private void SetDropdown()
        {
            _dropdown.ClearOptions();
            var list = new List<TMP_Dropdown.OptionData>();
            foreach (var skill in _skills)
            {
                list.Add(new TMP_Dropdown.OptionData(skill.name));
                _skillDictionary.Add(skill.name, skill.characteristics);
            }
            _dropdown.AddOptions(list);
            _dropdown.onValueChanged.AddListener(delegate { SetValue(_dropdown.value); });
        }

        private void SetValue(int index)
        {
            Debug.Log($"added a skill to the player {_skills[index]}");
        }
    }
}