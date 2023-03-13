using System.Collections;
using UnityEngine;

public class KeysTutorial : MonoBehaviour
{
    [SerializeField] GameObject Tutorial;

    [SerializeField] GameObject XboxControllText;
    [SerializeField] GameObject PCControllText;

    bool showedKeyBindings = false;
    bool isShowing = false;

    void Update()
    {
        if (showedKeyBindings == false)
        {
            isShowing = true;
            StartCoroutine(HideKeyBindiingsAfterSeccounds());
        }
        else if (SaveScript.isGameOver)
            isShowing = true;
        else if (showedKeyBindings)
            isShowing = false;

        Tutorial.SetActive(isShowing);
        XboxControllText.SetActive(Input.GetJoystickNames().Length > 0);
        PCControllText.SetActive(Input.GetJoystickNames().Length == 0);

    }
    IEnumerator HideKeyBindiingsAfterSeccounds()
    {
        yield return new WaitForSeconds(12f);
        Tutorial.SetActive(false);
        showedKeyBindings = true;
    }
}
