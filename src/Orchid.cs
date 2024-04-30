using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orchid : MonoBehaviour
{
	public Transform questBubblePoint;

	public Transform questNotifPoint;

	public Transform portalPoint;

	public GameObject questAvailablePrefab;

	public GameObject questProgressPrefab;

	public GameObject dialoguePrefab;

	public GameObject questCompleteNotifPrefab;

	public GameObject questCompletePrefab;

	public GameObject portalPrefab;

	public GameObject cam;

	public GameObject player;

	public GameObject PlayerInteractionManager;

	public GameObject enemy;

	private int tutorialStep;

	private bool createQuestAvailableBubble;

	private bool createQuestProgressBubble;

	private bool createQuestCompleteBubble;

	private bool createDialogueBox;

	private bool questComplete;

	private bool checkFloat;

	private bool checkTp;

	private bool isTutorial = true;

	private bool spawnPortal;

	private TextMeshPro textLine;

	private void Start()
	{
		cam = GameObject.Find("Main Camera");
		player = GameObject.Find("Player");
		enemy = GameObject.Find("Enemy");
		PlayerInteractionManager = GameObject.Find("PlayerHitbox");
		textLine = questCompleteNotifPrefab.transform.GetChild(0).GetComponent<TextMeshPro>();
	}

	private void Update()
	{
		if (isTutorial && SceneManager.GetActiveScene().name.Equals("Tutorial"))
		{
			if (tutorialStep == 0 && !createQuestAvailableBubble)
			{
				createAvailableBubble();
			}
			if (tutorialStep == 1)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
					}
					if (!questComplete && player.GetComponent<PlayerMovement>().CheckFlashJump())
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
						createCompleteNotif("Flash Jump 1/1");
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 2)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 3)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
					}
					if (!questComplete && !checkFloat && player.GetComponent<PlayerMovement>().CheckFloat())
					{
						checkFloat = true;
						createCompleteNotif("Float 1/1");
					}
					if (!questComplete && !checkTp && player.GetComponent<PlayerMovement>().CheckTp())
					{
						checkTp = true;
						createCompleteNotif("Feather Float 1/1");
					}
					if (checkFloat && checkTp)
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 4)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 5)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					player.GetComponent<PlayerWeapon>().TutorialUnlockSkills(5);
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
					}
					if (!questComplete && player.GetComponent<PlayerWeapon>().GetTutCleave())
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
						createCompleteNotif("Cleave 1/1");
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 6)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 7)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					player.GetComponent<PlayerWeapon>().TutorialUnlockSkills(7);
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
					}
					if (!questComplete && player.GetComponent<PlayerWeapon>().GetTutRush())
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
						createCompleteNotif("Rush 1/1");
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 8)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 9)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					player.GetComponent<PlayerWeapon>().TutorialUnlockSkills(9);
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
					}
					if (!questComplete && player.GetComponent<PlayerWeapon>().GetTutPlummet())
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
						createCompleteNotif("Plummet 1/1");
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 10)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 11)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					player.GetComponent<PlayerWeapon>().TutorialUnlockSkills(11);
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
					}
					if (!questComplete && player.GetComponent<PlayerWeapon>().GetTutReign())
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
						createCompleteNotif("Reign of Destruction 1/1");
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 12)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 13)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					player.GetComponent<PlayerWeapon>().TutorialUnlockSkills(13);
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
					}
					if (!questComplete && player.GetComponent<PlayerWeapon>().GetTutRuin())
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
						createCompleteNotif("Ruin 1/1");
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 14)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 15)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					player.GetComponent<PlayerWeapon>().TutorialUnlockSkills(15);
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
					}
					if (!questComplete && player.GetComponent<PlayerWeapon>().GetTutInfinity())
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
						createCompleteNotif("Infinity 1/1");
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 16)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 17)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					player.GetComponent<PlayerWeapon>().TutorialUnlockSkills(17);
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
					}
					if (!questComplete && player.GetComponent<PlayerWeapon>().GetTutBind())
					{
						Debug.Log("hi");
						if (GameObject.FindWithTag("BindSummon") != null || GameObject.FindWithTag("BindEnd") != null)
						{
							questComplete = true;
							if (GameObject.FindWithTag("QuestProgress") != null)
							{
								Object.Destroy(GameObject.FindWithTag("QuestProgress"));
							}
							createCompleteNotif("Bind 1/1");
						}
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 18)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 19)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					player.GetComponent<PlayerWeapon>().TutorialUnlockSkills(19);
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
						enemy.GetComponent<Enemy>().ActivateAttackMode();
					}
					if (!questComplete && GameObject.FindWithTag("GuardShield") != null)
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
						enemy.GetComponent<Enemy>().ActivateAttackMode();
						createCompleteNotif("Guard 1/1");
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 20)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 21)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					player.GetComponent<PlayerWeapon>().TutorialUnlockSkills(21);
					if (!createQuestProgressBubble)
					{
						createProgressBubble();
					}
					if (!questComplete && player.GetComponent<PlayerWeapon>().GetTutBuff())
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
						createCompleteNotif("Recieve a blessing from the Goddess of Grandis 1/1");
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
			if (tutorialStep == 22)
			{
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null && !createQuestAvailableBubble)
				{
					createAvailableBubble();
				}
			}
			if (tutorialStep == 23)
			{
				if (GameObject.FindWithTag("QuestAvailable") != null)
				{
					Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
				}
				if (!createDialogueBox)
				{
					createDialogue();
				}
				if (GameObject.FindWithTag("DialogueBox") == null)
				{
					if (!createQuestProgressBubble)
					{
						PlayerInteractionManager.GetComponent<PlayerInteractionManager>().GetAttack(0.5f);
						createProgressBubble();
					}
					if (!questComplete && PlayerInteractionManager.GetComponent<PlayerInteractionManager>().GetHealth() == 500000)
					{
						questComplete = true;
						if (GameObject.FindWithTag("QuestProgress") != null)
						{
							Object.Destroy(GameObject.FindWithTag("QuestProgress"));
						}
						createCompleteNotif("Use Heal 1/1");
					}
					if (questComplete && !createQuestCompleteBubble)
					{
						createCompleteBubble();
					}
				}
			}
		}
		if (tutorialStep == 24)
		{
			if (!createDialogueBox)
			{
				createDialogue();
			}
			if (GameObject.FindWithTag("DialogueBox") == null && !spawnPortal)
			{
				spawnPortal = true;
				Vector3 position = portalPoint.position;
				position.z = -0.9f;
				Object.Instantiate(portalPrefab, position, portalPoint.rotation);
			}
		}
		if (SceneManager.GetActiveScene().name.Equals("End") && !createQuestAvailableBubble)
		{
			createAvailableBubble();
		}
	}

	public void GoNextStep()
	{
		tutorialStep++;
		createQuestAvailableBubble = false;
		createQuestProgressBubble = false;
		createQuestCompleteBubble = false;
		createDialogueBox = false;
		questComplete = false;
		if (GameObject.FindWithTag("QuestComplete") != null)
		{
			Object.Destroy(GameObject.FindWithTag("QuestComplete"));
		}
	}

	public int GetTutorialStep()
	{
		return tutorialStep;
	}

	public void End()
	{
		if (GameObject.FindWithTag("QuestAvailable") != null)
		{
			Object.Destroy(GameObject.FindWithTag("QuestAvailable"));
		}
		tutorialStep = 30;
		if (!createDialogueBox)
		{
			createDialogue();
		}
	}

	private void createAvailableBubble()
	{
		createQuestAvailableBubble = true;
		Vector3 position = questBubblePoint.position;
		Quaternion identity = Quaternion.identity;
		position.z = -2f;
		Object.Instantiate(questAvailablePrefab, position, identity);
	}

	private void createDialogue()
	{
		createDialogueBox = true;
		Vector3 position = cam.transform.position;
		position.y = cam.transform.position.y - 2.95f;
		position.z = -5f;
		Quaternion identity = Quaternion.identity;
		Object.Instantiate(dialoguePrefab, position, identity);
	}

	private void createProgressBubble()
	{
		createQuestProgressBubble = true;
		Vector3 position = questBubblePoint.position;
		Quaternion identity = Quaternion.identity;
		position.z = -2f;
		Object.Instantiate(questProgressPrefab, position, identity);
	}

	private void createCompleteNotif(string line)
	{
		Vector3 position = questNotifPoint.position;
		Quaternion identity = Quaternion.identity;
		position.z = -2f;
		textLine.SetText(line);
		Object.Instantiate(questCompleteNotifPrefab, position, identity);
	}

	private void createCompleteBubble()
	{
		createQuestCompleteBubble = true;
		Vector3 position = questBubblePoint.position;
		Quaternion identity = Quaternion.identity;
		position.z = -2f;
		Object.Instantiate(questCompletePrefab, position, identity);
	}
}
