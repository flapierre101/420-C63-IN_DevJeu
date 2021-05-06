using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour, IPointerClickHandler
{
    LevelTransition transition;
    public AudioSource secret;
    public Image background;
    public Sprite sprite;


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
        else if (selection.Equals("MarthaClose"))
        {
            secret.Play();
            background.sprite = sprite;
        }
    }

}
