using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimatorTest : MonoBehaviour
{

    [SerializeField]
    private Animator charAnimator;
    private float moveSpeed = 3.0f;
    private Vector3[] possePositions = { new Vector3(0, 1, 0), new Vector3(0.75f, 0.75f, 0),
        new Vector3(1, 0, 0), new Vector3(0.75f, -0.75f, 0), new Vector3(0, -1, 0),
        new Vector3(-0.75f, -0.75f, 0) , new Vector3(-1, 0, 0), new Vector3(-0.75f, 0.75f, 0) };

    public GameObject builderRef;
    public SpriteRenderer charKing;
    public Transform posseAnchor;

    private List<GameObject> posse = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (posse.Count > 0)
        {
            posseAnchor.Rotate(new Vector3(0, 0, 1), -4 * Time.deltaTime);
            foreach (GameObject go in posse)
            {
                go.transform.Rotate(new Vector3(0, 0, 1), 4 * Time.deltaTime);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            kill();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            charAnimator.SetBool("IsDead", false);
            charAnimator.Update(0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            charAnimator.SetTrigger("Attack");
        }

        Vector3 move_vector = new Vector3();
        bool isDead = charAnimator.GetBool("IsDead");

        if (Input.GetKey(KeyCode.A) && !isDead)
        {
            charKing.flipX = true;
            move_vector.x -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) && !isDead)
        {
            charKing.flipX = false;
            move_vector.x += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) && !isDead)
        {
            move_vector.y += moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) && !isDead)
        {
            move_vector.y -= moveSpeed * Time.deltaTime;
        }
        if (move_vector.magnitude > 0)
        {
            charAnimator.SetBool("isWalking", true);
            transform.position += move_vector;
        }
        else { charAnimator.SetBool("isWalking", false); }
    }

    void kill()
    {
        charAnimator.SetBool("IsDead", true);
        charAnimator.Update(0);
    }

    void buildToggle()
    {
        if(charAnimator.GetBool("isBuilding"))
        {
            moveSpeed *= 2;
            charAnimator.SetBool("isBuilding", false);
            List<GameObject> toRemove = new List<GameObject>();
            foreach(GameObject go in posse)
            {
                if(go.tag == "Builder")
                {
                    toRemove.Add(go);

                }
            }
            foreach(GameObject go in toRemove)
            {
                posse.Remove(go);
                Destroy(go);
            }
        }
        else
        {
            int count = posse.Count;
            for (int i = 0; i < 8 - count; i++)
            {
                growPosse(0, i);
            }
            moveSpeed /= 2;
            charAnimator.SetBool("isBuilding", true);
        }

    }

    void growPosse(int type, int offset)
    {
        var builder = Instantiate(builderRef, posseAnchor);
        var builderanimator = builder.GetComponent<Animator>();
        builderanimator.SetFloat("Offset", 0.1f * offset);
        posse.Add(builder);
        orientPosse();
    }

    void orientPosse()
    {
        int pos = 0;
        foreach(GameObject go in posse)
        {
            go.transform.localPosition = possePositions[pos];
            pos++;
        }
    }

}
