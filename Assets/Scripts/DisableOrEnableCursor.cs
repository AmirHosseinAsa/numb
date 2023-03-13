using UnityEngine;

public class DisableOrEnableCursor : MonoBehaviour
{
    void Update()
    {
        Cursor.visible = SaveScript.isGameOver || SaveScript.isInMainMenu;
    }
}
