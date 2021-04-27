using UnityEngine;

namespace DiMe.Urarulla
{
    public class MainScene : MonoBehaviour
    {
        private ChairHandler chairHandler;
        private Transform characterHolder;
        
        [Range(0f, 1f)]
        [SerializeField]
        private float chairOccupationPercentage = .2f;

        private void Start()
        {
            characterHolder = transform.Find("characters");
            chairHandler = transform.Find("stage").GetComponentInChildren<ChairHandler>();

            SpawnCharacters((int)((float)chairHandler.OpenChairCount * chairOccupationPercentage));
        }

        private void SpawnCharacters(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var chair = chairHandler.GetChair();
                if (chair == null)
                    return;
                var model = GameManager.RandomCharacterModel;
                if (model == null)
                    return;
                var character = Instantiate(model, chair.position, chair.rotation, characterHolder).transform;
                character.Rotate(0f, 90f, 0f, Space.Self);
            }
        }

        internal static void CreateNewPlayerGameStand()
        {
            
        }
    }
}
