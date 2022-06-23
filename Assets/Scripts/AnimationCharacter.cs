using UnityEngine;

public class AnimationCharacter : MonoBehaviour
{
    //Components
    Animator myAnimator;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack(bool stateAttacking)
    {
        myAnimator.SetBool(Constants.ANIMATOR_ATTACKING, stateAttacking);
    }

    public void Walk(float valueWalk)
    {
        myAnimator.SetFloat(Constants.ANIMATOR_RUNNING, valueWalk);
    }

    public void Die()
    {
        myAnimator.SetTrigger(Constants.ANIMATOR_DIE);
    }
}
