using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class MainMenuShip : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    public Transform circle;
    bool looking = false;
    bool turned = false;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        Vector3 rotation = new Vector3(0, Random.Range(-180f, 180f), 0);
        circle.Rotate(rotation);
        agent.destination = target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (looking && turned == false && agent.remainingDistance <= 0.01f)
        {
            agent.angularSpeed = 1000;
            agent.speed = 2;
            var position = transform.position;
            if (transform.position.x >= 0)
            {
                position.x -= 2.5f;
            }
            else
            {
                position.x += 2.5f;
            }
            agent.SetDestination(position);
            turned = true;
        }

        if (looking == false && agent.remainingDistance <= 0.1 && timer <= 0)
        {
            Vector3 rotation = new Vector3(0, Random.Range(-180f, 180f), 0);
            circle.Rotate(rotation);
            transform.position = target.position;
            rotation = new Vector3(0, Random.Range(-180f, 180f), 0);
            circle.Rotate(rotation);
            agent.destination = target.position;
        }
    }

    public void LookAtButton(Transform buttonPos)
    {
        var position = Camera.main.ScreenToWorldPoint(buttonPos.position);
        if (transform.position.x >= 0)
        {
            int map = Mathf.RoundToInt((buttonPos.gameObject.GetComponent<RectTransform>().sizeDelta.x) / 47.2971f) + 6;
            position.x += map / 2;
        }
        else
        {
            int map = Mathf.RoundToInt((buttonPos.gameObject.GetComponent<RectTransform>().sizeDelta.x) / 47.2971f) + 6;
            position.x -= map / 2;
        }
        agent.SetDestination(position);
        looking = true;
    }

    public void StopLookingAtButton()
    {
        agent.angularSpeed = 180;
        agent.speed = 3.5f;
        Vector3 rotation = new Vector3(0, Random.Range(-180f, 180f), 0);
        circle.Rotate(rotation);
        agent.destination = target.position;
        looking = false;
        turned = false;
        timer = 3f;
    }
}
