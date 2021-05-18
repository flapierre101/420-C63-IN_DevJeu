using UnityEngine;

public class Bootstrap : MonoBehaviour

{
  private GameManager instance;
  public SoundManager.Music music;

  private void Awake()
  {
    instance = GameManager.Instance;
    instance.bootstrap();
    instance.SoundManager.Play(music);
  }
}
