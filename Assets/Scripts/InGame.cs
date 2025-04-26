using UnityEngine;
public class InGame : MonoBehaviour
{
    [SerializeField] private SampleFlowerCreator _flowerCreator;
    [SerializeField] private SampleReborn _rebornLeftSide = null;
    // [SerializeField] private SampleReborn _rebornRightSide = null;

    void Start()
    {
        _flowerCreator.ManualStart();
        _rebornLeftSide.ManualStart();
        // _rebornRightSide.ManualStart();
    }

    void Update()
    {
        _flowerCreator.ManualUpdate();
        _rebornLeftSide.ManualUpdate();
        // _rebornRightSide.ManualUpdate();
    }
}