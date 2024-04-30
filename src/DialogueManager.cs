using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	public GameObject textPrefab;

	private TextMeshPro textLine;

	private GameObject orchid;

	private Queue<string> sentences;

	private int tutorialStep;

	private bool isDoneTyping;

	private bool isStep1;

	private bool isStep2;

	private bool isStep3;

	private bool isStep4;

	private bool isStep5;

	private bool isStep6;

	private bool isStep7;

	private bool isStep8;

	private bool isStep9;

	private bool isStep10;

	private bool isStep11;

	private bool isStep12;

	private bool isStep13;

	private bool isStep14;

	private bool isStep15;

	private bool isStep16;

	private bool isStep17;

	private bool isStep18;

	private bool isStep19;

	private bool isStep20;

	private bool isStep21;

	private bool isStep22;

	private bool isStep23;

	private bool isStep24;

	private bool isStep30;

	private void Start()
	{
		orchid = GameObject.FindWithTag("Orchid");
		sentences = new Queue<string>();
		textLine = textPrefab.GetComponent<TextMeshPro>();
	}

	private void Update()
	{
		tutorialStep = orchid.GetComponent<Orchid>().GetTutorialStep();
		QuestSentences();
		if (Input.GetKeyDown(KeyCode.Space) && !isDoneTyping)
		{
			DisplayNextSentence();
		}
	}

	private IEnumerator TypeSentence(string line)
	{
		string sumLine = "";
		isDoneTyping = true;
		char[] array = line.ToCharArray();
		foreach (char c in array)
		{
			sumLine += c;
			textLine.SetText(sumLine);
			yield return new WaitForSeconds(1E-05f);
		}
		isDoneTyping = false;
	}

	private void QuestSentences()
	{
		if (tutorialStep == 1 && !isStep1)
		{
			isStep1 = true;
			StartDialogue(new string[10] { "Hello! You finally made it! The evil scientist Gelimer has brainwashed my brother, Lotus, and plans on using him to take over the world on his ship, the Black Heaven!\n\nPress the spacebar to continue dialogue.", "I need you to help me free Lotus of Gelimer's control.", "However, my brother is a very powerful individual and will be difficult to defeat.", "Luckily, I know how my brother fights and have created a simulator here for you to practice in before facing him for real.", "But first, let me brief you on who you are.", "You, as a master bladecaster, can create a wide variety of swords to aid you in combat.", "Before we get into your skillset, let me remind you of your movement.", "You use the arrow keys to move around and pressing the Left Alt button to jump.", "You can also flash jump by pressing the Left Alt button again while in the air. Holding the up-key and pressing Left Alt will allow you to flash jump upwards.", "Go ahead and try flash jumping now." });
		}
		if (tutorialStep == 2 && !isStep2)
		{
			isStep2 = true;
			StartDialogue(new string[1] { "Good job! Talk to me again to learn more." });
		}
		if (tutorialStep == 3 && !isStep3)
		{
			isStep3 = true;
			StartDialogue(new string[6] { "Nice!", "There are a couple more movement skills I would like to go over.", "While you are in the air, if you press the D-key, you will be able to float in the air for 2 seconds. If you press the D-key again while you are still floating, it will cancel the float.", "Also, by pressing the F-key, you will perform a feather float, which will teleport you backwards a set distance.", "This is a true teleport, meaning you will phase through objects which could be useful in battle. However, it comes with an 8-second cooldown.", "Try performing both of these skills now." });
		}
		if (tutorialStep == 4 && !isStep4)
		{
			isStep4 = true;
			StartDialogue(new string[1] { "Great! I see you've got the general movement skills down. Talk to me again to learn about your attack skills." });
		}
		if (tutorialStep == 5 && !isStep5)
		{
			isStep5 = true;
			StartDialogue(new string[3] { "As I said before, you are a master bladecaster and all your attack skills use the aether in the area to create and summon swords.", "Your main attack skill can be used by pressing the Left Control key. This skill summons a giant sword and cleaves whatever is in front of you.", "Use this skill now. You can also test all your skills on the dummy bot we have built in the center of the room." });
		}
		if (tutorialStep == 6 && !isStep6)
		{
			isStep6 = true;
			StartDialogue(new string[1] { "Cool, right? Talk to me again to learn your next skill." });
		}
		if (tutorialStep == 7 && !isStep7)
		{
			isStep7 = true;
			StartDialogue(new string[3] { "Your next skill is your rush skill. This skill lunges a sword forward, dealing damage to any enemy in your path and allowing you to dash with it. It can also be used in the air. However, it does have a 5-second cooldown.", "Be careful. This skill is NOT a teleport and you will hit objects that are in your path. Use it wisely.", "Go ahead and use this skill." });
		}
		if (tutorialStep == 8 && !isStep8)
		{
			isStep8 = true;
			StartDialogue(new string[1] { "Brilliant! There are still more skills to cover. Talk to me again." });
		}
		if (tutorialStep == 9 && !isStep9)
		{
			isStep9 = true;
			StartDialogue(new string[4] { "The next skill is your plummet skill.", "By pressing Left Shift while in the air, you will plummet down and deal immense damage to any enemy underneath you.", "This skill has a 1.5-second cooldown.", "Try executing this skill now." });
		}
		if (tutorialStep == 10 && !isStep10)
		{
			isStep10 = true;
			StartDialogue(new string[1] { "Nice! Time for your next skill. Talk to me again." });
		}
		if (tutorialStep == 11 && !isStep11)
		{
			isStep11 = true;
			StartDialogue(new string[5] { "This next skill is your summon skill, Reign of Destruction, and can be summoned by pressing the C-key.", "Using this skill will create a territory that contains a constant barrage of blades, dealing damage-over-time to any enemy in the area.", "The territory itself will last for 10-seconds, and the skill will have a cooldown of 30-seconds.", "Before the territory duration ends, the swords will reverse, dealing even more damage!", "Go ahead and cast Reign of Destruction now." });
		}
		if (tutorialStep == 12 && !isStep12)
		{
			isStep12 = true;
			StartDialogue(new string[1] { "Good job! We only have a few more skills to cover, but I would say these are your most important and strongest skills. Talk to me again." });
		}
		if (tutorialStep == 13 && !isStep13)
		{
			isStep13 = true;
			StartDialogue(new string[3] { "I would consider this next skill to be your \"mini-burst\" skill, since it comes with a 60-second cooldown.", "Let me introduce you to Ruin. By pressing the 1-key, you will summon the ancient sword of destruction to your location, dealing immense damage and destroying everything under it.", "Now go summon the sword of destruction." });
		}
		if (tutorialStep == 14 && !isStep14)
		{
			isStep14 = true;
			StartDialogue(new string[2] { "I have always wished I could use that skill.", "Your next skill is by far your strongest skill, so I need to tell you about it. Talk to me again." });
		}
		if (tutorialStep == 15 && !isStep15)
		{
			isStep15 = true;
			StartDialogue(new string[6] { "This next skill is your \"main burst\" skill and comes with a whole 3-minute cooldown. You will really need a break after casting this one.", "This skill is called Infinity and can be activated by pressing the 2-Key.", "When this skill is active, you gain absolute, full control over the aether in the area. The skill will seek out any enemy and mark them with a cursed blade, which will cause them to take an extra 15% damage from all sources of damage.", "Any enemy on your screen will also be taking a colossal amount of damage over time.", "Unfortunately, you are only able to maintain Infinity for 30-seconds.", "Go ahead. Take control of the aether and show me what you are made of." });
		}
		if (tutorialStep == 16 && !isStep16)
		{
			isStep16 = true;
			StartDialogue(new string[2] { "Absolutley astonishing.", "That just about does it for all your attack skills. However, there are 3 more mechanical skills that you need mastery of. Talk to me again." });
		}
		if (tutorialStep == 17 && !isStep17)
		{
			isStep17 = true;
			StartDialogue(new string[5] { "By pressing the W-key, you will use your bind skill, Blade Torrent.", "When you activate this skill, swords filled with magic will blossom over your enemy and bind their movements for 10-seconds.", "This skill will have a cooldown of 3-minutes as well. It's almost as if you're supposed to use it with Infinity.", "Be careful and don't miss your bind, otherwise you will have to wait for the whole duration of the cooldown to use it again. It has about the same range as your main attack skill.", "Go and bind the bot in the middle." });
		}
		if (tutorialStep == 18 && !isStep18)
		{
			isStep18 = true;
			StartDialogue(new string[1] { "We've talked a lot about your offense. Now let's talk about your defense. Talk to me again." });
		}
		if (tutorialStep == 19 && !isStep19)
		{
			isStep19 = true;
			StartDialogue(new string[5] { "By pressing the Q-key, you will activate Aether Guard, your invincibility frame.", "Holding down the Q-key will allow your Aether Guard to be active for up to 3-seconds, allowing you to negate all damage during that time.", "You can't move or use any other skills while Aether Guard is active. Therefore, you can release Aether Guard by releasing your Q-Key.", "This skill has a 25-second cooldown.", "I'm going to activate the bot's attack mode. Don't worry the lasers are harmless, but try guarding one of its attacks." });
		}
		if (tutorialStep == 20 && !isStep20)
		{
			isStep20 = true;
			StartDialogue(new string[1] { "Now that you know about your invincibility frame, there's only one more skill to inform you about. Your buff skill. Talk to me again." });
		}
		if (tutorialStep == 21 && !isStep21)
		{
			isStep21 = true;
			StartDialogue(new string[2] { "Your buff skill, activated by pressing the F1-key, allows you to recieve a powerful blessing from the Goddess of Grandis, increasing your damage by 15% for 60-seconds with a 90-second cooldown.", "Go and try this skill now." });
		}
		if (tutorialStep == 22 && !isStep22)
		{
			isStep22 = true;
			StartDialogue(new string[3] { "That's it! Those are all your skills!", "One more thing though... You need to be able to heal!", "Talk to me again to learn how to heal." });
		}
		if (tutorialStep == 23 && !isStep23)
		{
			isStep23 = true;
			StartDialogue(new string[4] { "To heal, you will press your A-key.", "Lucky for you, you are equipped with Powerful Elixir Potions which will heal 100% of your health, every time.", "I'm going to hit you real hard in just a sec, and after that, I want you to drink a potion.", "Ready?" });
		}
		if (tutorialStep == 24 && !isStep24)
		{
			isStep24 = true;
			StartDialogue(new string[5] { "Sorry for hitting you so hard, but at least you know how to heal yourself!", "If you ever forget where skills are binded to, you can press the \\-key to open up the bindings window, or look in the bottom right corner to also see the cooldowns on your skills.", "However, fair warning, in the battle with Lotus, there will be a 5-second cooldown on potion usage due to his aura.", "Other than that, you are now fully equipped to enter the simulator. You can do so by clicking the portal at the far right of this map.", "Or you can stay here and punch our bot for a little bit. Up to you." });
		}
		if (tutorialStep == 30 && !isStep30)
		{
			isStep30 = true;
			StartDialogue(new string[3] { "Congrats! You managed to clear!", "I forgot to tell you that the simulator wasn't completed yet so we could only simulate his containment vessel.", "If you want to fight again, you can click on the portal to the very right." });
		}
	}

	private void StartDialogue(string[] sentenceArray)
	{
		sentences.Clear();
		foreach (string item in sentenceArray)
		{
			sentences.Enqueue(item);
		}
		DisplayNextSentence();
	}

	private void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			Object.Destroy(GameObject.FindWithTag("DialogueBox"));
			return;
		}
		string line = sentences.Dequeue();
		StartCoroutine(TypeSentence(line));
	}
}
