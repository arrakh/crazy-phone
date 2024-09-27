using CrazyPhone.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CrazyPhone
{
    public class FormImageScript : MonoBehaviour
    {
        [SerializeField] private PhoneInput input;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Image formImage;

        [SerializeField] private AudioClip pageFlipClip;

        [Header("Forms")]
        [SerializeField] private Sprite form1;
        [SerializeField] private Sprite form2;
        [SerializeField] private Sprite form3;

        private List<Sprite> forms = new List<Sprite>();
        private int currentForm = 1;

        private void Awake() {
            forms.Add(form1);
            forms.Add(form2);
            forms.Add(form3);
        }

        // Update is called once per frame
        void Update()
        {
            if (input.GetKeyDown("#"))
            {
                FlipPage(/*nextPage=*/true);
            }

            if (input.GetKeyDown("*"))
            {
                FlipPage(/*nextPage=*/false);
            }
        }

        private void FlipPage(bool nextPage)
        {
            print("FLIP PAGE " + (nextPage ? "previous" : "next"));
            audioSource.PlayOneShot(pageFlipClip);

            print("current Form before: " + currentForm);

            if (nextPage)
            {
                currentForm++;
                if (currentForm == 4)
                {
                    currentForm = 1;
                }
            }
            else
            {
                currentForm--;
                if (currentForm == 0)
                {
                    currentForm = 3;
                }
            }

            print("current Form now: " + currentForm);

            formImage.sprite = forms[currentForm - 1];
        }

    }
}
