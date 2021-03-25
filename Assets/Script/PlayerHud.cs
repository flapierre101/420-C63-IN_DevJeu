using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    // Start is called before the first frame update
    public Player Player { get; private set; }
    public Text PlayerHealthText;
    public Text PlayerBombText;
    public Text PlayerScoreText;
    public Text GameOverText;
    public Image PlayerHealthImage;

    private void Start()
    {       

        Player = GameManager.Instance.Player;
        Player.Health.OnChanged += OnHealthChanged;
        Player.Bombs.OnChanged += OnBombsChanged;
        Player.Score.OnChanged += OnScoreChanged;

        OnHealthChanged(Player.Health);
        OnBombsChanged(Player.Bombs);
        OnScoreChanged(Player.Score);


    }

    // Update is called once per frame
    void Update()
    {     

        if (Player.getTimer() > 0)
        {
            PlayerHealthText.color = PlayerHealthText.color.WithAlpha(0.5f);
            PlayerHealthImage.color = PlayerHealthText.color.WithAlpha(0.5f);
        }
        else
        {
            PlayerHealthText.color = PlayerHealthText.color.WithAlpha(1.0f);
            PlayerHealthImage.color = PlayerHealthText.color.WithAlpha(1.0f);
        }

        //Petit ajout pour poursuivre la partie!
        if (Input.GetButtonDown("Fire3")&& Player.Health.Value <= 0 && Player.Score.Value >= 500)
        {  
                Player.Score.Value -= 500;
                Player.Health.Value = 5;
                Player.gameObject.SetActive(true);              
        }

    }

    private void OnHealthChanged(Health health)
    {
        if(health.Value == 0)        
            GameOverText.enabled = true;
        else
            GameOverText.enabled = false;

        PlayerHealthText.text = health.Value.ToString();
    }

    private void OnBombsChanged(Bombs bomb)
    {
        PlayerBombText.text = "x " + bomb.Value;
    }

    private void OnScoreChanged(Score score)
    {
        PlayerScoreText.text = score.Value.ToString();
    }
}
