using UnityEngine;
public class InGame : MonoBehaviour
{
    [SerializeField] private SampleFlowerCreator _flowerCreator;
    [SerializeField] private SampleReborn _rebornRightSide = null;
    [SerializeField] private SampleReborn _rebornLeftSide = null;

    void Start()
    {
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