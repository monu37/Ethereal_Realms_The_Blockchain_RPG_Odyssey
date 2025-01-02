using UnityEngine;



[CreateAssetMenu(fileName = "Ice ANd Fire Effect", menuName = "Data/Item Effect/Ice And Fire")]
public class IceAndFireEffect : ItemEffect
{

    [SerializeField] GameObject IceAndFirePrefab;
    [SerializeField] float XVelocity;

    public override void executeeffect(Transform _RespawnPos)
    {
        Player player = PlayerManager.instance.player;

        bool isThirdAttack = player.PrimaryAttackState.ComboCounter == 2;

        if (isThirdAttack)
        {
            GameObject newiceandfire = Instantiate(IceAndFirePrefab, _RespawnPos.position, player.transform.rotation);


            newiceandfire.GetComponent<Rigidbody2D>().velocity = new Vector2(XVelocity * player.FacingDir, 0);


            Destroy(newiceandfire, 6);
        }

    }
}
