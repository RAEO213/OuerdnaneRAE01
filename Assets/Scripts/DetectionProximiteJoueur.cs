using UnityEngine;

public class DetectionProximiteJoueur : MonoBehaviour
{
    GameObject cible;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cible = FindAnyObjectByType<DeplacementJoueur>().gameObject;
        if (!cible)
        {
            Debug.LogWarning("Vous devez avoir un objet avec le script DeplacementJoueur dans la scène");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cible != null)
        {
            VerifierDistance();
        }
    }

    void VerifierDistance()
    {
        float distanceAvecJoueur = Vector2.Distance(cible.transform.position, this.transform.position);
        float distanceMinPourCollision = (this.transform.localScale.x / 2) + (cible.transform.localScale.x / 2);

        if (distanceAvecJoueur < distanceMinPourCollision)
        {
            cible.GetComponent<DeplacementJoueur>().ReplacerJoueur();
        }
    }
}
