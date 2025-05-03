using UnityEngine;
public class InGame : MonoBehaviour
{
    [SerializeField] private GameObject _emaCreator;
    [SerializeField] private GameObject _creatorContoroller;
    [SerializeField] private FlowerStartAnimation _flowerStartAnimation;
    [SerializeField] private SampleFlowerCreator _flowerCreator;
    [SerializeField] private SampleReborn _rebornLeftSide = null;
    [SerializeField] private SampleReborn _rebornRightSide = null;
    [SerializeField] private int _animalBornFastestTime = 0;
    [SerializeField] private int _animalBornSlowestTime = 0;
    private float _randNumLeftSide = 0;
    private float _randNumRightSide = 0;
    private float _frameCounterLeftSide = 0;
    private float _frameCounterRightSide = 0;

    void Start()
    {
        _emaCreator.GetComponent<EmaCreator>().ManualStart();
        // _creatorContoroller.GetComponent<CreateController>().ManualStart();
        // _flowerStartAnimation.ManualStart();
        // _flowerCreator.ManualStart();
        // _rebornLeftSide.ManualStart();
        // _rebornRightSide.ManualStart();

        // _randNumLeftSide = Random.Range(_animalBornFastestTime, _animalBornSlowestTime);
        // _randNumRightSide = Random.Range(_animalBornFastestTime, _animalBornSlowestTime);
    }

    void Update()
    {
        // _creatorContoroller.GetComponent<CreateController>().ManualUpdate();
        // _flowerCreator.ManualUpdate();

        // if(_frameCounterLeftSide >= _randNumLeftSide * 0.001f)
        // {
        //     _frameCounterLeftSide = 0;
        //     _randNumLeftSide = Random.Range(_animalBornFastestTime, _animalBornSlowestTime);
        //     _rebornLeftSide.Reborn();
        // }

        // if(_frameCounterRightSide >= _randNumRightSide * 0.001f)
        // {
        //     _frameCounterRightSide = 0;
        //     _randNumRightSide = Random.Range(_animalBornFastestTime, _animalBornSlowestTime);
        //     _rebornRightSide.Reborn();
        // }

        // _frameCounterLeftSide += Time.deltaTime;
        // _frameCounterRightSide += Time.deltaTime;
    }
}