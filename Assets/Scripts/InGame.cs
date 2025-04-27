using UnityEngine;
public class InGame : MonoBehaviour
{
    [SerializeField] private FlowerStartAnimation _flowerStartAnimation;
    [SerializeField] private SampleFlowerCreator _flowerCreator;
    [SerializeField] private SampleReborn _rebornLeftSide = null;
    [SerializeField] private SampleReborn _rebornRightSide = null;

    void Start()
    {
        _flowerStartAnimation.ManualStart();
        _flowerCreator.ManualStart();
        _rebornLeftSide.ManualStart();
        _rebornRightSide.ManualStart();
    }

    void Update()
    {
        _flowerCreator.ManualUpdate();
        _rebornLeftSide.ManualUpdate();
        _rebornRightSide.ManualUpdate();
    }
}