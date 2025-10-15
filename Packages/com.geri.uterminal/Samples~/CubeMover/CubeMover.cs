/*
 * CubeMover Class
 * 
 * This script controls the movement and behavior of a cube in the Unity scene.
 * The cube randomly changes direction within a specified time interval and can take damage.
 *  
 */

using UnityEngine;
using UnityEngine.UI; 

namespace uTerminal.Samples
{ 
    public class CubeMover : MonoBehaviour
    {
        public string myName; // Name of the cube

        public int health = 100; // Health of the cube

        public override string ToString()
        {
            return myName; // Override ToString method to return the cube's name
        }

        public float moveSpeed = 2.5f; // Speed at which the cube moves
        public float timeBetweenDirectionChanges = 1.0f; // Time in seconds between direction changes

        private float timer; // Timer for tracking the time between direction changes
        private float randomX; // Random value for the X-axis movement
        private float randomZ; // Random value for the Z-axis movement

        public Slider healthSlider; // Reference to the health slider UI element

        void Start()
        {
            timer = timeBetweenDirectionChanges; // Initialize the timer for direction changes
        }

        void Update()
        {
            timer -= Time.deltaTime; // Update the timer

            if (timer <= 0f)
            {
                MoveCubeRandomly(); // Trigger random movement
                timer = timeBetweenDirectionChanges; // Reset the timer
            }

            // Move the cube towards the random position using Lerp for smooth movement
            Vector3 targetPosition = transform.position + new Vector3(randomX, 0f, randomZ);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }

        void MoveCubeRandomly()
        {
            // Set random values for X and Z axes to change the cube's direction
            randomX = Random.Range(-0.5f, 0.5f);
            randomZ = Random.Range(-0.5f, 0.5f);
        }

        [uCommand("damage", "Inflict damage on the player")]
        public void TakeDamage(int damage)
        {
            health -= damage; // Decrease health by the specified damage value

            if (health <= 0)
            {
                Die(); // If health drops to or below zero, call the Die method
            }

            healthSlider.value = health; // Update the health slider UI element
        }

        private void Die()
        {
            Destroy(gameObject); // Destroy the cube when it dies
        }  
    }
}