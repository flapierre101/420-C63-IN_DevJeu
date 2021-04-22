using UnityEngine;

public class FacingController : MonoBehaviour
{
  private const string FacingX = "FacingX";
  private const string FacingY = "FacingY";
  public Facing InitialFacing = Facing.Left;
  public Animator Animator;
  private Facing _facing = Facing.Invalid;

  public Facing Facing
  {
    get { return _facing; }
    set
    {
      if (_facing != value)
      {
        _facing = value;

        if (_facing == Facing.Left)
          Animator.SetFloat(FacingX, -1.0f);
        else if (_facing == Facing.Right)
          Animator.SetFloat(FacingX, 1.0f);
        else if (_facing == Facing.Up)
          Animator.SetFloat(FacingY, 1.0f);
        else if (_facing == Facing.Down)
          Animator.SetFloat(FacingY, -1.0f);
      }
    }
  }

  public Vector2 Direction
  {
    get
    {
      switch (Facing)
      {
        case Facing.Right:
          return Vector2.right;
        case Facing.Left:
          return Vector2.left;
        case Facing.Up:
          return Vector2.up;
        case Facing.Down:
          return Vector2.down;
        default:
          return Vector2.zero;
      }
    }
  }

  //public void Flip()
  //{
  //    if (Facing == Facing.Left)
  //        Facing = Facing.Right;
  //    else if (Facing == Facing.Right)
  //        Facing = Facing.Left;
  //}

  private void Awake()
  {
    Facing = InitialFacing;
    Animator = GetComponent<Animator>();
  }
}
