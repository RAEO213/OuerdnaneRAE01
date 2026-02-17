// using System;
using UnityEngine;
using UnityEngine.InputSystem; // Cette ligne permet de détecter les touches du clavier

public class DeplacementJoueur : MonoBehaviour
{
    [Header("Déplacement du joueur")]
    public Vector2 positionInitialeJoueur;
    public float vitesse = 5;
    public float vitesseRotation = 180;

    [Header("Limites de la zone d'arrivée")]
    public float positionZoneX = 8f;
    public float positionZoneMinY = -3f;
    public float positionZoneMaxY = 3f;
    GameObject zoneArrivee;

    void Start()
    {
        positionInitialeJoueur = transform.position;

        //On cherche l'objet sur la scène
        zoneArrivee = FindAnyObjectByType<ZoneArrivee>().gameObject;

        if (!zoneArrivee)
        {
            Debug.LogWarning("Vous devez avoir un objet avec le script ZoneArrivee dans la scène");
        }
        else
        {
            ReplacerZone();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Variables temporaires pour gérer le déplacement et la rotation du joueur
        float deplacement = 0;
        float rotation = 0;

        // Gestion des touches pour la rotation du joueur
        if (Keyboard.current.aKey.isPressed)
        {
            rotation = vitesseRotation * Time.deltaTime;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            rotation = vitesseRotation * Time.deltaTime * -1;
        }

        // Gestion des touches pour le déplacement du joueur
        if (Keyboard.current.wKey.isPressed)
        {
            deplacement = vitesse * Time.deltaTime;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            deplacement = vitesse * Time.deltaTime * -1; //Permet d'aller par en arrière en multipliant par -1
        }

        //On applique la rotation et le déplacement
        transform.Rotate(0, 0, rotation);
        transform.Translate(deplacement * Vector2.up); //Vector2.up correspond à la flèche verte du joueur

        //On bloque la position pour éviter de sortir de l'écran
        //Mathf.Clamp limite une valeur entre 2 points
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8, 8), Mathf.Clamp(transform.position.y, -4, 4));


        //Vérification de la collision avec la zone d'arrivée
        float distanceAvecZone = Vector2.Distance(zoneArrivee.transform.position, this.transform.position);
        float distanceMinPourCollision = (this.transform.localScale.x / 2) + (zoneArrivee.transform.localScale.x / 2);

        //Si la distance entre la zone d'arrivée et le joueur est plus petite que la largeur du joueur + la largeur de la zone (la moitié car le point de pivot est au centre du sprite)
        if (distanceAvecZone < distanceMinPourCollision)
        {
            ReplacerZone();
            ReplacerJoueur();
        }
    }

    public void ReplacerZone()
    {
        //On replace le joueur à sa position d'origine et on place la zone arrivee de manière aléatoire en Y
        zoneArrivee.transform.position = new Vector2(positionZoneX, Random.Range(positionZoneMinY, positionZoneMaxY));
    }

    // Fonction qui sert à replacer le joueur.
    // Est utilisée aussi dans le script DetectionProximiteJoueur
    public void ReplacerJoueur()
    {
        transform.eulerAngles = new Vector3(0, 0, -90);
        transform.position = positionInitialeJoueur;
    }
}
