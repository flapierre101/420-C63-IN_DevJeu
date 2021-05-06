using UnityEngine;

public class Sorcerer : MonoBehaviour
{
  public BoxCollider2D SorcererCollider;
  public Animator Animator;
  public SpriteRenderer SpriteX;
  public Health SorcererHealth;
  public FacingController FacingController;
  public Transform SpawnPoint;




  private void Awake()
  {
    SorcererCollider = GetComponent<BoxCollider2D>();
    Animator = GetComponent<Animator>();
    SpriteX = GetComponent<SpriteRenderer>();
    SorcererHealth = GetComponent<Health>();
    FacingController = GetComponent<FacingController>();
  }

  void Update()
  {

  }
}
