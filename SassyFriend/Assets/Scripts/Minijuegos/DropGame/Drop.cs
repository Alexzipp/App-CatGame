using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movimiento de la gota - Si es limpia o sucia.
public class Drop : MonoBehaviour
{
    public bool isClean = true;
    public float fallSpeed = 3f;

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Destruir al salir de la pantalla
        if (transform.position.y < -6f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) //Si la gota cayó en el jugador esto dice si es limpia o sucia
    {
        if (other.CompareTag("Player")) // Tengo que acordarme de poner el tag aaaaaa
        {
            if (isClean)
                DropGameManager.Instance.CatchCleanDrop();
            else
                DropGameManager.Instance.CatchDirtyDrop();

            Destroy(gameObject);
        }
    }
}
