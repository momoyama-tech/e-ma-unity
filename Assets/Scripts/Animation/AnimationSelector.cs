using UnityEngine;

public class AnimationSelector : MonoBehaviour
{
    private Animator animator;
    private int animationIndex;
    [SerializeField] private RuntimeAnimatorController transformController;
    [SerializeField] private RuntimeAnimatorController rotationController;

    void Start()
    {
        animationIndex = Random.Range(0, 2);

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