using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urarulla
{
    public class Pointer : MonoBehaviour
    {
        private GameObject theWheel;
        public GameObject jsonObject;

        public bool firstSpin;

        private List<string> jsonNameList = new List<string>() {
            "Green",
            "Yellow",
            "Orange",
            "Pink",
            "Purple",
            "Blue",
            "Black",
            "White",
        };

        private LuckWheel theWheelScript;
        private JsonData jsonScript;
        private BoxCollider boxCol;

        private void Start()
        {
            //Searches for the object which has the JSON script attached
            jsonScript = FindObjectOfType<JsonData>();
            jsonObject = jsonScript.gameObject;

            //Searches for the object which has the WHEEL script attached
            theWheelScript = FindObjectOfType<LuckWheel>();
            theWheel = theWheelScript.gameObject;

            //searches and disbales the collider of the object this script is attached (Pointer object)
            //the collider enables the pointer to detect the colors of the wheel through the use of tags and colliders
            boxCol = GetComponent<BoxCollider>();
            boxCol.enabled = false;
        }

        // private void Update()
        // {
        //     // if the wheel is in the state of not spinning and the first spin has been done
        //     // the pointers collider is enabled which can then detect the colors of the wheel
        //     if (theWheelScript.canSpin && firstSpin)
        //     {
        //         boxCol.enabled = true;
        //     }
        //     // if the above mentioned conditions are not met disables the collider
        //     // Usually when the wheel is spinning so it doesnt detect the colors while spinning and only when stopped
        //     else
        //     {
        //         boxCol.enabled = false;
        //     }
        // }

        private void OnTriggerEnter(Collider coll)
        {
            CheckIfColliderTagMatchesJsonNameListItem(coll.tag);
        }

        private void CheckIfColliderTagMatchesJsonNameListItem(string tag)
        {
            if (!jsonNameList.Contains(tag))
                return;

            int index = jsonNameList.IndexOf(tag) + 1;

            jsonScript.currentQuestionColorNum = index;

            //replaces the json file on the json script with the json having the correct questions
            string name = jsonNameList[index];
            string fileName = $"Json/question{name}";
            jsonScript.JSONfile = Resources.Load<TextAsset>(fileName);

            //activates the questions script
            jsonScript.Run(index);
        }
    }
}