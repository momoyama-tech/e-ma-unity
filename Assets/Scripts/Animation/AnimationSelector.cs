using UnityEngine;

public class AnimationSelector : MonoBehaviour
{
    private Animator animator;
    private int animationIndex;
    [SerializeField] private RuntimeAnimatorController[] animationControllers;

    void Start()
    {
        animationIndex = Random.Range(0, 2);

        animator = GetComponent<Animator>();
        if (animationIndex == 0)
        {
            animator.runtimeAnimatorController = animationControllers[0];
        }
        else if (animationIndex == 1)
        {
            animator.runtimeAnimatorController = animationControllers[1];
        }
    }

    void Update()
    {
        
    }
}