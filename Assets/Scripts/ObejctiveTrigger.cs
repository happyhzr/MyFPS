using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObejctiveTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Managers.Mission.ReachObjective();
    }
}
