using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private SampleFlowerCreator _flowerCreator;

    void Start()
    {
        _flowerCreator.ManualStart();
    }

    void Update()
    {
        _flowerCreator.ManualUpdate();
    }
}