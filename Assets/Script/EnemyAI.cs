using UnityEngine;

public class EnemyAI : MonoBehaviour
{
  private Player player;
  private BoxCollider2D playerCollider;
  public bool playerDetected;
  public bool PlayerInAttackRange;
  public bool PatrolX;
  public float PatrolMin;
  public float PatrolMax;
  public float RangeDetection;
  public float AttackRange;
  public Rigidbody2D Rigidbody2D;
  private FacingController facingController;

  private void Awake()
  {
    player = GameManager.Instance.Player;
    facingController = GetComponent<FacingController>();
    playerCollider = GameManager.Instance.Player.GetComponent<BoxCollider2D>();
    Rigidbody2D = GetComponent<Rigidbody2D>();
    playerDetected = false;
    PlayerInAttackRange = false;
  }

  // Update is called once per frame
  void Update()
  {

    if (PatrolX)                                                                                      // If enemy patrolling on X axis
    {
      if (player.isActiveAndEnabled)
      {
        var playerPositionX = playerCollider.bounds.max.x;
        var enemyPositionX = gameObject.GetComponent<BoxCollider2D>().bounds.min.x;
        var enemyMinY = gameObject.GetComponent<BoxCollider2D>().bounds.min.y;
        var enemyMaxY = gameObject.GetComponent<BoxCollider2D>().bounds.max.y;
        var playerMinY = playerCollider.bounds.min.y;
        var playerMaxY = playerCollider.bounds.max.y;

        float xDelta = Mathf.Abs(enemyPositionX - playerPositionX);

        if (facingController.Facing == Facing.Left && playerPositionX < enemyPositionX)
        {
          if (xDelta < RangeDetection && enemyMinY > playerMinY && enemyMaxY > playerMaxY)                       // determine if enemy can "see" player when facing LEFT
          {
            playerDetected = true;

            if (xDelta < AttackRange)
            {
              PlayerInAttackRange = true;
            }
          }
        }
        else if (facingController.Facing == Facing.Right && playerPositionX > enemyPositionX)                        // determine if enemy can "see" when facing RIGHT, avoids player detection when facing opposites
        {
          if (xDelta < RangeDetection && enemyMinY > playerMinY && enemyMaxY > playerMaxY)
          {
            playerDetected = true;

            if (xDelta < AttackRange)
            {
              PlayerInAttackRange = true;
            }
          }
        }                                                                                          // reset variables if player out of range
        else
        {
          playerDetected = false;
          PlayerInAttackRange = false;
        }
      }
      else
      {                                                                                           // reset variable if player inactive/dead
        playerDetected = false;
        PlayerInAttackRange = false;
      }

      if (!playerDetected)                                                                        // normal patrolling behavior depending on min and max coordinates
      {
        if (gameObject.transform.position.x >= PatrolMax)
        {
          facingController.Facing = Facing.Left;
        }
        else if (gameObject.transform.position.x <= PatrolMin)
        {
          facingController.Facing = Facing.Right;
        }
      }
    }                                                                       // If enemy patrolling on Y axis
    else
    {
      if (player.isActiveAndEnabled)
      {
        var playerPositionY = playerCollider.bounds.max.y;
        var enemyPositionY = gameObject.GetComponent<BoxCollider2D>().bounds.min.y;
        //var enemyMinX = gameObject.GetComponent<BoxCollider2D>().bounds.min.x;
        //var enemyMaxX = gameObject.GetComponent<BoxCollider2D>().bounds.max.x;
        //var playerCenterX = playerCollider.bounds.center.x;
        var enemyCenterX = gameObject.GetComponent<BoxCollider2D>().bounds.center.x;
        var playerMinX = playerCollider.bounds.min.x;
        var playerMaxX = playerCollider.bounds.max.x;


        float yDelta = Mathf.Abs(enemyPositionY - playerPositionY);

        Debug.Log("PLAYER Y: " + playerPositionY);
        Debug.Log("ENEMY Y: " + enemyPositionY);
        Debug.Log("DELTA Y: " + yDelta);




        if (facingController.Facing == Facing.Down && playerPositionY < enemyPositionY)
        {
          if (yDelta < RangeDetection && playerMinX < enemyCenterX && playerMaxX > enemyCenterX)                     // determine if enemy can "see" player when facing DOWN
          {
            playerDetected = true;

            if (yDelta < AttackRange)
            {
              PlayerInAttackRange = true;
            }
          }
        }
        else if (facingController.Facing == Facing.Up && playerPositionY > enemyPositionY)
        {
          if (yDelta < RangeDetection && playerMinX < enemyCenterX && playerMaxX > enemyCenterX)                     // determine if enemy can "see" player when facing UP
          {
            playerDetected = true;

            if (yDelta < AttackRange)
            {
              PlayerInAttackRange = true;
            }
          }
        }                                                                                         // reset variables if player out of range
        else
        {
          playerDetected = false;
          PlayerInAttackRange = false;
        }
      }
      else
      {                                                                                           // reset variable if player inactive/dead
        playerDetected = false;
        PlayerInAttackRange = false;
      }

      if (!playerDetected)
      {
        if (gameObject.transform.position.y >= PatrolMax)
        {
          facingController.Facing = Facing.Down;
        }
        else if (gameObject.transform.position.y <= PatrolMin)
        {
          facingController.Facing = Facing.Up;
        }
      }
    }
  }
}
