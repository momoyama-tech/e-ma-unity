using UnityEngine;

public class CreateController : MonoBehaviour
{
    void Start()
    {
        // _Creator.GetComponent<RandomButterflyCreator>().ManualStart();
        // _Creator.GetComponent<CreatureCreator>().ManualStart();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
            this.gameObject.GetComponent<RandomButterflyCreator>().ManualUpdate();
    }
}