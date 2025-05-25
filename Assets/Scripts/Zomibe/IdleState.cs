using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    PersueTargetState persueTargetState;

    [SerializeField] bool hasTargetState;

    [Header("Decetion")]
    [SerializeField] float detectionRadius = 30f;//检测的球坐标半径
    [SerializeField] float minDecetionRadiusAngle = -180f;//僵尸最正面-180度
    [SerializeField] float maxDecetionRadiusAngle = 100f;//僵尸最左、右侧均为100
    [SerializeField] LayerMask detectionLayer;
    [SerializeField] LayerMask detectionEnvLayer;


    private void Awake()
    {
        persueTargetState = GetComponent<PersueTargetState>();
    }

    public override State StateTick(ZombieManager zombieManager)
    {
        if (zombieManager.currentTarget != null)
        {
            
            return persueTargetState;
        }
        else
        {
            FindATargetViaLineOfSight(zombieManager);
            return this;
        }

    }

    private void FindATargetViaLineOfSight(ZombieManager zombieManager)
    {
        //搜集所有的半球内的碰撞物(玩家)
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerManager player = colliders[i].transform.GetComponent<PlayerManager>();

            //追寻第list里的第一个玩家
            if (player != null)
            {

                Debug.Log("僵尸发现玩家");
                Vector2 targetDirection = transform.position - player.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);//玩家与僵尸的正向夹角

                // Debug.Log("ViewableAngle = " + viewableAngle + (viewableAngle >= minDecetionRadiusAngle && viewableAngle <= maxDecetionRadiusAngle));

                if (viewableAngle > minDecetionRadiusAngle && viewableAngle < maxDecetionRadiusAngle)
                {
                    Debug.Log("通过僵尸范围检测");
                    RaycastHit hit;
                    float characterHight = 2f;
                    Vector3 playerStartPoint = new Vector3(player.transform.position.x, characterHight, player.transform.position.z);
                    Vector3 zombieStartPoint = new Vector3(transform.position.x, characterHight, transform.position.z);

                    //射线检测，检测是否僵尸和玩家之间有的直线有障碍物
                    if (Physics.Linecast(playerStartPoint, zombieStartPoint, out hit, detectionEnvLayer))
                    {
                        //有碰撞物，不能追逐目标;
                        Debug.Log("有碰撞物，不能追逐玩家 ==" + hit.transform.name);
                    }
                    else
                    {
                        Debug.Log("可以追逐玩家");

                        zombieManager.currentTarget = player;
                    }
                }
                // break;
            }
        }


    }
}
