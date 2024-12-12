using UnityEngine;
using DG.Tweening; // DoTween k�t�phanesini kullanmay� unutma

public class Arrow : MonoBehaviour
{
    private bool isStuck = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isStuck && collision.gameObject.CompareTag("Untagged"))
        {
            isStuck = true;

            // Hareketi durdur
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // �arpma noktas�nda okun pozisyonunu ayarla
            ContactPoint contact = collision.contacts[0];
            transform.position = contact.point;

            // Dinamik olarak okun y�n�n� ayarla
            Vector3 incomingDirection = rb.velocity.normalized; // Okun �arpma y�n�
            Vector3 normal = contact.normal;                   // �arpma y�zeyinin normali
            Quaternion targetRotation = Quaternion.LookRotation(-normal, Vector3.up);
            transform.rotation = targetRotation;

            // Titre�im efekti
            DoShakeEffect();
        }
    }

    private void DoShakeEffect()
    {
        // Titre�im hareketi (�rne�in pozisyon �zerinde k���k hareket)
        transform.DOShakePosition(0.2f, 0.1f, 10, 90, false, true)
            .OnComplete(() =>
            {
                // Efekt sonras� pozisyonu stabilize et (�arpma noktas�nda kalmas� i�in)
                transform.position = transform.position;
            });
    }
}
