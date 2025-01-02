using Cinemachine;
using UnityEngine;

public class PlayerFx : EntityFx
{
    [Header("Screen shake FX")]
    [SerializeField] private float ShakeMultiplier;
    public Vector3 ShakeSwordImpact;
    public Vector3 ShakeHighDamage;
    private CinemachineImpulseSource ScreenShake;

    [Header("After image fx")]
    [SerializeField] private GameObject AfterImagePrefab;
    [SerializeField] private float ColorLooseRate;
    [SerializeField] private float AfterImageCooldown;
    private float AfterImageCooldownTimer;
    [Space]
    [SerializeField] private ParticleSystem DustFx;

    protected override void Start()
    {
        base.Start();
        ScreenShake = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        AfterImageCooldownTimer -= Time.deltaTime;
    }

    public void screenshake(Vector3 _shakepower)
    {
        ScreenShake.m_DefaultVelocity = new Vector3(_shakepower.x * player.FacingDir, _shakepower.y) * ShakeMultiplier;
        ScreenShake.GenerateImpulse();
    }


    public void createafterimage()
    {
        if (AfterImageCooldownTimer < 0)
        {
            AfterImageCooldownTimer = AfterImageCooldown;
            GameObject newAfterImage = Instantiate(AfterImagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImageFx>().setupafterimage(ColorLooseRate, Sr.sprite);
        }
    }


    public void playdustfx()
    {
        if (DustFx != null)
            DustFx.Play();
    }
}
