using UnityEngine;

public class Hard : MonoBehaviour
{
    bool y;

    private void Start()
    {
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            y = false;
        }
        else
        {
            y = true;
        }
    }
    void Update()
    {
        if (!y)
        {
            transform.localPosition += Vector3.up * Time.deltaTime * 0.25f;
            if (transform.localPosition.y >= 0.8f)
            {
                y = true;
            }
        }
        else
        {
            transform.localPosition -= Vector3.up * Time.deltaTime * 0.25f;
            if (transform.localPosition.y <= -0.4f)
            {
                y = false;
            }
        }
    }
}
