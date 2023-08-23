using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{

    public string LevelName;
    public GameObject playerPref;
    [HideInInspector] public GameObject Player;
    public GameObject BoxSlider;
    public GameObject Teleporter;
    public GameObject WayPoint;

    public GameObject Enemy;

    [HideInInspector] public GameObject boxSlider;

    public GameObject Hurdle;
    public GameObject VerticalHurdle;
    public GameObject BoxHurdle;

    public GridManager gridManager;


    private bool isGameOver = false;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;


    public List<Vector2Int> Enemies;
    public List<Vector2Int> VerticalHurdlesList;
    public List<Vector2Int> HorizontalHurdlesList;
    public List<Vector2Int> BoxHurdlesList;


    public Vector2Int PlayerPosition;
    public List<Vector2Int> BoxSliderPosition;
    public List<Vector2Int> TeleporterPosition;
    public Vector2Int WayPointPosition;
    public int x = 0;

    public float moveSpeed;

    private Tween PlayerMovement;

    public bool isMoving;

    public bool isLastLevel;

    void Start()
    {
        isMoving = false;

        SetPlayerPosition(PlayerPosition);

        if (HorizontalHurdlesList.Count > 0)
        {
            foreach (var Pos in HorizontalHurdlesList)
            {
                SetHorizontalHurdlesPos(Pos);
            }
        }

        if (VerticalHurdlesList.Count > 0)
        {
            foreach (var Pos in VerticalHurdlesList)
            {
                SetVerticalHurdlesPos(Pos);
            }
        }


        if (BoxSliderPosition.Count > 0)
        {
            foreach (var Pos in BoxSliderPosition)
            {
                SetBoxSliderPosition(Pos);
            }
        }


        if (TeleporterPosition.Count > 0)
        {
            foreach (var Pos in TeleporterPosition)
            {
                SetTeleporterPosition(Pos);
            }
        }

        if (Enemies.Count > 0)
        {
            foreach (var Pos in Enemies)
            {
                SetEnemyPosition(Pos);
            }
        }


        if (BoxHurdlesList.Count > 0)
        {
            foreach (var Pos in BoxHurdlesList)
            {
                SetBoxHurdlePosition(Pos);
            }
        }

        SetWayPointPosition(WayPointPosition);

    }


    public void SetPlayerPosition(Vector2Int playerPos)
    {
        Player = Instantiate(playerPref, gridManager.gameGrid[playerPos.x, playerPos.y].transform.position,
            quaternion.identity);
        Player.transform.parent = gridManager.gameGrid[playerPos.x, playerPos.y].transform;


        Debug.Log("Player Pos: " + gridManager
            .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position);


        PlayerMovement = Player.transform.DOMove(gridManager
            .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.normalized, 0).OnComplete(()=> {


                    Player.transform.position = gridManager
                        .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                            Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position;


                });






        gridManager.gameGrid[playerPos.x, playerPos.y].GetComponent<GridCell>().SetGameObject(Player);

        Player.transform.localScale = gridManager.gameGrid[playerPos.x, playerPos.y].transform.localScale;
    }

    void SetBoxSliderPosition(Vector2Int boxPos)
    {
        boxSlider = Instantiate(BoxSlider, gridManager.gameGrid[boxPos.x, boxPos.y].transform.position,
            quaternion.identity);
        gridManager.gameGrid[boxPos.x, boxPos.y].GetComponent<GridCell>().SetGameObject(boxSlider);
        boxSlider.transform.parent = gridManager.gameGrid[boxPos.x, boxPos.y].transform;
        boxSlider.transform.position = gridManager.gameGrid[boxPos.x, boxPos.y].transform.position;
        boxSlider.transform.localScale = gridManager.gameGrid[boxPos.x, boxPos.y].transform.localScale;

        for (int i = 0; i < 3; i++)
        {
            gridManager.gameGrid[boxPos.x - i, boxPos.y].GetComponent<SpriteRenderer>().color = new Color(89, 89, 89, 0.1f);
        }
        
    }

    void SetBoxHurdlePosition(Vector2Int boxPos)
    {
        var boxHurdle = Instantiate(BoxHurdle, gridManager.gameGrid[boxPos.x, boxPos.y].transform.position,
            quaternion.identity);
        gridManager.gameGrid[boxPos.x, boxPos.y].GetComponent<GridCell>().SetGameObject(boxHurdle);
        boxHurdle.transform.parent = gridManager.gameGrid[boxPos.x, boxPos.y].transform;
        boxHurdle.transform.position = gridManager.gameGrid[boxPos.x, boxPos.y].transform.position;
        boxHurdle.transform.localScale = gridManager.gameGrid[boxPos.x, boxPos.y].transform.localScale;
    }

    void SetTeleporterPosition(Vector2Int boxPos)
    {
        var boxSlider = Instantiate(Teleporter, gridManager.gameGrid[boxPos.x, boxPos.y].transform.position,
            quaternion.identity);
        gridManager.gameGrid[boxPos.x, boxPos.y].GetComponent<GridCell>().SetGameObject(boxSlider);
        boxSlider.transform.parent = gridManager.gameGrid[boxPos.x, boxPos.y].transform;
        boxSlider.transform.position = gridManager.gameGrid[boxPos.x, boxPos.y].transform.position;
        boxSlider.transform.localScale = gridManager.gameGrid[boxPos.x, boxPos.y].transform.localScale;
    }

    void SetHorizontalHurdlesPos(Vector2Int Pos)
    {
        var hurdle = Instantiate(Hurdle, gridManager.hurdleGrid[Pos.x, Pos.y].transform.position, quaternion.identity);
        hurdle.transform.parent = gridManager.hurdleGrid[Pos.x, Pos.y].transform;
        gridManager.hurdleGrid[Pos.x, Pos.y].GetComponent<GridCell_Hurdle>().SetGameObject(hurdle);


        hurdle.transform.position = gridManager.hurdleGrid[Pos.x, Pos.y].transform.position;

        hurdle.transform.localScale = gridManager.hurdleGrid[Pos.x, Pos.y].transform.localScale;
    }

    void SetVerticalHurdlesPos(Vector2Int Pos)
    {
        var hurdle = Instantiate(VerticalHurdle, gridManager.VerticalhurdleGrid[Pos.x, Pos.y].transform.position,
            quaternion.identity);
        //gridManager.hurdleGrid[Pos.x, Pos.y].GetComponent<GridCell_Hurdle>().isOccupied = true;
        hurdle.transform.parent = gridManager.VerticalhurdleGrid[Pos.x, Pos.y].transform;
        gridManager.VerticalhurdleGrid[Pos.x, Pos.y].GetComponent<GridCell_Hurdle>().SetGameObject(hurdle);

        hurdle.transform.position = gridManager.VerticalhurdleGrid[Pos.x, Pos.y].transform.position;

        hurdle.transform.localScale = gridManager.VerticalhurdleGrid[Pos.x, Pos.y].transform.localScale;
    }


    void SetEnemyPosition(Vector2Int Pos)
    {
        var enemy = Instantiate(Enemy, gridManager.gameGrid[Pos.x, Pos.y].transform.position, quaternion.identity);
        gridManager.gameGrid[Pos.x, Pos.y].GetComponent<GridCell>().SetGameObject(enemy);
        enemy.transform.parent = gridManager.gameGrid[Pos.x, Pos.y].transform;
        enemy.transform.position = gridManager.gameGrid[Pos.x, Pos.y].transform.position;
        enemy.transform.localScale = gridManager.gameGrid[Pos.x, Pos.y].transform.localScale;
    }

    void SetWayPointPosition(Vector2Int Pos)
    {
        var wayPoint = Instantiate(WayPoint, gridManager.gameGrid[Pos.x, Pos.y].transform.position,
            quaternion.identity);
        gridManager.gameGrid[Pos.x, Pos.y].GetComponent<GridCell>().SetGameObject(wayPoint);
        wayPoint.transform.parent = gridManager.gameGrid[Pos.x, Pos.y].transform;
        wayPoint.transform.position = gridManager.gameGrid[Pos.x, Pos.y].transform.position;
        wayPoint.transform.localScale = gridManager.gameGrid[Pos.x, Pos.y].transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!LevelManager.Instance.isPaused)
        {
            Swipe();
        }
    }

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0) && !isGameOver && !LevelManager.Instance.isPaused)
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0) && !isGameOver && !LevelManager.Instance.isPaused)
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && !isMoving)
            {
                Debug.Log("up swipe");
                MoveUp();
            }

            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && !isMoving)
            {
                Debug.Log("down swipe");
                MoveDown();
            }

            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && !isMoving)
            {
                Debug.Log("left swipe");
                MoveLeft();
            }

            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && !isMoving)
            {
                Debug.Log("right swipe");

                MoveRight();
            }
        }
    }


    IEnumerator AnimWait()
    {
        yield return new WaitForSeconds(0.5f);
        BoxSliderMovement();
    }


    /*void MoveRight()
    {
        if (!isMoving)
        {
            isMoving = true;
            for (int i = Player.GetComponentInParent<GridCell>().GetPositionY(); i < gridManager.width - 1; i++)
            {
                if (gridManager
                        .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                            Player.GetComponentInParent<GridCell>().GetPositionY() + 1].GetComponent<GridCell>().GetGameObject() == null
                    && !gridManager
                        .hurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                            Player.GetComponentInParent<GridCell>().GetPositionY()].GetComponent<GridCell_Hurdle>()
                        .isOccupied
                   )
                {
                    gridManager
                        .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                            i].GetComponent<GridCell>().SetGameObject(null);
                    Player.transform.parent = gridManager.gameGrid[
                        Player.GetComponentInParent<GridCell>().GetPositionX(),
                        i + 1].transform;

                    PlayerMovement = Player.transform.DOMoveX(gridManager.gameGrid[
                        Player.GetComponentInParent<GridCell>().GetPositionX(),
                        i + 1].transform.position.x, 0.5f);

                    gridManager
                        .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                            i + 1].GetComponent<GridCell>().SetGameObject(Player);
                    
                    //Player.transform.Translate(100f * Time.deltaTime, 0f, 0f);
                }
                //else
            }

            if(Player.GetComponentInParent<GridCell>().GetPositionY() < gridManager.width-1)
            {
                    if (!gridManager
                            .hurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                Player.GetComponentInParent<GridCell>().GetPositionY()]
                            .GetComponent<GridCell_Hurdle>()
                            .isOccupied)
                    {
                        if (gridManager
                            .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                Player.GetComponentInParent<GridCell>().GetPositionY()]
                            .GetComponent<GridCell>()
                            .Object
                            .transform.transform.CompareTag("Teleporter"))
                        {
                            StartCoroutine(AnimWait());
                        }
                        else if (gridManager
                                 .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                     Player.GetComponentInParent<GridCell>().GetPositionY()]
                                 .GetComponent<GridCell>()
                                 .Object
                                 .transform.transform.CompareTag("Enemy"))
                        {
                            //StartCoroutine(WaitforAnim());
                            isGameOver = true;
                            Destroy(Player, 0.25f);
                        }
                        else if (gridManager
                                 .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                     Player.GetComponentInParent<GridCell>().GetPositionY()]
                                 .GetComponent<GridCell>()
                                 .Object
                                 .transform.transform.CompareTag("WayPoint"))
                        {
                            //StartCoroutine(WaitforAnim());
                            isGameOver = true;
                            //Player.SetActive(false);
                            Debug.Log("Level Compeleted");
                            Destroy(Player, 0.5f);

                            StartCoroutine(NextLevelIEnumerator());


                        }
                    }

                    //break;
                }
            //}
        }

        if (Player.GetComponentInParent<GridCell>().GetPositionY() == gridManager.width - 1
            || gridManager
                .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                    Player.GetComponentInParent<GridCell>().GetPositionY() + 1].GetComponent<GridCell>().isOccupied
            || gridManager
                .hurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                    Player.GetComponentInParent<GridCell>().GetPositionY() + 1].GetComponent<GridCell_Hurdle>()
                .isOccupied
           )
        {
            //Debug.Log("Call");
            if (!PlayerMovement.active)
            {
                Player.transform.DOMoveX(gridManager
                        .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                            Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.x + 0.1f, 0.25f)
                    .OnComplete(() =>
                    {
                        Player.transform.DOMoveX(gridManager
                            .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.x, 0.25f);
                        isMoving = false;
                    });
            }
        }

        PlayerMovement.OnComplete(() => { isMoving = false; });
    }*/


    void MoveRight()
    {
        if (!isMoving)
        {
            isMoving = true;
            for (int i = Player.GetComponentInParent<GridCell>().GetPositionY(); i < gridManager.width - 1; i++)
            {
                if (gridManager
                        .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i + 1]
                        .GetComponent<GridCell>()
                        .GetGameObject() == null
                    && !gridManager
                        .hurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i]
                        .GetComponent<GridCell_Hurdle>()
                        .isOccupied
                   )
                {
                    Debug.Log("GameObject: " + gridManager
                        .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i + 1]
                        .GetComponent<GridCell>()
                        .GetGameObject());

                    Debug.Log($"gricell[{Player.GetComponentInParent<GridCell>().GetPositionX()}][{i}]");

                    var targetGrid =
                        gridManager.gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i + 1];

                    Debug.Log(Player.GetComponentInParent<GridCell>().GetPositionX() + "+" + (i + 1));

                    //if(isMoving)
                    {
                        MoveToTarget(targetGrid);
                    }
                }
                else
                {
                    if (Player.GetComponentInParent<GridCell>().GetPositionY() < gridManager.width - 1)
                    {
                        if (!gridManager
                                .hurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                    i]
                                .GetComponent<GridCell_Hurdle>()
                                .isOccupied)
                        {
                            if (gridManager
                                .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                    i + 1].GetComponent<GridCell>().Object.transform.transform.CompareTag("Teleporter"))
                            {
                                StartCoroutine(AnimWait());
                            }
                            else if (gridManager
                                     .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                         i + 1].GetComponent<GridCell>().Object.transform.transform.CompareTag("Enemy"))
                            {
                                Player.transform.DOMove(gridManager
                                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i + 1].transform
                                    .position, 0.8f);
                                isGameOver = true;
                                Destroy(Player, 0.5f);
                                StartCoroutine(ResetLevel());

                            }
                            else if (gridManager
                                     .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                         i + 1]
                                     .GetComponent<GridCell>()
                                     .Object
                                     .transform.transform.CompareTag("WayPoint"))
                            {
                                Player.transform.DOMove(gridManager
                                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i + 1].transform
                                    .position, 0.8f);
                                
                                PlayerMovement.OnComplete(() =>
                                {
                                    var particleObject = gridManager
                                        .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i + 1]
                                        .GetComponent<GridCell>().Object.transform.GetChild(0);

                                    particleObject.gameObject.SetActive(true);
                                    isGameOver = true;

                                    AudioManager.instance.Play("WayPoint");

                                    Debug.Log("Level Compeleted");
                                    StartCoroutine(NextLevelIEnumerator());
                                });
                            }
                        }
                    }

                    break;
                }
            }


            if (Player.GetComponentInParent<GridCell>().GetPositionY() == gridManager.width - 1
                || gridManager
                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                        Player.GetComponentInParent<GridCell>().GetPositionY() + 1].GetComponent<GridCell>().isOccupied
                || gridManager
                    .hurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                        Player.GetComponentInParent<GridCell>().GetPositionY()].GetComponent<GridCell_Hurdle>()
                    .isOccupied
               )
            {
                //Debug.Log("Call");
                if (!PlayerMovement.active)
                {
                    Player.transform.DOMoveX(gridManager
                                .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                    Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.x + 0.1f,
                            0.25f)
                        .OnComplete(() =>
                        {
                            Player.transform.DOMoveX(gridManager
                                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                        Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.x,
                                0.25f);
                            isMoving = false;
                        });
                }
            }
        }
    }


    void MoveToTarget(GameObject targetObject)
    {
        PlayerMovement = Player.transform.DOMove(targetObject.transform.position, moveSpeed).OnComplete(() =>
        {
            Player.GetComponentInParent<GridCell>().SetGameObject(null);
            Player.GetComponentInParent<GridCell>().isOccupied = false;


            targetObject.GetComponent<GridCell>().SetGameObject(Player);

            Player.transform.parent = targetObject.transform;

            isMoving = false;
        });
    }

    IEnumerator WaitToStopMoving()
    {
        yield return new WaitForSeconds(0.5f);
        isMoving = false;
    }

    IEnumerator NextLevelIEnumerator()
    {
        yield return new WaitForSeconds(0.5f);

        unlockNewLevel();

        if (!isLastLevel)
        {
            GameManager.instance.NextLevel();
        }
        else
        {
            LevelManager.Instance.LoadMenuScene();
        }

    }

    void MoveLeft()
    {
        if (!isMoving)
        {
            isMoving = true;
            for (int i = Player.GetComponentInParent<GridCell>().GetPositionY(); i > 0; i--)
            {
                if (gridManager
                        .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i - 1]
                        .GetComponent<GridCell>()
                        .GetGameObject() == null
                    && !gridManager
                        .hurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i - 1]
                        .GetComponent<GridCell_Hurdle>()
                        .isOccupied
                   )
                {
                    Debug.Log("GameObject: " + gridManager
                        .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i - 1]
                        .GetComponent<GridCell>()
                        .GetGameObject());

                    Debug.Log($"gricell[{Player.GetComponentInParent<GridCell>().GetPositionX()}][{i - 1}]");
                    var targetGrid =
                        gridManager.gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i - 1];

                    Debug.Log(Player.GetComponentInParent<GridCell>().GetPositionX() + "+" + (i - 1));

                    MoveToTarget(targetGrid);
                }
                else
                {
                    if (!gridManager
                            .hurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i - 1]
                            .GetComponent<GridCell_Hurdle>().isOccupied)
                    {
                        if (gridManager
                            .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                i - 1]
                            .GetComponent<GridCell>()
                            .Object
                            .transform.transform.CompareTag("Teleporter"))
                        {
                            StartCoroutine(AnimWait());
                        }
                        else if (gridManager
                                 .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i - 1]
                                 .GetComponent<GridCell>().Object.transform.transform.CompareTag("Enemy"))
                        {
                            Player.transform.DOMove(gridManager
                                .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i - 1].transform
                                .position, 0.8f);
                            isGameOver = true;
                            Destroy(Player, 0.5f);
                            StartCoroutine(ResetLevel());

                        }
                        else if (gridManager
                                 .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i - 1]
                                 .GetComponent<GridCell>().Object.transform.transform.CompareTag("WayPoint"))
                        {
                            Player.transform.DOMove(gridManager
                                .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i - 1].transform
                                .position, 0.8f);
                            PlayerMovement.OnComplete(() =>
                            {
                                var particleObject = gridManager
                                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(), i - 1]
                                    .GetComponent<GridCell>().Object.transform.GetChild(0);



                                particleObject.gameObject.SetActive(true);

                                isGameOver = true;
                                AudioManager.instance.Play("WayPoint");
                                Debug.Log("Level Compeleted");
                                Destroy(Player, 0.5f);
                                StartCoroutine(NextLevelIEnumerator());
                            });
                        }
                    }

                    break;
                }
            }


            if (Player.GetComponentInParent<GridCell>().GetPositionY() == 0
                || gridManager
                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                        Player.GetComponentInParent<GridCell>().GetPositionY() - 1].GetComponent<GridCell>().isOccupied
                || gridManager
                    .hurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                        Player.GetComponentInParent<GridCell>().GetPositionY() - 1].GetComponent<GridCell_Hurdle>()
                    .isOccupied
               )
            {
                if (!PlayerMovement.active)
                {
                    Player.transform.DOMoveX(gridManager
                                .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                    Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.x - 0.1f,
                            0.25f)
                        .OnComplete(() =>
                        {
                            Player.transform.DOMoveX(gridManager
                                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                        Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.x,
                                0.25f);
                            isMoving = false;
                        });
                }
            }
        }
    }

    void MoveUp()
    {
        if (!isMoving)
        {
            isMoving = true;
            for (int i = Player.GetComponentInParent<GridCell>().GetPositionX(); i < gridManager.height - 1; i++)
            {
                if (gridManager
                        .gameGrid[i + 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                        .GetComponent<GridCell>()
                        .GetGameObject() == null
                    && !gridManager
                        .VerticalhurdleGrid[i, Player.GetComponentInParent<GridCell>().GetPositionY()]
                        .GetComponent<GridCell_Hurdle>()
                        .isOccupied
                   )
                {
                    Debug.Log("GameObject: " + gridManager
                        .gameGrid[i + 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                        .GetComponent<GridCell>()
                        .GetGameObject());

                    Debug.Log($"gricell[{Player.GetComponentInParent<GridCell>().GetPositionX()}][{i}]");

                    var targetGrid =
                        gridManager.gameGrid[i + 1, Player.GetComponentInParent<GridCell>().GetPositionY()];

                    Debug.Log(Player.GetComponentInParent<GridCell>().GetPositionX() + "+" + (i + 1));

                    //if(isMoving)
                    {
                        MoveToTarget(targetGrid);
                    }
                }
                else
                {
                    if (!gridManager
                            .VerticalhurdleGrid[i, Player.GetComponentInParent<GridCell>().GetPositionY()]
                            .GetComponent<GridCell_Hurdle>().isOccupied)
                    {
                        if (gridManager
                            .gameGrid[i + 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                            .GetComponent<GridCell>()
                            .Object
                            .transform.transform.CompareTag("Teleporter"))
                        {
                            StartCoroutine(AnimWait());
                        }
                        else if (gridManager
                                 .gameGrid[i + 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                                 .GetComponent<GridCell>().Object.transform.transform.CompareTag("Enemy"))
                        {
                            Player.transform.DOMove(gridManager
                                .gameGrid[i + 1, Player.GetComponentInParent<GridCell>().GetPositionY()].transform
                                .position, 0.8f);
                            isGameOver = true;
                            Destroy(Player, 0.5f);
                            StartCoroutine(ResetLevel());

                        }
                        else if (gridManager
                                 .gameGrid[i + 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                                 .GetComponent<GridCell>().Object.transform.transform.CompareTag("WayPoint"))
                        {
                            Player.transform.DOMove(gridManager
                                .gameGrid[i + 1, Player.GetComponentInParent<GridCell>().GetPositionY()].transform
                                .position, 0.8f);

                            PlayerMovement.OnComplete(() =>
                            {
                                var particleObject = gridManager
                                    .gameGrid[i + 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                                    .GetComponent<GridCell>().Object.transform.GetChild(0);

                                particleObject.gameObject.SetActive(true);

                                isGameOver = true;
                                AudioManager.instance.Play("WayPoint");
                                Debug.Log("Level Compeleted");
                                Destroy(Player, 0.5f);
                                StartCoroutine(NextLevelIEnumerator());
                            });
                        }
                    }

                    break;
                }
            }

            if (Player.GetComponentInParent<GridCell>().GetPositionX() == gridManager.height - 1
                || gridManager
                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                        Player.GetComponentInParent<GridCell>().GetPositionY()].GetComponent<GridCell>().isOccupied
                || gridManager
                    .VerticalhurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                        Player.GetComponentInParent<GridCell>().GetPositionY()].GetComponent<GridCell_Hurdle>()
                    .isOccupied
               )
            {
                if (!PlayerMovement.active)
                {
                    Player.transform.DOMoveY(gridManager
                                .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                    Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.y + 0.1f,
                            0.25f)
                        .OnComplete(() =>
                        {
                            Player.transform.DOMoveY(gridManager
                                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                        Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.y,
                                0.25f);
                            isMoving = false;
                        });
                }
            }
        }
    }

    void MoveDown()
    {
        if (!isMoving)
        {
            isMoving = true;
            for (int i = Player.GetComponentInParent<GridCell>().GetPositionX(); i > 0; i--)
            {
                if (gridManager
                        .gameGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                        .GetComponent<GridCell>()
                        .GetGameObject() == null
                    && !gridManager
                        .VerticalhurdleGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                        .GetComponent<GridCell_Hurdle>()
                        .isOccupied
                   )
                {
                    Debug.Log("GameObject: " + gridManager
                        .gameGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                        .GetComponent<GridCell>()
                        .GetGameObject());

                    //Debug.Log($"gricell[{Player.GetComponentInParent<GridCell>().GetPositionX()}][{i}]");

                    var targetGrid =
                        gridManager.gameGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()];

                    //Debug.Log(Player.GetComponentInParent<GridCell>().GetPositionX() + "+" + (i + 1));

                    //if(isMoving)
                    {
                        MoveToTarget(targetGrid);
                    }
                }
                else
                {
                    if (!gridManager
                            .VerticalhurdleGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                            .GetComponent<GridCell_Hurdle>().isOccupied)
                    {
                        if (gridManager
                            .gameGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                            .GetComponent<GridCell>()
                            .Object
                            .transform.transform.CompareTag("Teleporter"))
                        {
                            StartCoroutine(AnimWait());
                        }
                        else if (gridManager
                                 .gameGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                                 .GetComponent<GridCell>().Object.transform.transform.CompareTag("Enemy"))
                        {
                            Player.transform.DOMove(gridManager
                                .gameGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()].transform
                                .position, 0.8f);
                            //StartCoroutine(WaitforAnim());
                            isGameOver = true;
                            Destroy(Player, 0.5f);

                            StartCoroutine(ResetLevel());


                        }
                        else if (gridManager
                                 .gameGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                                 .GetComponent<GridCell>().Object.transform.transform.CompareTag("WayPoint"))
                        {
                            var particleObject = gridManager
                                .gameGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()]
                                .GetComponent<GridCell>().Object.transform.GetChild(0);
                            Player.transform.DOMove(gridManager
                                .gameGrid[i - 1, Player.GetComponentInParent<GridCell>().GetPositionY()].transform
                                .position, 0.8f);
                            PlayerMovement.OnComplete(() =>
                            {
                                particleObject.gameObject.SetActive(true);


                                particleObject.GetComponent<ParticleSystem>().Play();
                                isGameOver = true;
                                AudioManager.instance.Play("WayPoint");
                                Debug.Log("Level Compeleted");
                                Destroy(Player, 0.5f);
                                StartCoroutine(NextLevelIEnumerator());
                            });
                        }
                    }

                    break;
                }
            }

            if (Player.GetComponentInParent<GridCell>().GetPositionX() == 0
                || gridManager
                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                        Player.GetComponentInParent<GridCell>().GetPositionY()].GetComponent<GridCell>().isOccupied
                || gridManager
                    .VerticalhurdleGrid[Player.GetComponentInParent<GridCell>().GetPositionX() - 1,
                        Player.GetComponentInParent<GridCell>().GetPositionY()].GetComponent<GridCell_Hurdle>()
                    .isOccupied
               )
            {
                if (!PlayerMovement.active)
                {
                    Player.transform.DOMoveY(gridManager
                                .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                    Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.y - 0.1f,
                            0.25f)
                        .OnComplete(() =>
                        {
                            Player.transform.DOMoveY(gridManager
                                    .gameGrid[Player.GetComponentInParent<GridCell>().GetPositionX(),
                                        Player.GetComponentInParent<GridCell>().GetPositionY()].transform.position.y,
                                0.25f);
                            isMoving = false;
                        });
                }
            }
        }
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void unlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("unlockLevel", PlayerPrefs.GetInt("unlockLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    void BoxSliderMovement()
    {
        if (x < 2)
        {
            gridManager.gameGrid[
                    boxSlider.GetComponentInParent<GridCell>().GetPositionX(),
                    boxSlider.GetComponentInParent<GridCell>().GetPositionY()].GetComponent<GridCell>()
                .SetGameObject(null);


            boxSlider.transform.parent = gridManager.gameGrid[
                boxSlider.GetComponentInParent<GridCell>().GetPositionX() - 1,
                boxSlider.GetComponentInParent<GridCell>().GetPositionY()].transform;

            gridManager.gameGrid[
                    boxSlider.GetComponentInParent<GridCell>().GetPositionX(),
                    boxSlider.GetComponentInParent<GridCell>().GetPositionY()].GetComponent<GridCell>()
                .SetGameObject(boxSlider);

            boxSlider.transform.DOMoveY(gridManager.gameGrid[
                boxSlider.GetComponentInParent<GridCell>().GetPositionX(),
                boxSlider.GetComponentInParent<GridCell>().GetPositionY()].transform.position.y, 0.25f);

            x++;
        }

        else
        {
            x = 0;
            gridManager.gameGrid[
                    boxSlider.GetComponentInParent<GridCell>().GetPositionX(),
                    boxSlider.GetComponentInParent<GridCell>().GetPositionY()].GetComponent<GridCell>()
                .SetGameObject(null);


            //Hard Code Value
            boxSlider.transform.parent =
                gridManager.gameGrid[BoxSliderPosition[0].x, BoxSliderPosition[0].y].transform;

            gridManager.gameGrid[
                    boxSlider.GetComponentInParent<GridCell>().GetPositionX(),
                    boxSlider.GetComponentInParent<GridCell>().GetPositionY()].GetComponent<GridCell>()
                .SetGameObject(boxSlider);

            boxSlider.transform.DOMoveY(gridManager.gameGrid[
                boxSlider.GetComponentInParent<GridCell>().GetPositionX(),
                boxSlider.GetComponentInParent<GridCell>().GetPositionY()].transform.position.y, 0.25f);
        }
    }
}