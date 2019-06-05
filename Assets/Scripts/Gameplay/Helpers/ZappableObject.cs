using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Unset, Up, Down, Left, Right
}

public class ZappableObject : MonoBehaviour {

    public Direction direction = Direction.Unset;
    private EffectController effects;

    private void Start()
    {
        effects = FindObjectOfType<EffectController>();
    }

    public void InitialZap()
    {
        FindZappables();
    }

    public void Zap()
    {
        DamageParent();
        FindZappable();
    }

    private void FindZappable()
    {
        var spawnedObjects = FindObjectOfType<SpawnHelper>().spawnedObjectsParent;
        float distanceBetweenObjects = FindObjectOfType<Spawner>().distanceBetweenObjects;

        for (int i = 0; i < spawnedObjects.childCount; i++)
        {
            GameObject G = spawnedObjects.GetChild(i).gameObject;

            if (G.transform.position.y == transform.position.y)
            {
                if (direction == Direction.Left)
                {
                    if (((G.transform.position - transform.position).sqrMagnitude < distanceBetweenObjects * distanceBetweenObjects * 1.1f) && G.transform.position.x < transform.position.x)
                    {
                        if (G.GetComponent<ZappableObject>() != null)
                        {
                            G.GetComponent<ZappableObject>().direction = Direction.Left;
                            G.GetComponent<ZappableObject>().Zap();
                        }
                    }
                }
                if (direction == Direction.Right)
                {
                    if (((G.transform.position - transform.position).sqrMagnitude < distanceBetweenObjects * distanceBetweenObjects * 1.1f) && G.transform.position.x > transform.position.x)
                    {
                        if (G.GetComponent<ZappableObject>() != null)
                        {
                            G.GetComponent<ZappableObject>().direction = Direction.Right;
                            G.GetComponent<ZappableObject>().Zap();
                        }
                    }
                }
            }

            if (G.transform.position.x == transform.position.x)
            {
                if (direction == Direction.Up)
                {
                    if (((G.transform.position - transform.position).sqrMagnitude < distanceBetweenObjects * distanceBetweenObjects * 1.1f) && G.transform.position.y > transform.position.y)
                    {
                        if (G.GetComponent<ZappableObject>() != null)
                        {
                            G.GetComponent<ZappableObject>().direction = Direction.Up;
                            G.GetComponent<ZappableObject>().Zap();
                        }
                    }
                }
                if (direction == Direction.Down)
                {
                    if (((G.transform.position - transform.position).sqrMagnitude < distanceBetweenObjects * distanceBetweenObjects * 1.1f) && G.transform.position.y < transform.position.y)
                    {
                        if (G.GetComponent<ZappableObject>() != null)
                        {
                            G.GetComponent<ZappableObject>().direction = Direction.Down;
                            G.GetComponent<ZappableObject>().Zap();
                        }
                    }
                }
            }
        }
    }

    private void FindZappables()
    {
        effects.ZapEffect(gameObject.transform.position);
        var spawnedObjects = FindObjectOfType<SpawnHelper>().spawnedObjectsParent;
        float distanceBetweenObjects = FindObjectOfType<Spawner>().distanceBetweenObjects;

        for (int i = 0; i < spawnedObjects.childCount; i++)
        {
            GameObject G = spawnedObjects.GetChild(i).gameObject;

            if (G.transform.position.y == transform.position.y)
            {
                if ((G.transform.position - transform.position).sqrMagnitude < distanceBetweenObjects * distanceBetweenObjects * 1.1f && G.transform.position.x > transform.position.x)
                {
                    if (G.GetComponent<ZappableObject>() != null)
                    {
                        G.GetComponent<ZappableObject>().direction = Direction.Right;
                        G.GetComponent<ZappableObject>().Zap();
                    }
                }
                if ((G.transform.position - transform.position).sqrMagnitude < distanceBetweenObjects * distanceBetweenObjects * 1.1f && G.transform.position.x < transform.position.x)
                {
                    if (G.GetComponent<ZappableObject>() != null)
                    {
                        G.GetComponent<ZappableObject>().direction = Direction.Left;
                        G.GetComponent<ZappableObject>().Zap();
                    }
                }
            }
            if (G.transform.position.x == transform.position.x)
            {
                if ((G.transform.position - transform.position).sqrMagnitude < distanceBetweenObjects * distanceBetweenObjects * 1.1f && G.transform.position.y > transform.position.y)
                {
                    if (G.GetComponent<ZappableObject>() != null)
                    {
                        G.GetComponent<ZappableObject>().direction = Direction.Up;
                        G.GetComponent<ZappableObject>().Zap();
                    }
                }
                if ((G.transform.position - transform.position).sqrMagnitude < distanceBetweenObjects * distanceBetweenObjects * 1.1f && G.transform.position.y < transform.position.y)
                {
                    if (G.GetComponent<ZappableObject>() != null)
                    {
                        G.GetComponent<ZappableObject>().direction = Direction.Down;
                        G.GetComponent<ZappableObject>().Zap();
                    }
                }
            }
        }
    }

    private void DamageParent()
    {
        //sound
        effects.ZapEffect(gameObject.transform.position);

        if (GetComponent<Brick>() != null)
        {
            GetComponent<Brick>().TakeDamageFromZap();
        }
        else if (GetComponent<Ball>() != null)
        {
            GetComponent<Ball>().TakeDamageFromZap();
        }
        else if (GetComponent<GrowingBrick>() != null)
        {
            GetComponent<GrowingBrick>().TakeDamageFromZap();
        }
        else if (GetComponent<Hex>() != null)
        {
            GetComponent<Hex>().TakeDamageFromZap();
        }
        else if (GetComponent<Pentagram>() != null)
        {
            GetComponent<Pentagram>().hitsRemaining--;
        }
        else if (GetComponent<Pyramid>() != null)
        {
            GetComponent<Pyramid>().TakeDamageFromZap();
        }
        else if (GetComponent<Rhombus>() != null)
        {
            GetComponent<Rhombus>().TakeDamageFromZap();
        }
        else if (GetComponent<RotatingPyramid>() != null)
        {
            GetComponent<RotatingPyramid>().TakeDamageFromZap();
        }
    }
}