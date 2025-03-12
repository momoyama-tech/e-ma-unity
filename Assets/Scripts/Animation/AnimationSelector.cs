using UnityEngine;

public class AnimationSelector : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int animationIndex;
    [SerializeField] private RuntimeAnimatorController transformController;
    [SerializeField] private RuntimeAnimatorController rotationController;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animationIndex == 0)
        {
            animator.runtimeAnimatorController = transformController;
        }
        else if (animationIndex == 1)
        {
            animator.runtimeAnimatorController = rotationController;
        }
    }

    void Update()
    {
        
    }
}