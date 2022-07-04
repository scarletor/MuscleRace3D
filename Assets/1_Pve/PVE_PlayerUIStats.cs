using System;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using TMPro;
namespace Racing
{
    public class PVE_PlayerUIStats : MonoBehaviour
    {
        [Header("References")]
        public SpriteRenderer health;
        public SpriteRenderer weaponAmmo;
        public SpriteRenderer weaponFireRate;

        public TMP_Text playerName;
        public TMP_Text playerNameUI;

        [Header("Configurations")]
        public Color enemyColor;
        public Color weaponColor;

        private float originalHealthWidth;
        private float originalAmmoWidth;


        public float currentHP, maxHP;

        [Button]
        public void UpdateHp()
        {
            var healthRatio = (currentHP / maxHP);
            health.size = new Vector2(healthRatio * 2, 0.25f);

        }

        public GameObject camera;
        private void Update()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        [Button]
        public void Die()
        {

        }

    }
}
