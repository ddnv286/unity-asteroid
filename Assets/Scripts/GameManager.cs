using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public float respawnInvulnerability = 3.0f;
    public int score = 0;

    public void AsteroidDestroyed (Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        // the smaller asteroid, the more point would be rewarded
        if (asteroid.size < 0.75f)
        {
            score += 100;
        } else if (asteroid.size < 1.25f)
        {
            score += 50;
        } else
        {
            score += 25;
        }
    }

    public void PlayerDied ()
    {
        // play the explosion when died
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;

        if (this.lives <= 0)
        {
            GameOver();
        } else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
        
    }

    private void Respawn ()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignoring Collision");
        this.player.gameObject.SetActive(true);
        // give the player 3s of invulnerability
        Invoke(nameof(TurnOnCollision), this.respawnInvulnerability);
    }

    private void TurnOnCollision ()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver ()
    {
        // nothing yet
        this.lives = 3;
        this.score = 0;
        Invoke(nameof(Respawn), this.respawnTime);
    }
}
