using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject[] path;
    private AI ai_state = new AI();
    // Start is called before the first frame update
    void Start()
    {
        ai_state.CurrentState = AI.ai_states.patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        switch (ai_state.CurrentState) {
            case AI.ai_states.patrolling:
                patrol();
                break;
            case AI.ai_states.chasing:
                chase();
                break;
        }
    }

    private void patrol() {
        for (int i = 0; i < path.Length; i++) {
            GameObject obj = path[i];
            transform.position = Vector3.MoveTowards(transform.position, obj.transform.position, 0.5f * Time.deltaTime);
            if (transform.position != obj.transform.position)
                i--;
        }
    }

    private void chase() {

    }

}
