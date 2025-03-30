using UnityEngine;

public class KillAfterSeconds : MonoBehaviour
{
    public float seconds = 2.0f;
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > seconds)
            Destroy(gameObject);
    }
}
