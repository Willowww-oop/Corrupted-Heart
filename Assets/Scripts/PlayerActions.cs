using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject CharacterOne;
    [SerializeField] GameObject CharacterTwo;

    int characterVal;

    float jumpHeight;
    float speed;
    float customGrav;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterVal = 0;
        Instantiate<GameObject>(CharacterOne, transform.position, Quaternion.identity);
        CharacterOne.SetActive(true);   
    }

    // Update is called once per frame
    void Update()
    {
        SwapCharacter();

        if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.Translate(0, 0, 3);
        }

        
    }

    void SwapCharacter()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (characterVal)
            {
                case 0:
                    if (characterVal == 0)
                    {
                        CharacterTwo.SetActive(false);
                        CharacterOne.SetActive(true);
                        Instantiate<GameObject>(CharacterOne, transform.position, Quaternion.identity);

                        characterVal++;
                    }

                    break;

                case 1:
                    if (characterVal == 1)
                    {
                        CharacterOne.SetActive(false);
                        CharacterTwo.SetActive(true);
                        Instantiate<GameObject>(CharacterTwo, transform.position, Quaternion.identity);

                        characterVal--;
                    }

                    break;
            }

            //Console.WriteLine(characterVal);
        }
    }

}
