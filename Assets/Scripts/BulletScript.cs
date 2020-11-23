using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public enum BulletEffect
    {
        [Tooltip("Full Metal Jacket")] FMJ,
        [Tooltip("Hollow Point")] HP,
        [Tooltip("High Velocity")] HV,
        [Tooltip("Incendiary")] INC
    }

    public List<BulletEffect> effects;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        Vector4 newCol = new Vector4(0,0,0,1);

        if (effects.Contains(BulletEffect.INC))
        {
            newCol.x += 1;
        }

        if (effects.Contains(BulletEffect.FMJ))
        {
            newCol.x += 1f;
            newCol.y += 1f;
        }


        float biggest = 0;

        for (int i = 0; i < 3; i++)
        {
            if (newCol[i] > biggest) biggest = newCol[i];
        }
        if (biggest >= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                newCol[i] *= 1 / biggest;
            }

            for (int i = 0; i < 4; i++)
            {
                newCol[i] = Mathf.Clamp(newCol[i], 0, 1);
            }
        }

        GetComponent<SpriteRenderer>().color = newCol;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
    }
}
