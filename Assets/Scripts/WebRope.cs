using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebRope : MonoBehaviour
{
    static WebRope _instance;
    public static WebRope Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WebRope>();
            }

            return _instance;
        }
    }

    LineRenderer webRenderer;
    public LayerMask webLayerMask;

    bool isAttached = false;

    float climbSpeed = 3f;
    float swingSpeed = 50f;

    Vector3 playerOffset;

    // Start is called before the first frame update
    void Start()
    {
        webRenderer = GetComponent<LineRenderer>();
        webRenderer.enabled = false;
    }

    void Update()
    {
        if (!isAttached)
        {
            playerOffset = Vector3.zero;
            return;
        }

        playerOffset = new Vector3(Input.GetAxisRaw("Horizontal") * swingSpeed * Time.deltaTime, 0f, 0f);

        if (Input.GetButtonDown("Jump"))
        {
            DestroyRope();
        }
    }

    void FixedUpdate()
    {
        if (!isAttached)
        {
            return;
        }

        Player.Instance.SetMove(false);

        Vector3 playerPos = Player.Instance.GetPosition();
        Player.Instance.transform.position = Vector3.Lerp(playerPos + playerOffset, webRenderer.GetPosition(1), climbSpeed * Time.fixedDeltaTime);
        webRenderer.SetPosition(0, playerPos + playerOffset);

        float distance = Vector2.Distance(playerPos, webRenderer.GetPosition(1));
        if (distance < 2f)
        {
            DestroyRope();
        }
    }

    public void CreateRope()
    {
        if (isAttached)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, webLayerMask);

        if (hit.collider == null)
        {
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RenderRope(Player.Instance.GetPosition(), mousePos);
    }

    public void RenderRope(Vector3 startPoint, Vector3 endPoint)
    {
        webRenderer.enabled = true;
        webRenderer.positionCount = 2;

        startPoint.z = -1f;
        endPoint.z = -1f;

        webRenderer.SetPosition(0, startPoint);
        webRenderer.SetPosition(1, endPoint);

        isAttached = true;
    }

    void DestroyRope()
    {
        isAttached = false;
        webRenderer.enabled = false;
        Player.Instance.SetMove(true);
    }
}
