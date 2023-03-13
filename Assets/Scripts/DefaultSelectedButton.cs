using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultSelectedButton : MonoBehaviour
{
    [SerializeField] GameObject DefaultButton;
    bool changed = false;

    private void Update()
    {
        if ((EventSystem.current.currentSelectedGameObject != DefaultButton && !changed) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectDefaultButton();
            changed = true;
        }
    }

    public void SelectDefaultButton()
    {
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);


        //set a new selected object
        EventSystem.current.SetSelectedGameObject(DefaultButton);

    }

    private void OnDisable()
    {
        changed = false;
    }

    private void OnEnable()
    {
        SelectDefaultButton();
        changed = false;
    }
}
