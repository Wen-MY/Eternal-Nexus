using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    Vector3 startPoint;
    Vector3 startPosition;
    public Text text;
    public static int wireCount = 0;
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
        wireCount++;
        Cursor.lockState = CursorLockMode.None; // 解锁鼠标光标
        Cursor.visible = true; // 设置鼠标光标可见
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                UpdateWire(collider.transform.position);

                if (transform.parent.name.Equals(collider.transform.parent.name))
                {
                    Main.Instance.SwitchChange(1);

                    collider.GetComponent<Wire>()?.Done();
                    Done();
                }
                return;
            }
        }

        UpdateWire(newPosition);
    }

    void Done()
    {
        // turn on light
        lightOn.SetActive(true);
        Destroy(this);
        wireCount--;
        if (wireCount <= 0)
        {
            text.enabled = true;
            SceneManager.LoadScene("Level2"); 
        }
    }

    private void OnMouseUp()
    {
        UpdateWire(startPosition);
    }

    void UpdateWire(Vector3 newPosition)
    {
        transform.position = newPosition;

        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);

    }
 }