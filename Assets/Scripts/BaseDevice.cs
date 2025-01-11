using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDevice : MonoBehaviour
{
    [SerializeField] private float radius = 1.5f;

    public virtual void Operate()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseUp()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        Vector3 playerPosition = player.position;
        playerPosition.y = transform.position.y;
        if (Vector3.Distance(playerPosition, transform.position) < radius)
        {
            Vector3 direction = transform.position - player.position;
            if (Vector3.Dot(player.forward, direction) > 0.5f)
            {
                Operate();
            }
        }
    }
}
