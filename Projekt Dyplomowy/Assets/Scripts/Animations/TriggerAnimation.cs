using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//sources
//https://stackoverflow.com/questions/40752083/how-to-find-child-of-a-gameobject-or-the-script-attached-to-child-gameobject-via

public class TriggerAnimation : MonoBehaviour
{
    PlayerDirectionDisplayHandler playerDirectionDisplayHandler;
    Animator animator;
    public static bool runAnimation = true;
    public static bool runAgain = true;
    public static bool startTransition = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //(AnswerHandler.index != int.Parse(gameObject.name))
        //podmienić to na dole :)
        // PlayerCanInteract.index
        if (1 != int.Parse(gameObject.name))
        {
            GameObject originalGameObject = GameObject.Find(gameObject.name);
            if (originalGameObject.GetComponent<Renderer>().enabled) originalGameObject.GetComponent<Renderer>().enabled = !originalGameObject.GetComponent<Renderer>().enabled;

            for (int i = 0; i < originalGameObject.transform.childCount; i++)
            {
                GameObject child = originalGameObject.transform.GetChild(i).gameObject; // dziecko
                //Debug.Log("Rodzic " + originalGameObject.name + " Dziecko " + child.name);
                child.SetActive(false);
            }

        }
        else
        {
            GameObject originalGameObject = GameObject.Find(gameObject.name);
            if (originalGameObject.GetComponent<Renderer>().enabled == false) originalGameObject.GetComponent<Renderer>().enabled = !originalGameObject.GetComponent<Renderer>().enabled;

            for (int i = 0; i < originalGameObject.transform.childCount; i++)
            {
                GameObject child = originalGameObject.transform.GetChild(i).gameObject; // dziecko
                //Debug.Log("Rodzic " + originalGameObject.name + " Dziecko " + child.name);
                child.SetActive(true);
            }
            StartCoroutine(Time());
        }
    }

    IEnumerator Time()
    {
        // index 1 
        yield return new WaitForSeconds(0f);

        if (gameObject.tag == "UseDoor")
        {
            StartCoroutine(DoorAnimations());
        }

        // tranzycja wejścia 
        if (SentenceHandler.hashTableAnswers[1] != null && startTransition == true)
        {
            Debug.Log("Start tranzycji");
            GameObject square = GameObject.Find("Square");
            Animator squareAnimator = square.GetComponent<Animator>();
            squareAnimator.SetBool("RunRight", true);
            startTransition = false;

            yield return new WaitForSeconds(2.5f);
            GameObject player = GameObject.Find("Player");

            playerDirectionDisplayHandler = GameObject.Find("Player").GetComponent<PlayerDirectionDisplayHandler>();
            playerDirectionDisplayHandler.HideAllPlayerPerspectives();
            player.transform.GetChild(0).gameObject.SetActive(true);
            GameObject playerFront = GameObject.Find("PlayerFront");
            Animator playerFrontAnimator = playerFront.GetComponent<Animator>();
            playerFrontAnimator.SetBool("is"+PlayerCanInteract.index.ToString() + "True",true);
            Debug.Log("Skończone");

        }

        // użycie tranzycji z textem if( po kliknięciu wyboru)
        // ładowanie animacji gracza i czekanie na tranzycje
        // właczenie animacji gracza if(mamy index i stan udzielonej odpowiedzi)
        // zmienna przejscie wszystkich animacji po to by załadować obiekt


        if (gameObject.tag == "PlayerCantMove") PlayerMovement.canMove = false;
        else PlayerMovement.canMove = true;
    }


    IEnumerator DoorAnimations()
    {
        if (runAnimation == true && runAgain == true)
        {
            runAgain = false;
            OpenDoor();
            yield return new WaitForSeconds(1f);
            animator.SetBool("isIndexCorrect", true);
            Debug.Log("Otwórz Drzwi");
        }
        if (runAnimation == false && runAgain == true)
        {
            runAgain = false;
            animator.SetBool("isIndexCorrect", false);
            yield return new WaitForSeconds(1f);
            CloseDoor();
            yield return new WaitForSeconds(4f); // czas na poczekanie na tranzycje
            startTransition = true;
            Debug.Log("Zamnknij Drzwi");
        }
    }

    void OpenDoor()
    {
        DoorHandler.doorStatus = 0;
    }
    void CloseDoor()
    {
        DoorHandler.doorStatus = 1;
    }

    private IEnumerator WaitForAnimation(Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
    }
}
