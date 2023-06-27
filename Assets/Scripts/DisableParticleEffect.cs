using UnityEngine;

public class DisableParticleEffect : MonoBehaviour
{
    public GameObject particleEffectPrefab;

    private GameObject particleEffectInstance;
    private ParticleSystem particleSystem;

    private void Start()
    {
        // Instantiate the particle effect from the prefab
        particleEffectInstance = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);

        // Get the ParticleSystem component of the particle effect
        particleSystem = particleEffectInstance.GetComponent<ParticleSystem>();
    }

    public void DisableLoopingAndPrewarm()
    {
        var main = particleSystem.main;
        main.loop = false;
        main.prewarm = false;
    }
}
