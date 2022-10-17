using UnityEngine;

public class paddleMovement : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public float mvtSpeed;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State != "pause")
        {
            if (Input.GetKey(up) && transform.position.y <= 4f)
                transform.position += new Vector3(0, mvtSpeed * Time.deltaTime, 0);
            else if (Input.GetKey(down) && transform.position.y >= -4f)
                transform.position -= new Vector3(0, mvtSpeed * Time.deltaTime, 0);
        }
    }
}
