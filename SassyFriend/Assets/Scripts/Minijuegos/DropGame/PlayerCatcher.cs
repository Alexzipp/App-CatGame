using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatcher : MonoBehaviour
{
    //Alch no sé como funciona lo del movimiento xd
    [Header("Config Movimiento")]
    public float moveSpeed = 15f;       // velocidad de interpolación
   
    [Header("Protección")]
    public float margin = 0.5f;         // marco de seguridad para que no salga de la pantalla

    private Camera cam;
    private float minX, maxX;

    void Start()
    {
        cam = Camera.main;

        // Calculamos los límites según la cámara y el margen
        float halfWidth = GetHalfWidth();
        minX = cam.ViewportToWorldPoint(Vector3.zero).x + halfWidth + margin;
        maxX = cam.ViewportToWorldPoint(Vector3.right).x - halfWidth - margin;
    }

    void Update()
    {
        Vector3 targetPos = transform.position;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // distancia de cámara
            targetPos = cam.ScreenToWorldPoint(mousePos);
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = touch.position;
            touchPos.z = 10f;
            targetPos = cam.ScreenToWorldPoint(touchPos);
        }
#endif

        // Fijamos solo el movimiento horizontal, respetando el margen
        float clampedX = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Movimiento suave
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    private float GetHalfWidth()
    {
        // si tienes un collider o sprite, tomamos su ancho
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) return sr.bounds.extents.x;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) return col.bounds.extents.x;

        return 0.5f; // fallback
    }
}
