using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float LaserDistance = 100f;
    [SerializeField] private Transform LaserFirePoint;
    [SerializeField] private LineRenderer MyLineRenderer;
    [SerializeField] private LayerMask BlockLaserLayer;
    [SerializeField] private LayerMask PlayerLayer;

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, (LaserFirePoint.position - gameObject.transform.position).normalized, Mathf.Infinity, BlockLaserLayer + PlayerLayer);
        if (Hit.collider != null)
        {
            Draw2DRay(LaserFirePoint.position,Hit.point);
            if(Hit.collider.gameObject.tag == "Player")
            {
                Hit.collider.gameObject.GetComponent<PlayerCharacter>().Kill();
            }
        }
        else
        {
            Draw2DRay(LaserFirePoint.position, LaserFirePoint.position + (LaserFirePoint.position - gameObject.transform.position).normalized * LaserDistance);
        }

    }

    private void Draw2DRay(Vector2 StartPos, Vector2 EndPos)
    {
        MyLineRenderer.SetPosition(0, StartPos);
        MyLineRenderer.SetPosition(1, EndPos);
    }
}
