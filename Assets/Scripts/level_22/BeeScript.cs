using DG.Tweening;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeeScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtNumber;
    [SerializeField] private GameObject sprite;
    public float spriteWidth;
    public float spriteHeight;

    public float yOrigin =0;
    private float ySpriteOrigin;

    public int num;

    public static int ids = 0;
    public BeeType type = BeeType.NORMAL;

    bool isFlying = false;

    public BeeState state = BeeState.MOVING;

    [SerializeField] private int direction = 1;
    [SerializeField] private float speed = 2f;

    Tween RotateZ;
    Tween MoveY;


    private void Update()
    {
        switch (state)
        { 
            case BeeState.MOVING:              

                gameObject.transform.localPosition += (Vector3.right * direction) * Time.deltaTime * (speed * 100);
                if (!isFlying)
                {
                     RotateZ = sprite.transform.DOLocalRotate(new Vector3(sprite.transform.localRotation.x, sprite.transform.localRotation.y, sprite.transform.localRotation.z + 5), 0.7f)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.InOutSine);
                    RotateZ.intId = getNewID();

                    MoveY = sprite.transform.DOLocalMove(new Vector3(sprite.transform.localPosition.x, sprite.transform.localPosition.y + 80, sprite.transform.localPosition.z), 0.7f)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.InOutSine);
                    MoveY.intId = getNewID();
                    isFlying = true;
                }

                checkLimit();


                break;
        }

        
    }

    public void touch()
    {
        if (GameManagerScript.Instance.getState() == GameManagerScript.GameState.PLAYING && state == BeeState.MOVING)
        {
            SoundManager.instance.playOnce(SoundManager.SFXS.popSfx2);
            trapToHive();
        }
    }

    private void checkLimit()
    {
        if (direction == 1)
        {
            if (transform.localPosition.x - spriteWidth >= Screen.currentResolution.width /2)
            {
                transform.localPosition -= (Vector3.right * direction) * Time.deltaTime * (speed * 100);
                changeDirection();
            }
        }
        else if (direction == -1)
        {
            if (transform.localPosition.x + spriteWidth <= 0 - Screen.currentResolution.width/2)
            {
                gameObject.transform.localPosition -= (Vector3.right * direction) * Time.deltaTime * (speed * 100);
                changeDirection();
            }
        }
    }

    private void OnEnable()
    {
        spriteWidth = sprite.GetComponent<RectTransform>().rect.width;
        spriteHeight = sprite.GetComponent<RectTransform>().rect.height;
        ySpriteOrigin = sprite.transform.localPosition.y;
        
        state = BeeState.MOVING;
        
    }

    private void OnDisable()
    {
        state = BeeState.UNACTIVE;
    }

    public void stopAnimation() 
    { 
        if (isFlying)
        {
            DOTween.Kill(RotateZ.intId);
            DOTween.Kill(MoveY.intId);
            isFlying = false;

        }
    }


    public void Respawn(Vector3 newPosition, int newDirection, int n, bool extra)
    {
        transform.localScale = Vector3.one;
        if (newPosition.x == 0) {
            gameObject.transform.localPosition = new Vector3(newPosition.x - spriteWidth, newPosition.y, newPosition.z);
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z);
        }


        direction = newDirection;
        txtNumber.gameObject.transform.localScale = new Vector3(direction * -1, 1, 1);
        num = n;
        txtNumber.text = num.ToString();

        if (extra)
        {
            type = BeeType.DOUBLE;
            sprite.GetComponent<RawImage>().texture = BeeManager.instance.EXTRA_BEE;
        }
        else
        {
            type = BeeType.NORMAL;
            sprite.GetComponent<RawImage>().texture = BeeManager.instance.NORMAL_BEE;
        }
        
        sprite.transform.localScale = new Vector3(direction * -1, 1, 1);
        sprite.transform.localPosition = new Vector3(sprite.transform.localPosition.x,ySpriteOrigin,1);
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        stopAnimation();
        state = BeeState.DESPAWNING;
        gameObject.transform.DOScale(0, 1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });

    }

    public void trapToHive()
    {
        stopAnimation();
        state = BeeState.TRAPPED;
        gameObject.transform.DOScale(0,1f).SetEase(Ease.InOutSine);
        gameObject.transform.DOLocalMoveY(-(Screen.height/2 + spriteHeight), 1.2f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            gameObject.SetActive(false);
            BeeManager.instance.addBee();
        });

    }

    public void changeDirection()
    {
        direction *= -1;
        txtNumber.gameObject.transform.localScale = new Vector3(direction * -1, 1, 1);
        sprite.transform.localScale = new Vector3(direction*-1,1,1);
        num = ValuesGeneratorScripts.Instance.getRandomValue();
        txtNumber.text = num.ToString();
    }

    public enum BeeState
    {
        PAUSED,
        MOVING,
        TRAPPED,
        LAUNCHED,
        DESPAWNING,
        UNACTIVE
    }

    public enum BeeType
    {
        NORMAL,
        DOUBLE
    }

    public static int getNewID()
    {
        return ids++;
    }

}
