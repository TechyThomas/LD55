using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Update is called once per frame
    public float moveSpeed = 3f;
    Vector3 targetCameraPos;

    bool isMoving = false;

    void Start()
    {
        targetCameraPos = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetCameraPos) <= 0.5f)
        {
            isMoving = false;
        }

        if (isMoving)
        {
            return;
        }

        Vector3 playerPos = Player.instance.GetPosition();

        Vector3 screenToWorldTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f));
        Vector3 screenToWorldBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f));

        float cameraWidthOffset = screenToWorldTopRight.x - screenToWorldBottomLeft.x;
        float cameraHeightOffset = screenToWorldBottomLeft.y - screenToWorldTopRight.y;

        Debug.Log(playerPos.x + ", " + screenToWorldBottomLeft.x + ", " + screenToWorldTopRight.x);

        if (playerPos.x >= screenToWorldTopRight.x)
        {
            targetCameraPos.x += cameraWidthOffset;
            isMoving = true;
        }
        else if (playerPos.x < screenToWorldBottomLeft.x)
        {
            targetCameraPos.x -= cameraWidthOffset;
            isMoving = true;
        }

        // if (playerPos.y >= screenToWorldTopRight.y)
        // {
        //     cameraPos.y += cameraHeightOffset;
        // }
        // else if (playerPos.y <= screenToWorldBottomLeft.y)
        // {
        //     cameraPos.y -= cameraHeightOffset;
        // }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetCameraPos, moveSpeed * Time.fixedDeltaTime);
        // transform.position = Vector3.MoveTowards(transform.position, targetCameraPos, moveSpeed * Time.fixedDeltaTime);
    }
}
