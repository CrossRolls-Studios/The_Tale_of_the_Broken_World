using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float currentMoveSpeed;
    public float diagonalMoveModifier;

    private Animator anim;
    private Rigidbody2D myRigitBody;

    private bool playerMoving;
    private Vector2 lastmove;

    private static bool playerExists;

    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    public string startPoint;

    public bool speaking;
    public bool dialog;
    private GameObject nearestNPC;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        myRigitBody = GetComponent<Rigidbody2D>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }



        
    }

    // Update is called once per frame
    void Update() {
        playerMoving = false;
        
        if (!speaking)
        {
            SetEdgesOnNPS();
            if (Input.GetButtonDown("Use"))
            {
                FindingNPC();
            }
            if (!attacking)
            {
                Moving();
                Attacking();
            }
            
        }
        else
        {
            myRigitBody.velocity = Vector2.zero;
            if (dialog)
            {
                if (Input.GetButtonDown("Use"))
                {
                    DialogWithNPC();
                }
            }
        }


        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }

        if(attackTimeCounter <= 0)
        {
            attacking = false;
            anim.SetBool("Attack", attacking);
        }

        ParametresToAnimator();
    }

    public Vector2 Lastmove
    {
        get { return lastmove; }
        set { lastmove = value; }
    }

    private void FindingNPC()
    {
        if (Vector3.Distance(nearestNPC.transform.position, transform.position) < 5f)
        {
            Debug.Log("Near the " + nearestNPC.name);
            speaking = true;
            nearestNPC.GetComponent<NPCController>().MeetNPC(); 
        }
    }

    public void StartDialogWithNPC()
    {
        if (nearestNPC.GetComponentInChildren<NPCQuestController>().readyToGiveQuest == true)
        {
            nearestNPC.GetComponentInChildren<NPCQuestController>().Dialog(0);
            dialog = true;
        }
        else if (nearestNPC.GetComponentInChildren<NPCQuestController>().questGiven == true)
        {
            nearestNPC.GetComponentInChildren<NPCQuestController>().QuestNotCompleted();
        }
    }

    public void DialogWithNPC()
    {
        NPCQuestController npc = nearestNPC.GetComponentInChildren<NPCQuestController>();
        if (npc.currentDialogPhrase < npc.questsToGive[npc.currentQuestNumber].GetComponent<Quest>().dialogBeforeQuest.Length - 1)
        {
            npc.currentDialogPhrase++;
            npc.Dialog(npc.currentDialogPhrase);

        }
        else if(npc.currentDialogPhrase == npc.questsToGive[npc.currentQuestNumber].GetComponent<Quest>().dialogBeforeQuest.Length - 1)
        {
            npc.TextAfterGivingQuest();
            gameObject.GetComponentInChildren<QuestManager>().GetNewQuest(npc.questsToGive[npc.currentQuestNumber]);
            npc.GivingQuest();
        }
    }

    private void SetEdgesOnNPS()
    {
        int minRange = 0;
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("NPC").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("NPC")[i].GetComponent<NPCController>().greenEdges.SetActive(false);
            if (Vector3.Distance(GameObject.FindGameObjectsWithTag("NPC")[i].transform.position, transform.position) < Vector3.Distance(GameObject.FindGameObjectsWithTag("NPC")[minRange].transform.position, transform.position))
            {
                minRange = i;
            }
        }
        nearestNPC = GameObject.FindGameObjectsWithTag("NPC")[minRange];
        if (Vector3.Distance(nearestNPC.transform.position, transform.position) < 5f)
        {
            nearestNPC.GetComponent<NPCController>().greenEdges.SetActive(true);
        }
    }

    private void ParametresToAnimator()
    {
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastmove.x);
        anim.SetFloat("LastMoveY", lastmove.y);
    }

    private void Moving()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
           
            myRigitBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, myRigitBody.velocity.y);
            playerMoving = true;
            lastmove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            
            myRigitBody.velocity = new Vector2(myRigitBody.velocity.x, Input.GetAxisRaw("Vertical") * currentMoveSpeed);
            playerMoving = true;
            lastmove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            myRigitBody.velocity = new Vector2(0f, myRigitBody.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            myRigitBody.velocity = new Vector2(myRigitBody.velocity.x, 0f);
        }
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            currentMoveSpeed = moveSpeed * diagonalMoveModifier;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }
    }

    private void Attacking()
    {
        if (Input.GetButtonDown("Attack"))
        {
            attackTimeCounter = attackTime;
            attacking = true;
            myRigitBody.velocity = Vector2.zero;
            anim.SetBool("Attack", attacking);
        }
    }


    public void StopSpeaking()
    {
        speaking = false;
        dialog = false;
        if (nearestNPC.GetComponentInChildren<NPCQuestController>() != null)
        {
            nearestNPC.GetComponentInChildren<NPCQuestController>().currentDialogPhrase = 0;
        }
        
    }
}
