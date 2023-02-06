using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FighterController : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField]
    float attack;

    [SerializeField]
    float defense;

    [SerializeField]
    float speed;

    [SerializeField]
    float maxHealth;

    [Header("UI")]
    [SerializeField]
    TextMeshProUGUI healthText;

    [SerializeField]
    Image avatar;

    [SerializeField]
    Button attackButton;

    [SerializeField]
    Button healButton;

    [SerializeField]
    Button specialButton;

    [Header("Battle")]
    [SerializeField]
    FighterController opponent;

    [SerializeField]
    GameObject actionsContainer;

    [SerializeField]
    float regularEffectiveness = 2.25F;

    [SerializeField]
    float specialEffectiveness = 4.25F;

    [SerializeField]
    float healEffectiveness = 5.25F;

    float attackCount;
    float currentHealth;

    private void Start()
    {
        currentHealth= maxHealth;

        specialButton.interactable = false;
        healButton.interactable = false;
    }

    public void Attack()
    {
        attackCount++;
        if (attackCount >= 3)
        { 
            specialButton.interactable = true; 
        }
        opponent.TakeDamage(AttackTypes.REGULAR);
        actionsContainer.SetActive(false);
    }


    public void Special()
    {
        opponent.TakeDamage(AttackTypes.SPECIAL);
        actionsContainer.SetActive(false);

        attackCount = 0;
        specialButton.interactable = false;
    }

    public void Heal()
    {
        float heal = (defense / speed) * Random.Range(healEffectiveness / 2, healEffectiveness);

        currentHealth += Mathf.Abs(heal);
        currentHealth = Mathf.Floor(currentHealth);
        SetHealth();

        healButton.interactable = false;
        actionsContainer.SetActive(false);

        opponent.TakeControl();

    }

    public void TakeControl()
    {
        actionsContainer.SetActive(true);
    }

    public void TakeDamage(AttackTypes attackType)
    {
        float damage = (opponent.GetAttack() * opponent.GetSpeed()) / (defense * speed);


        if (attackType == AttackTypes.REGULAR)
        {
            damage *= Random.Range(1.0F, regularEffectiveness);
        }
        /*Special Attack incrementa el resultado del ataque 1.25 y 1.75 */
        else if (attackType == AttackTypes.SPECIAL)
        {
            damage *= Random.Range(regularEffectiveness, specialEffectiveness);
        }
        
        currentHealth -= Mathf.Abs(damage);
        currentHealth = Mathf.Floor(currentHealth);
        SetHealth();
        GameOver();

        healButton.interactable = true;
        actionsContainer.SetActive(true);
    }

    void SetHealth()
    {
        if (currentHealth < 0.0F)
        {
            currentHealth = 0.0F;
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthText.text = currentHealth.ToString();
    }

    public float GetAttack()
    {
        return attack;
    }

    public float GetSpeed()
    { 
        return speed; 
    }

    public void GameOver()
    {
        if (currentHealth == 0)
            SceneManager.LoadScene(2);
    }

}
