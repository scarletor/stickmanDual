using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Lean.Gui;
using DragonBones;
using DG.Tweening;

public class PlayerMoveController : MonoBehaviour
{

    // PUBLIC
    public float speedMovements;
    public float HP;
    // PRIVATE
    private Rigidbody _rigidbody;
    [SerializeField] bool continuousRightController = true;

    public GameObject target;
    private void Start()
    {
        lastPos = transform.position;
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();


        arma.AddDBEventListener(EventObject.COMPLETE, PlayIdle);

        gameObject.active = false;
        gameObject.active = true;

        HP = 100;

        InvokeRepeating("test",1, 6);
       
    }

    void test()
    {
        //transform.DOLocalMoveX(transform.position.x + 4, .4f);
        //transform.DOLocalRotate(new Vector3(0, 0, -90), 1).SetEase(Ease.OutBack);
        transform.DOJump(new Vector3(7, -4, 1), 12, 1, 1).SetEase(Ease.InSine);

    }

    enum playerStateEnum
    {
        attack,
        getHit,
        die,
        skill1,
        skill2,
        skill3,
        victory



    }

    playerStateEnum playerState
    {
        get { return 0; }
        set
        {
            var dir = (target.transform.position.x - transform.position.x) > 0 ? 1 : -1;
            if (value == playerStateEnum.attack)
            {

            }
            if (value == playerStateEnum.getHit)
            {
                var animHit = "GetHit" + Random.Range(1, 4);
                arma.animation.FadeIn(animHit, 0.2f, 1, 1);
                transform.DOMoveX(transform.position.x - .5f * dir, .1f);
                myHpUI.value -= 5f;

                if (myHpUI.value <= 0)
                    playerState = playerStateEnum.die;

            }
            if (value == playerStateEnum.die)
            {
                arma.RemoveDBEventListener(EventObject.COMPLETE, PlayIdle);
                //transform.DOLocalMoveX(transform.position.x + 4, .4f);
                //transform.DOLocalRotate(new Vector3(0, 0, -90), 1).SetEase(Ease.OutBack);
                transform.DOJump(new Vector3(7, -4, 1), 12, 1, 1).SetEase(Ease.InSine);
                arma.animation.FadeIn("Die", 0.2f, 1, 1);

            }
            if (value == playerStateEnum.skill1)
            {

            }
            if (value == playerStateEnum.skill2)
            {

            }

            if (value == playerStateEnum.skill3)
            {

            }
            if (value == playerStateEnum.victory)
            {

            }


        }

    }






    public LeanJoystick joy;
    bool isFlyingIn = false;
    bool isFlyingOut = false;
    bool isIdle = false;
    public bool canMove = true;
    void CheckAnimationFly()
    {
        if (joy.ScaledValue.x != 0)
        {
            if (canMove == false) return;

            if ((target.transform.position.x - transform.position.x) >= 0 && joy.ScaledValue.x >= 0 && isFlyingIn == false)
            {
                isFlyingIn = true;
                isFlyingOut = false;
                isIdle = false;
                arma.animation.FadeIn("FlyIn", 0.2f, -1, 1);
                Debug.LogError("RUN 1");
            }
            if ((target.transform.position.x - transform.position.x) >= 0 && joy.ScaledValue.x < 0 && isFlyingOut == false)
            {
                isFlyingIn = false;
                isFlyingOut = true;
                isIdle = false;
                arma.animation.FadeIn("FlyOut", 0.2f, -1, 1);
                Debug.LogError("RUN 2");

            }


            if ((target.transform.position.x - transform.position.x) < 0 && joy.ScaledValue.x < 0 && isFlyingIn == false)
            {
                isFlyingIn = true;
                isFlyingOut = false;
                isIdle = false;
                arma.animation.FadeIn("FlyIn", 0.2f, -1, 1);
                Debug.LogError("RUN 1");
            }
            if ((target.transform.position.x - transform.position.x) < 0 && joy.ScaledValue.x >= 0 && isFlyingOut == false)
            {
                isFlyingIn = false;
                isFlyingOut = true;
                isIdle = false;
                arma.animation.FadeIn("FlyOut", 0.2f, -1, 1);
                Debug.LogError("RUN 2");

            }



        }

        else if (isIdle == false)
        {
            isIdle = true;
            isFlyingOut = false;
            isFlyingIn = false;

            arma.animation.FadeIn("Idle", 0.2f, 1, 1);
            Debug.LogError("RUN 3");

        }

    }






    Vector2 lastPos;
    void GetSpeed()
    {
        Vector2 curPos = transform.position;

        lastPos = curPos;

    }
    void CheckDirection()
    {
        if ((transform.position.x - target.transform.position.x) > 0)
            gameObject.transform.localScale = new Vector2(-1, 1);
        else
            gameObject.transform.localScale = new Vector2(1, 1);


    }

    void LookAtEnemy()
    {

    }


    public Text textX, textY, textSum;
    float offsetMoveX = 1.5f;
    float offsetMoveY = 1.5f;
    float distantce;
    void FixedUpdate()
    {

        CheckAnimationFly();
        MoveByJoyStick();
        CheckDirection();
        BeamControl();



        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Charge();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Skill1();
        }
    }

    public GameObject beamImpact;
    void BeamControl()
    {
        beamImpact.transform.position = target.transform.position + new Vector3(0, -2f, 0);
        beam.GetComponent<LineRenderer>().SetPosition(0, transform.position + new Vector3(1.5f, -2.7f, 0));
        beam.GetComponent<LineRenderer>().SetPosition(1, target.transform.position + new Vector3(0, -2f, 0));

        beam.GetComponent<LineRenderer>().sharedMaterial.mainTextureScale = new Vector2(1, 1);
        beam.GetComponent<LineRenderer>().sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * 8, 0);
    }



    void MoveByJoyStick()
    {
        if (gameObject.name.Contains("Enemy")) return;
        if (canMove == false) return;
        _rigidbody.MovePosition(transform.position + (transform.up * joy.ScaledValue.y * Time.deltaTime * speedMovements) +
            (transform.right * joy.ScaledValue.x * offsetMoveX * Time.deltaTime * speedMovements));

    }
    void PlayIdle(string type, EventObject eventObject)
    {
        arma.animation.FadeIn("Idle", 0.2f, 1, 1);
        chargeBeam.active = false;
        beam.active = false;
        chargeEff.active = false;
        beamImpact.active = false;
    }

    public UnityArmatureComponent arma;
    [SerializeField]
    int attackStep = 0;
    private IEnumerator coroutine;
    public void Attack()
    {
        attackBoxCol.SetActive(true);
        DOVirtual.DelayedCall(.1f, () =>
        {
            attackBoxCol.SetActive(false);

        });
        canMove = false;
        attackStep++;
        if (attackStep == 1)
            arma.animation.FadeIn("Atack1", 0.2f, 1, 1);
        if (attackStep == 2)
            arma.animation.FadeIn("Atack2", 0.1f, 1, 1);
        if (attackStep == 3)
            arma.animation.FadeIn("Atack3", 0.1f, 1, 1);

        if (attackStep >= 3)
            attackStep = 0;


        var dir = (target.transform.position.x - transform.position.x) > 0 ? 1 : -1;
        transform.DOMoveX(transform.position.x + 2f * dir, .1f);

        //DOVirtual.DelayedCall(.05f, () => {
        //    CreateTrail();

        //});

        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = ResetAttackStep();
        StartCoroutine(coroutine);
    }


    public GameObject image;
    float tem = 0;
    void CreateTrail()
    {
        tem = tem + 0.1f;
        foreach (UnityEngine.Transform trans in image.transform)
        {

            var newGo = Instantiate(trans);
            newGo.transform.position = trans.position;
            newGo.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f - tem);

        }

    }

    bool isCorouRun = false;
    IEnumerator ResetAttackStep()
    {
        //attackBoxCol.SetActive(false);
        yield return new WaitForSeconds(1);
        attackStep = 0;
        canMove = true;

    }
    public GameObject chargeEff;
    public void Charge()
    {
        arma.RemoveDBEventListener(EventObject.COMPLETE, PlayIdle);
        arma.animation.FadeIn("Charge", 0.2f, -1, 1);
        chargeEff.active = true;

    }
    public void ReleaseCharge()
    {
        arma.AddDBEventListener(EventObject.COMPLETE, PlayIdle);
        arma.animation.FadeIn("Idle", 0.2f, 1, 1);
        chargeEff.active = false;

    }




    public GameObject fireBall, standWind;
    public void Skill1()
    {
        arma.animation.FadeIn("Skill1", 0.2f, 1, 1);
        fireBall.active = true;
        standWind.active = true;



        fireBall.transform.parent = gameObject.transform;

        fireBall.transform.localPosition = new Vector3(0, -2.4f, 0);
        DOVirtual.DelayedCall(1.7f, () =>
        {
            fireBall.transform.parent = null;
            fireBall.transform.DOMove(target.transform.position + new Vector3(0, -10, 0), .3f);
            standWind.active = false;
        });

        DOVirtual.DelayedCall(4f, () =>
        {
            fireBall.active = false;
        });
    }

    //kameha
    public GameObject chargeBeam, beam;
    public void Skill2()
    {
        arma.animation.FadeIn("Skill2", 0.2f, 1, 1);
        chargeBeam.active = true;



        DOVirtual.DelayedCall(1.6f, () =>
        {
            beam.active = true;
            beamImpact.active = true;
        });


    }
    public void Skill3()
    {
        arma.animation.FadeIn("Skill3", 0.2f, 1, 1);
    }
    public void Defend()
    {
        arma.RemoveDBEventListener(EventObject.COMPLETE, PlayIdle);
        arma.animation.FadeIn("Defend", 0.2f, 1, 1);
    }

    public void ReleaseDefend()
    {
        arma.AddDBEventListener(EventObject.COMPLETE, PlayIdle);
        arma.animation.FadeIn("Idle", 0.2f, 1, 1);

    }






    [Space(30)]
    [TextArea]
    public string textArea1 = "attackHitBox";
    public GameObject attackBoxCol;


    public Slider myHpUI;
    public void GetHit()
    {
        playerState = playerStateEnum.getHit;
    }

    public RectTransform target1;
    public AnimationCurve animationCurve;

    private RectTransform rectTransform;
    private Vector3 start;
    private Vector3 end;

    private Coroutine coroutine1;

    private void Start1()
    {
        rectTransform = GetComponent<RectTransform>();
        start = rectTransform.position;
        end = target1.position + new Vector3(target1.rect.width / 2f, target1.rect.height / 2f, 0f);
    }

}
