using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour, IPointerClickHandler
{
    LevelTransition transition;

    private void Awake()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        var selection = eventData.rawPointerPress.name;
        Debug.Log(selection);

        if (selection.Equals("StartGame"))
        {
            SceneManager.LoadScene("Town");
        }
        else if (selection.Equals("Quit"))
        {
            Application.Quit();
        }
    }

}
