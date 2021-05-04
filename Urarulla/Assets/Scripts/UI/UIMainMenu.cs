using UnityEngine;

namespace DiMe.Urarulla
{
    public class UIMainMenu : MonoBehaviour
    {
        internal void StartButton(MainMenuStartBtn btn)
        {
            if (GameManager.Players.Count == 0)
            {
                GameManager.AddPlayer($"Player {GameManager.Instance.players.Count}");
            }
            else
            {
                // foreach ()
                // {
                // }
            }

            GameManager.Instance.SetMenuActive(1);
            btn.ResetButton();
        }
    }
}